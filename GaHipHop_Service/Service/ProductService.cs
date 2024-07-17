using AutoMapper;
using Firebase.Auth;
using GaHipHop_Model.DTO.Request;
using GaHipHop_Model.DTO.Response;
using GaHipHop_Repository.Entity;
using GaHipHop_Repository.Repository;
using GaHipHop_Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace GaHipHop_Service.Service
{
    public class ProductService : IProductService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Tools.Firebase _firebase;

        public ProductService(IConfiguration configuration, IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, Tools.Firebase firebase)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _firebase = firebase;
        }

        //Status = TRUE
        public async Task<List<ProductResponse>> GetAllProduct(QueryObject queryObject)
        {
            var products = _unitOfWork.ProductRepository.Get(
                filter: p => queryObject.SearchText == null || p.ProductName.Contains(queryObject.SearchText),
                includeProperties: "Kind,Discount",
                pageIndex: 1,
                pageSize: 6)
                .Where(k => k.Status == true);

            if (!products.Any())
            {
                throw new CustomException.DataNotFoundException("No Product in Database");
            }

            var productResponses = new List<ProductResponse>();
            foreach (var product in products)
            {
                var productResponse = _mapper.Map<ProductResponse>(product);
                var discount = _unitOfWork.DiscountRepository.GetByID(product.DiscountId);

                if (discount != null && discount.ExpiredDate >= DateTime.Now && discount.Status)
                {
                    var discountedPrice = product.ProductPrice * (1 - discount.Percent / 100);
                    productResponse.CurrentPrice = Math.Round(discountedPrice, 3);
                }
                else
                {
                    productResponse.CurrentPrice = product.ProductPrice;
                }

                productResponses.Add(productResponse);
            }

            return productResponses;
        }

        public async Task<List<ProductAnyKindResponse>> GetAllProductFalse(QueryObject queryObject)
        {

            var products = _unitOfWork.ProductRepository.Get(
                filter: p => queryObject.SearchText == null || p.ProductName.Contains(queryObject.SearchText),
                pageIndex: 1,
                pageSize: 5)
                .Where(k => k.Status == false);

            if (!products.Any())
            {
                throw new CustomException.DataNotFoundException("No Product False in Database");
            }

            var productResponses = new List<ProductAnyKindResponse>();
            foreach (var product in products)
            {
                var productResponse = _mapper.Map<ProductAnyKindResponse>(product);

                // Discount calculation logic
                var discount = _unitOfWork.DiscountRepository.GetByID(product.DiscountId); 

                if (discount != null && discount.ExpiredDate >= DateTime.Now && discount.Status)
                {
                    var discountedPrice = product.ProductPrice * (1 - discount.Percent / 100);
                    productResponse.CurrentPrice = Math.Round(discountedPrice, 3);
                }
                else
                {
                    productResponse.CurrentPrice = product.ProductPrice;
                }

                productResponses.Add(productResponse);
            }

            return productResponses;
        }

        public async Task<List<ProductResponse>> GetAllProductByCategoryId(long id)
        {
            var category = _unitOfWork.CategoryRepository.GetByID(id);

            if (category == null)
            {
                throw new CustomException.DataNotFoundException($"Category not found with ID: {id}");
            }

            var products = _unitOfWork.ProductRepository.Get(
                filter: k => k.CategoryId == id && k.Status == true,
                includeProperties: "Kind",
                pageIndex: 1,
                pageSize: 5)
                .ToList();
            if (!products.Any())
            {
                throw new CustomException.DataNotFoundException($"Category with ID: {id} does not have any Kind");
            }

            var productResponses = _mapper.Map<List<ProductResponse>>(products);
            return productResponses;
        }

        public async Task<ProductAnyKindResponse> GetProductById(long id)
        {
            try
            {
                var product = _unitOfWork.ProductRepository.Get(filter: p => p.Id == id
                                                              , includeProperties: "Kind,Discount,Category").FirstOrDefault();

                if (product == null)
                {
                    throw new CustomException.DataNotFoundException("Product not found");
                }
                var productResponse = _mapper.Map<ProductAnyKindResponse>(product);
                    productResponse = CalculateDiscountedPriceAboutKind(product);

                _mapper.Map(product, productResponse);
                return productResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*public async Task<ProductResponse> CreateProduct(ProductRequest productRequest)
        {
            var accountId = Authentication.GetUserIdFromHttpContext(_httpContextAccessor.HttpContext);
            if (!long.TryParse(accountId, out long Id))
            {
                throw new CustomException.ForbbidenException("User ID claim invalid.");
            }

            var admin = _unitOfWork.AdminRepository.Get(a => a.Id == Id).FirstOrDefault();

            

            var existingProduct = _unitOfWork.ProductRepository.Get().FirstOrDefault(p => p.ProductName.ToLower() == productRequest.ProductName.ToLower());

            if (existingProduct != null)
            {
                throw new CustomException.DataExistException($"Product with name '{productRequest.ProductName}' already exists.");
            }

            var discount = _unitOfWork.DiscountRepository.GetByID(productRequest.DiscountId);
            if (discount == null)
            {
                throw new CustomException.DataNotFoundException("Discount not found.");
            }

            var category = _unitOfWork.CategoryRepository.GetByID(productRequest.CategoryId);
            if (category == null)
            {
                throw new CustomException.DataNotFoundException("Category not found.");
            }

            var newProduct = _mapper.Map<Product>(productRequest);
            newProduct.CreateDate = DateTime.UtcNow;
            newProduct.Status = true;
            newProduct.AdminId = admin.Id;
            newProduct.Discount = discount;
            newProduct.Category = category;

            var productResponse = CalculateDiscountedPrice(newProduct);

            _unitOfWork.ProductRepository.Insert(newProduct);

            var newKind = _mapper.Map<Kind>(productRequest);
            newKind.ProductId = newProduct.Id;
            newKind.ColorName = productRequest.ColorName;
            newKind.Quantity = productRequest.KindQuantity;
            newKind.Status = true;
            if (productRequest.File != null)
            {
                if (productRequest.File.Length >= 10 * 1024 * 1024)
                {
                    throw new CustomException.InvalidDataException("File size exceeds the maximum allowed limit.");
                }
                string imageDownloadUrl = await _firebase.UploadImage(productRequest.File);
                newKind.Image = imageDownloadUrl;
            }

            _unitOfWork.KindRepository.Insert(newKind);
            newProduct.StockQuantity = newKind.Quantity;
            _unitOfWork.Save();

            _mapper.Map(newProduct, productResponse);
            return productResponse;
        }*/

        public async Task<ProductAnyKindResponse> CreateProduct(ProductRequest productRequest)
        {
            var accountId = Authentication.GetUserIdFromHttpContext(_httpContextAccessor.HttpContext);
            if (!long.TryParse(accountId, out long Id))
            {
                throw new CustomException.ForbbidenException("User ID claim invalid.");
            }

            var admin = _unitOfWork.AdminRepository.Get(a => a.Id == Id).FirstOrDefault();

            var existingProduct = _unitOfWork.ProductRepository.Get().FirstOrDefault(p => p.ProductName.ToLower() == productRequest.ProductName.ToLower());

            if (existingProduct != null)
            {
                throw new CustomException.DataExistException($"Product with name '{productRequest.ProductName}' already exists.");
            }

            var discount = _unitOfWork.DiscountRepository.GetByID(productRequest.DiscountId);
            if (discount == null)
            {
                throw new CustomException.DataNotFoundException("Discount not found.");
            }

            var category = _unitOfWork.CategoryRepository.GetByID(productRequest.CategoryId);
            if (category == null)
            {
                throw new CustomException.DataNotFoundException("Category not found.");
            }

            var newProduct = _mapper.Map<Product>(productRequest);
            newProduct.CreateDate = DateTime.UtcNow;
            newProduct.Status = true;
            newProduct.AdminId = admin.Id;
            newProduct.Discount = discount;
            newProduct.Category = category;

            var productResponse = CalculateDiscountedPriceAboutKind(newProduct);

            _unitOfWork.ProductRepository.Insert(newProduct);

            foreach (var kindRequest in productRequest.Kinds)
            {
                var newKind = _mapper.Map<Kind>(kindRequest);
                newKind.ProductId = newProduct.Id;
                newKind.Status = true;

                if (kindRequest.File != null)
                {
                    if (kindRequest.File.Length >= 10 * 1024 * 1024)
                    {
                        throw new CustomException.InvalidDataException("File size exceeds the maximum allowed limit.");
                    }
                    string imageDownloadUrl = await _firebase.UploadImage(kindRequest.File);
                    newKind.Image = imageDownloadUrl;
                }

                _unitOfWork.KindRepository.Insert(newKind);
                newProduct.StockQuantity += newKind.Quantity;
            }

            _unitOfWork.Save();

            _mapper.Map(newProduct, productResponse);
            return productResponse;
        }



        public async Task<ProductResponse> UpdateProduct(long id, ProductRequest productRequest)
        {
            // 1. Fetch the Existing Product
            var existingProduct = _unitOfWork.ProductRepository.GetByID(id); // Use async variant

            if (existingProduct == null)
            {
                throw new CustomException.DataNotFoundException($"Product with ID {id} not found."); // Use a more specific exception type
            }

            // 2. Check for Duplicate Product Name
            if (existingProduct.ProductName.ToLower() != productRequest.ProductName.ToLower())
            {
                var duplicateExists = _unitOfWork.ProductRepository.Get((p => p.Id != id
                                                                                      && p.ProductName.ToLower() == productRequest.ProductName.ToLower()));

                if (duplicateExists == null)
                {
                    throw new CustomException.DataExistException($"Product with name '{productRequest.ProductName}' already exists.");
                }
            }

            // 3. Update Product Properties
            _mapper.Map(productRequest, existingProduct);
            existingProduct.ModifiedDate = DateTime.Now;

            // Discount calculation logic
            var productResponse = CalculateDiscountedPrice(existingProduct); // Centralize discount logic

            // 4. Save Changes and Map the Response
            _unitOfWork.Save(); // Use the async version of Save

            _mapper.Map(existingProduct, productResponse); // Update response with final product state
            return productResponse;
        }


        private ProductResponse CalculateDiscountedPrice(Product existingProduct)
        {
            var productResponse = _mapper.Map<ProductResponse>(existingProduct);

            if (existingProduct.DiscountId != null)
            {
                var discount = _unitOfWork.DiscountRepository.GetByID(existingProduct.DiscountId); // Fetch discount

                if (discount != null && discount.ExpiredDate >= DateTime.Now && discount.Status)
                {
                    var discountedPrice = existingProduct.ProductPrice * (1 - discount.Percent / 100);
                    productResponse.CurrentPrice = Math.Round(discountedPrice, 3);
                }
                else
                {
                    productResponse.CurrentPrice = existingProduct.ProductPrice;
                }
            }
            else
            {
                productResponse.CurrentPrice = existingProduct.ProductPrice; // No discount
            }

            return productResponse;
        }


        private ProductAnyKindResponse CalculateDiscountedPriceAboutKind(Product existingProduct)
        {
            var productResponse = _mapper.Map<ProductAnyKindResponse>(existingProduct);

            if (existingProduct.DiscountId != null)
            {
                var discount = _unitOfWork.DiscountRepository.GetByID(existingProduct.DiscountId); // Fetch discount

                if (discount != null && discount.ExpiredDate >= DateTime.Now && discount.Status)
                {
                    var discountedPrice = existingProduct.ProductPrice * (1 - discount.Percent / 100);
                    productResponse.CurrentPrice = Math.Round(discountedPrice, 3);
                }
                else
                {
                    productResponse.CurrentPrice = existingProduct.ProductPrice;
                }
            }
            else
            {
                productResponse.CurrentPrice = existingProduct.ProductPrice; // No discount
            }

            return productResponse;
        }

        public async Task<bool> DeleteProduct(long id)
        {
            try
            {

                var product = _unitOfWork.ProductRepository.GetByID(id);
                if (product == null)
                {
                    throw new CustomException.DataNotFoundException("Product not found.");
                }

                var kinds = _unitOfWork.KindRepository.Get().Where(p => p.ProductId == id);
                foreach (var kind in kinds)
                {
                    kind.Status = false;
                    _unitOfWork.KindRepository.Update(kind);
                }

                product.Status = false;
                _unitOfWork.ProductRepository.Update(product);
                _unitOfWork.Save();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
