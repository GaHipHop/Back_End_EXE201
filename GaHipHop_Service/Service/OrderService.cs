using AutoMapper;
using GaHipHop_Model.DTO.Request;
using GaHipHop_Model.DTO.Response;
using GaHipHop_Repository.Entity;
using GaHipHop_Repository.Repository;
using GaHipHop_Service.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tools;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace GaHipHop_Service.Service
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICartService _cartService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailService;
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper, ICartService cartService, IHttpContextAccessor httpContextAccessor, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cartService = cartService;
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
        }
        public async Task<OrderResponse> CreateOrder(OrderRequest orderRequest)
        {

            Random rand = new Random();
            int randomNumber = rand.Next(10000, 99999);

            var cartItems = _cartService.GetCartItems();

            var userInfo = new UserInfo
            {
                UserName = orderRequest.UserName,
                Email = orderRequest.Email,
                Phone = orderRequest.Phone,
                Address = orderRequest.Address,
                Province = orderRequest.Province,
                Wards = orderRequest.Wards
            };
            _unitOfWork.UserInfoRepository.Insert(userInfo);
            _unitOfWork.Save();

            var order = _mapper.Map<Order>(orderRequest);

            var checkOrderCode = _unitOfWork.OrderRepository.Get(filter: cod => cod.OrderCode == order.OrderCode);

            order.UserId = userInfo.Id;
            order.OrderRequirement = orderRequest.OrderRequirement;
            order.PaymentMethod = orderRequest.PaymentMethod;
/*            order.AdminId = 1;*/

            while (_unitOfWork.OrderRepository.Get(filter: cod => cod.OrderCode == order.OrderCode).Any())
            {
                randomNumber = new Random().Next(10000, 99999);
                order.OrderCode = "ORD" + randomNumber.ToString("D5");
            }

            order.OrderCode = "ORD" + randomNumber.ToString("D5");


            order.CreateDate = DateTime.Now;
            order.TotalPrice = cartItems.TotalPrice;
            order.Status = "Pending";

            _unitOfWork.OrderRepository.Insert(order);
            _unitOfWork.Save();

            foreach(var cartItem in cartItems.Items)
            {
                var orderDetail = _mapper.Map<OrderDetails>(cartItem);

                orderDetail.OrderId = order.Id;

                orderDetail.OrderPrice = cartItem.ProductPrice;

                orderDetail.OrderQuantity = cartItem.Quantity;

                orderDetail.KindId = cartItem.Id;

                _unitOfWork.OrderDetailsRepository.Insert(orderDetail);
            }

            _cartService.ClearCart();

            return await Task.FromResult(_mapper.Map<OrderResponse>(order));
        }

        public async Task<bool> UpdateStatusOrderConfirmed(long id)
        {
            try
            {
                var accountId = Authentication.GetUserIdFromHttpContext(_httpContextAccessor.HttpContext);

                //var checkmanager = ManagerPermissionHelper.CheckManagerPermissionAndRetrieveInfo(_httpContextAccessor, _unitOfWork);

                var existingOrder = _unitOfWork.OrderRepository.GetByID(id);

                if (existingOrder == null)
                {
                    throw new CustomException.DataNotFoundException("Order not found.");
                }

                if (existingOrder.Status == "Confirmed")
                {
                    throw new CustomException.InvalidDataException("Cannot update order that is already confirmed.");
                }

                /*var order = _mapper.Map(orderRequest, existingOrder);

                order.Status = "Confirmed";

                order.AdminId = 1;*/

                if (!long.TryParse(accountId, out long Id))
                {
                    throw new CustomException.ForbbidenException("User ID claim invalid.");
                }

                var admin = _unitOfWork.AdminRepository.Get(a => a.Id == Id).FirstOrDefault();

                existingOrder.AdminId = admin.Id;

                existingOrder.Status = "Confirmed";

                _unitOfWork.OrderRepository.Update(existingOrder);
/*                _unitOfWork.Save();*/

                var orderDetails = _unitOfWork.OrderDetailsRepository.Get(filter: od => od.OrderId == existingOrder.Id).ToList();

                foreach (var orderDetail in orderDetails)
                {
                    var kind = _unitOfWork.KindRepository.GetByID(orderDetail.KindId);

                    if (kind != null)
                    {
                        if (kind.Quantity - orderDetail.OrderQuantity < 0)
                        {
                            throw new CustomException.InvalidDataException("Cannot confirm order as it would result in a product's quantity going below zero.");
                        }
                        else
                        {
                            kind.Quantity -= orderDetail.OrderQuantity;

                            _unitOfWork.KindRepository.Update(kind);
                        }                    
                    }
                }

                

                _unitOfWork.Save();


                var userInfo = _unitOfWork.UserInfoRepository.GetByID(existingOrder.UserId);
                if (userInfo == null)
                {
                    throw new CustomException.DataNotFoundException("User information not found.");
                }

                var orderResponse = new OrderResponse
                {
                    OrderCode = existingOrder.OrderCode,
                    TotalPrice = existingOrder.TotalPrice,
                    PaymentMethod = existingOrder.PaymentMethod,
                    CreateDate = existingOrder.CreateDate,
                    UserInfo = new UserInfoResponse
                    {
                        UserName = userInfo.UserName,
                        Email = userInfo.Email,
                        Phone = userInfo.Phone,
                        Province = userInfo.Province,
                        Wards = userInfo.Wards,
                        Address = userInfo.Address
                    },
                    OrderDetails = orderDetails.Select(od =>
                    {
                        var kind = _unitOfWork.KindRepository.GetByID(od.KindId);
                        var product = _unitOfWork.ProductRepository.GetByID(kind.ProductId); // Assuming there's a ProductRepository to get product details

                        return new OrderDetailResponse
                        {
                            OrderQuantity = od.OrderQuantity,
                            OrderPrice = od.OrderPrice,
                            ProductName = product.ProductName, // Setting the product name
                            ColorName = kind.ColorName
                        };
                    }).ToList()
                };

                await _emailService.SendConfirmedOrderEmailAsync(userInfo.Email, orderResponse);

                /*var orderResponse = _mapper.Map<OrderResponse>(existingOrder);
                return orderResponse;*/

                return true;
            }
            catch (CustomException.InternalServerErrorException ex)
            {
                throw new CustomException.InternalServerErrorException("An error occurred during data processing.", ex);
            }
        }

        public async Task<bool> UpdateStatusOrderReject(long id)
        {
            try
            {
                var accountId = Authentication.GetUserIdFromHttpContext(_httpContextAccessor.HttpContext);
                //Authentication authentication = new(_configuration, _unitOfWork);

                //var checkmanager = ManagerPermissionHelper.CheckManagerPermissionAndRetrieveInfo(_httpContextAccessor, _unitOfWork);

                var existingOrder = _unitOfWork.OrderRepository.GetByID(id);

                if (existingOrder == null)
                {
                    throw new CustomException.DataNotFoundException("Order not found.");
                }

                if (existingOrder.Status == "Rejected")
                {
                    throw new CustomException.InvalidDataException("Cannot update order that is already Rejected.");
                }

                if (existingOrder.Status == "Confirmed")
                {
                    throw new CustomException.InvalidDataException("This order cannot be rejected because you have already confirmed.");
                }

                /*var order = _mapper.Map(orderRequest, existingOrder);

                order.Status = "Reject";

                order.AdminId = 1;*/

                if (!long.TryParse(accountId, out long Id))
                {
                    throw new CustomException.ForbbidenException("User ID claim invalid.");
                }

                var admin = _unitOfWork.AdminRepository.Get(a => a.Id == Id).FirstOrDefault();

                existingOrder.AdminId = admin.Id;

                existingOrder.Status = "Rejected";

                _unitOfWork.OrderRepository.Update(existingOrder);
                _unitOfWork.Save();

                var orderDetails = _unitOfWork.OrderDetailsRepository.Get(filter: od => od.OrderId == existingOrder.Id).ToList();



                var userInfo = _unitOfWork.UserInfoRepository.GetByID(existingOrder.UserId);
                if (userInfo == null)
                {
                    throw new CustomException.DataNotFoundException("User information not found.");
                }

                var orderResponse = new OrderResponse
                {
                    OrderCode = existingOrder.OrderCode,
                    TotalPrice = existingOrder.TotalPrice,
                    PaymentMethod = existingOrder.PaymentMethod,
                    CreateDate = existingOrder.CreateDate,
                    UserInfo = new UserInfoResponse
                    {
                        UserName = userInfo.UserName,
                        Email = userInfo.Email,
                        Phone = userInfo.Phone,
                        Province = userInfo.Province,
                        Wards = userInfo.Wards,
                        Address = userInfo.Address
                    },
                    OrderDetails = orderDetails.Select(od =>
                    {
                        var kind = _unitOfWork.KindRepository.GetByID(od.KindId);
                        var product = _unitOfWork.ProductRepository.GetByID(kind.ProductId); // Assuming there's a ProductRepository to get product details

                        return new OrderDetailResponse
                        {
                            OrderQuantity = od.OrderQuantity,
                            OrderPrice = od.OrderPrice,
                            ProductName = product.ProductName, // Setting the product name
                            ColorName = kind.ColorName
                        };
                    }).ToList()
                };

                await _emailService.SendRejectedOrderEmailAsync(userInfo.Email, orderResponse);

                return true;
            }
            catch (CustomException.InternalServerErrorException ex)
            {
                throw new CustomException.InternalServerErrorException("An error occurred during data processing.", ex);
            }
        }
        public List<OrderResponse> GetAllOrderByStatusPending(string? keyword, int pageIndex, int pageSize)
        {
            try
            {
                //var checkadmin = AdminPermissionHelper.CheckAdminPermissionAndRetrieveInfo(_httpContextAccessor, _unitOfWork);

                Expression<Func<Order, bool>> filter = s => (string.IsNullOrEmpty(keyword) ||
                                                            s.OrderCode.Contains(keyword) || 
                                                            s.UserInfo.Email.Contains(keyword) ||
                                                            s.UserInfo.UserName.Contains(keyword))
                                                            && s.Status == "Pending";

                var listOrder = _unitOfWork.OrderRepository.Get(
                    filter: filter,
                    includeProperties: "UserInfo,OrderDetails,OrderDetails.Kind,OrderDetails.Kind.Product",
                    pageIndex: pageIndex,
                    pageSize: pageSize
                );
                var OrderResponses = _mapper.Map<List<OrderResponse>>(listOrder);
                return OrderResponses;
            }
            catch (CustomException.InternalServerErrorException ex)
            {
                throw new CustomException.InternalServerErrorException(/*"An error occurred during data processing."*/ ex.Message);
            }
        }

        public List<OrderResponse> GetAllOrderByStatusConfirmed(string? keyword, int pageIndex, int pageSize)
        {
            try
            {
                //var checkadmin = AdminPermissionHelper.CheckAdminPermissionAndRetrieveInfo(_httpContextAccessor, _unitOfWork);

                Expression<Func<Order, bool>> filter = s => (string.IsNullOrEmpty(keyword) ||
                                                            s.OrderCode.Contains(keyword) ||
                                                            s.UserInfo.Email.Contains(keyword) ||
                                                            s.UserInfo.UserName.Contains(keyword))
                                                            && s.Status == "Confirmed";

                var listOrder = _unitOfWork.OrderRepository.Get(
                    filter: filter,
                    includeProperties: "UserInfo,OrderDetails,OrderDetails.Kind,OrderDetails.Kind.Product",
                    pageIndex: pageIndex,
                    pageSize: pageSize
                );
                var OrderResponses = _mapper.Map<List<OrderResponse>>(listOrder);
                return OrderResponses;
            }
            catch (CustomException.InternalServerErrorException ex)
            {
                throw new CustomException.InternalServerErrorException("An error occurred during data processing.", ex);
            }
        }

        public List<OrderResponse> GetAllOrderByStatusReject(string? keyword, int pageIndex, int pageSize)
        {
            try
            {

                Expression<Func<Order, bool>> filter = s => (string.IsNullOrEmpty(keyword) ||
                                            s.OrderCode.Contains(keyword) ||
                                            s.UserInfo.Email.Contains(keyword) ||
                                            s.UserInfo.UserName.Contains(keyword))
                                            && s.Status == "Rejected";

                var listOrder = _unitOfWork.OrderRepository.Get(
                    filter: filter,
                    includeProperties: "UserInfo,OrderDetails,OrderDetails.Kind,OrderDetails.Kind.Product",
                    pageIndex: pageIndex,
                    pageSize: pageSize
                );
                var OrderResponses = _mapper.Map<List<OrderResponse>>(listOrder);
                return OrderResponses;
            }
            catch (CustomException.InternalServerErrorException ex)
            {
                throw new CustomException.InternalServerErrorException("An error occurred during data processing.", ex);
            }
        }

        public async Task<OrderResponse> GetOrderById(long id)
        {
            try
            {

                var order = _unitOfWork.OrderRepository.Get(
                    filter: o => o.Id == id, includeProperties: "UserInfo,OrderDetails"
                ).FirstOrDefault();

                if (order == null)
                {
                    throw new CustomException.DataNotFoundException("Order not found.");
                }

                var orderResponse = _mapper.Map<OrderResponse>(order);
                return orderResponse;
            }
            catch (CustomException.InternalServerErrorException ex)
            {
                throw new CustomException.InternalServerErrorException("An error occurred during data processing.", ex);
            }
        }

        public async Task<OrderSummaryResponse> GetOrdersSummaryByMonthYear(int month, int year)
        {
            try
            {
                var orders = _unitOfWork.OrderRepository.Get(
                    filter: o => o.CreateDate.Month == month && o.CreateDate.Year == year && o.Status == "Confirmed",
                    includeProperties: "UserInfo,OrderDetails,OrderDetails.Kind,OrderDetails.Kind.Product"
                ).ToList();

                var previousMonthOrders = _unitOfWork.OrderRepository.Get(
                    filter: o => o.CreateDate.Month == month - 1 && o.CreateDate.Year == year && o.Status == "Confirmed",
                    includeProperties: "UserInfo,OrderDetails,OrderDetails.Kind,OrderDetails.Kind.Product"
                ).ToList();

                // Tính Count và TotalAmount
                int count = orders.Count;
                double totalAmount = orders.Sum(o => o.TotalPrice);
                int quantitySold = orders.Sum(o => o.OrderDetails.Sum(qs => qs.OrderQuantity));


                int countPreviousMonth = previousMonthOrders.Count;
                double totalAmountPreviousMonth = previousMonthOrders.Sum(o => o.TotalPrice);

                var mostSoldProductGroup = orders
                    .SelectMany(o => o.OrderDetails)
                    .GroupBy(od => od.Kind.Product)
                    .OrderByDescending(g => g.Sum(od => od.OrderQuantity))
                    .FirstOrDefault();

                var mostSoldProduct = mostSoldProductGroup?.Key?.ProductName ?? "Not product.";
                var mostSoldProductQuantity = mostSoldProductGroup?.Sum(od => od.OrderQuantity) ?? 0;
                var orderResponses = _mapper.Map<List<OrderResponse>>(orders);

                var orderSummaryResponse = new OrderSummaryResponse
                {
                    Orders = orderResponses,
                    Count = count,
                    TotalAmount = totalAmount,
                    quantitySold = quantitySold,
                    MostSoldProduct = mostSoldProduct,
                    MostSoldProductQuantity = mostSoldProductQuantity,
                    CountPreviousMonth = countPreviousMonth,
                    TotalAmountPreviousMonth = totalAmountPreviousMonth
                };

                return await Task.FromResult(orderSummaryResponse);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting orders summary: " + ex.Message);
            }
        }


    }
}
