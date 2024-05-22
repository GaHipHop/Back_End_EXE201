using AutoMapper;
using GaHipHop_Model.DTO.Response;
using GaHipHop_Repository.Entity;
using GaHipHop_Repository.Repository;
using GaHipHop_Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;
using static System.Net.Mime.MediaTypeNames;

namespace GaHipHop_Service.Service
{
    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CartService(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        private List<CartItem> GetCartItemsFromSession()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var cartItemsJson = session.GetString("CartItems");
            return cartItemsJson == null ? new List<CartItem>() : JsonConvert.DeserializeObject<List<CartItem>>(cartItemsJson);
        }

        private void SaveCartItemsToSession(List<CartItem> cartItems)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var cartItemsJson = JsonConvert.SerializeObject(cartItems);
            session.SetString("CartItems", cartItemsJson);
        }

        /*public CartItem AddItem(long productId, int quantity)
        {
            var product = _unitOfWork.ProductRepository.GetByID(productId);
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            var cartItems = GetCartItemsFromSession();
            var existingItem = cartItems.FirstOrDefault(item => item.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cartItems.Add(new CartItem { ProductId = productId, Quantity = quantity, ProductPrice = product.ProductPrice, ProductName = product.ProductName });
            }
            SaveCartItemsToSession(cartItems);
            return _mapper.Map<CartItem>(existingItem ?? cartItems.Last());
        }*/

        /*public CartItem AddItem(long productId, int quantity)
        {
            var product = _unitOfWork.ProductRepository.GetByID(productId);
            if (product == null)
            {
                throw new CustomException.DataNotFoundException("Product not found.");
            }

            if (quantity > product.ProductQuantity)
            {
                throw new CustomException.InvalidDataException("Requested quantity is greater than the available stock.");
            }
            // Check if the product has a discount
            if (product.DiscountId != null)
            {
                var discount = _unitOfWork.DiscountRepository.GetByID(product.DiscountId);
                // Check if the discount exists and is still valid
                if (discount != null && discount.ExpiredDate >= DateTime.Now && discount.Status)
                {
                    // Calculate the discounted price
                    var discountedPrice = product.ProductPrice * (1 - discount.Percent / 100);
                    product.ProductPrice = discountedPrice;
                }
            }

            var cartItems = GetCartItemsFromSession();
            var existingItem = cartItems.FirstOrDefault(item => item.ProductId == productId);
            if (existingItem != null)
            {
                if (existingItem.Quantity + quantity > product.ProductQuantity)
                {
                    throw new CustomException.InvalidDataException("Requested quantity exceeds the available stock.");
                }
                existingItem.Quantity += quantity;
            }
            else
            {
                cartItems.Add(new CartItem { ProductId = productId, Quantity = quantity, ProductPrice = product.ProductPrice, ProductName = product.ProductName });
            }
            SaveCartItemsToSession(cartItems);
            return _mapper.Map<CartItem>(existingItem ?? cartItems.Last());
        }*/

        public CartItem AddItem(long productId, int quantity, long imageId)
        {
            var product = _unitOfWork.ProductRepository.GetByID(productId);
            if (product == null)
            {
                throw new CustomException.DataNotFoundException("Product not found.");
            }

            /*if (quantity > product.ProductQuantity)
            {
                throw new CustomException.InvalidDataException("Requested quantity is greater than the available stock.");
            }*/
            var cartItems = GetCartItemsFromSession();
            var totalQuantityInCart = cartItems.Where(item => item.ProductId == productId).Sum(item => item.Quantity);
            if (totalQuantityInCart + quantity > product.StockQuantity)
            {
                throw new CustomException.InvalidDataException("Requested quantity exceeds the available stock.");
            }

            bool imageExists = _unitOfWork.KindRepository.Exists(img => img.Id == imageId & img.ProductId == productId);
            if (!imageExists)
            {
                throw new CustomException.DataNotFoundException("Product image not found.");
            }

            if (product.DiscountId != null)
            {
                var discount = _unitOfWork.DiscountRepository.GetByID(product.DiscountId);
 
                if (discount != null && discount.ExpiredDate >= DateTime.Now && discount.Status)
                {

                    var discountedPrice = product.ProductPrice * (1 - discount.Percent / 100);
                    product.ProductPrice = discountedPrice;
                }
            }

            //var cartItems = GetCartItemsFromSession();
            var existingItem = cartItems.FirstOrDefault(item => item.ProductId == productId && item.ProductImage == imageId);
            if (existingItem != null)
            {
                if (existingItem.Quantity + quantity > product.StockQuantity)
                {
                    throw new CustomException.InvalidDataException("Requested quantity exceeds the available stock.");
                }
                existingItem.Quantity += quantity;
            }
            else
            {
                cartItems.Add(new CartItem { ProductId = productId, Quantity = quantity, ProductPrice = product.ProductPrice, ProductName = product.ProductName, ProductImage = imageId });
            }
            SaveCartItemsToSession(cartItems);
            return _mapper.Map<CartItem>(existingItem ?? cartItems.Last());
        }


        public void RemoveItem(long productId)
        {
            var cartItems = GetCartItemsFromSession();
            var item = cartItems.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                cartItems.Remove(item);
                SaveCartItemsToSession(cartItems);
            }
        }

        /*public void UpdateItemQuantity(long productId, int quantity)
        {
            var cartItems = GetCartItemsFromSession();
            var item = cartItems.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                item.Quantity = quantity;
                SaveCartItemsToSession(cartItems);
            }
        }*/

        /*public void UpdateItemQuantity(long productId, int quantity)
        {
            var cartItems = GetCartItemsFromSession();
            var item = cartItems.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                var product = _unitOfWork.ProductRepository.GetByID(productId);
                if (product == null)
                {
                    throw new CustomException.DataNotFoundException("Product not found.");
                }

                if (quantity > product.ProductQuantity)
                {
                    throw new CustomException.InvalidDataException("Requested quantity exceeds the available stock.");
                }

                item.Quantity = quantity;
                SaveCartItemsToSession(cartItems);
            }
            else
            {
                throw new CustomException.DataNotFoundException("Item not found in cart.");
            }
        }*/

        public void UpdateItemQuantity(long productId, int quantity, long imageId)
        {
            var cartItems = GetCartItemsFromSession();
            var item = cartItems.FirstOrDefault(i => i.ProductId == productId && i.ProductImage == imageId);
            if (item != null)
            {
                var product = _unitOfWork.ProductRepository.GetByID(productId);
                if (product == null)
                {
                    throw new CustomException.DataNotFoundException("Product not found.");
                }

                // Check if the total quantity of the item in the cart exceeds the available stock
                var totalQuantityInCart = cartItems.Where(i => i.ProductId == productId).Sum(i => i.Quantity) - item.Quantity;
                if (quantity > product.StockQuantity - totalQuantityInCart)
                {
                    throw new CustomException.InvalidDataException("Requested quantity exceeds the available stock.");
                }

                // Check if the requested image exists for the product
                bool imageExists = _unitOfWork.KindRepository.Exists(img => img.Id == imageId && img.ProductId == productId);
                if (!imageExists)
                {
                    throw new CustomException.DataNotFoundException("Product image not found.");
                }

                item.Quantity = quantity;
                SaveCartItemsToSession(cartItems);
            }
            else
            {
                throw new CustomException.DataNotFoundException("Item not found in cart.");
            }
        }



        public void ClearCart()
        {
            var cartItems = new List<CartItem>();
            SaveCartItemsToSession(cartItems);
        }

        /*public List<CartItem> GetCartItems()
        {
            var cartItems = GetCartItemsFromSession();
            return cartItems.Select(item => _mapper.Map<CartItem>(item)).ToList();
        }*/
        public CartResponse GetCartItems()
        {
            var cartItems = GetCartItemsFromSession();
            var cartItemDTOs = cartItems.Select(item => _mapper.Map<CartItem>(item)).ToList();
            var totalPrice = cartItemDTOs.Sum(item => item.ProductPrice * item.Quantity);
            return new CartResponse { Items = cartItemDTOs, TotalPrice = totalPrice };
        }

    }
}
