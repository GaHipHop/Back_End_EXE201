using AutoMapper;
using Firebase.Auth;
using GaHipHop_Model.DTO.Request;
using GaHipHop_Model.DTO.Response;
using GaHipHop_Repository.Entity;
using GaHipHop_Repository.Repository;
using GaHipHop_Service.Interfaces;
using Microsoft.AspNetCore.Http;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ProductResponse>> GetAllProduct(QueryObject queryObject)
        {
            var products = _unitOfWork.ProductRepository.Get(
                filter: p => queryObject.SearchText == null || p.ProductName.Contains(queryObject.SearchText)); // ToListAsync for better query execution

            var productResponses = new List<ProductResponse>();
            foreach (var product in products)
            {
                var productResponse = _mapper.Map<ProductResponse>(product); // Map basic properties

                // Discount calculation logic
                var discount = _unitOfWork.DiscountRepository.GetByID(product.DiscountId); // Fetch discount

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

        public async Task<ProductResponse> GetProductById(long id)
        {
            try
            {
                var product = _unitOfWork.ProductRepository.Get(filter: p => p.Id == id).FirstOrDefault();

                if (product == null)
                {
                    throw new CustomException.DataNotFoundException("Product not found");
                }
                if (product.DiscountId != null && product.Discount != null)
                {
                    var discount = product.Discount;

                    if (discount.ExpiredDate >= DateTime.Now && discount.Status)
                    {
                        var discountedPrice = product.ProductPrice * (1 - discount.Percent / 100);
                        product.ProductPrice = Math.Round(discountedPrice, 3);
                    }
                }
                var productResponse = _mapper.Map<ProductResponse>(product);
                return productResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProductResponse> CreateProduct(CreateProductRequest createProductRequest)
        {
            var existingProduct = _unitOfWork.ProductRepository.Get().FirstOrDefault(p => p.ProductName.ToLower() == createProductRequest.ProductName.ToLower());

            if (existingProduct != null)
            {
                throw new ArgumentException($"Product with name '{createProductRequest.ProductName}' already exists.");
            }
            var admin = _unitOfWork.AdminRepository.GetByID(createProductRequest.AdminId);
            if (admin == null)
            {
                throw new ArgumentException("Admin not found.");
            }

            var discount = _unitOfWork.DiscountRepository.GetByID(createProductRequest.DiscountId);
            if (discount == null)
            {
                throw new ArgumentException("Discount not found.");
            }

            var category = _unitOfWork.CategoryRepository.GetByID(createProductRequest.CategoryId);
            if (category == null)
            {
                throw new ArgumentException("Category not found.");
            }

            var newProduct = _mapper.Map<Product>(createProductRequest);
            newProduct.CreateDate = DateTime.UtcNow;

            newProduct.Admin = admin;
            newProduct.Discount = discount;
            newProduct.Category = category;

            _unitOfWork.ProductRepository.Insert(newProduct);
            _unitOfWork.Save();

            var productResponse = _mapper.Map<ProductResponse>(newProduct);

            return productResponse;
        }

        public async Task<ProductResponse> UpdateProduct(long id, UpdateProductRequest updateProductRequest)
        {
            // 1. Fetch the Existing Product
            var existingProduct = _unitOfWork.ProductRepository.GetByID(id); // Use async variant

            if (existingProduct == null)
            {
                throw new ArgumentException($"Product with ID {id} not found."); // Use a more specific exception type
            }

            // 2. Check for Duplicate Product Name
            if (existingProduct.ProductName.ToLower() != updateProductRequest.ProductName.ToLower())
            {
                var duplicateExists = _unitOfWork.ProductRepository.Get((p => p.Id != id
                                                                                      && p.ProductName.ToLower() == updateProductRequest.ProductName.ToLower()));

                if (duplicateExists == null)
                {
                    throw new ArgumentException($"Product with name '{updateProductRequest.ProductName}' already exists.");
                }
            }

            // 3. Update Product Properties
            _mapper.Map(updateProductRequest, existingProduct);
            existingProduct.ModifiedDate = DateTime.Now;

            // Discount calculation logic
            var productResponse = CalculateDiscountedPrice(existingProduct); // Centralize discount logic

            // 4. Save Changes and Map the Response
            _unitOfWork.Save(); // Use the async version of Save

            _mapper.Map(existingProduct, productResponse); // Update response with final product state
            return productResponse;
        }

        // Centralized Discount Calculation
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

        public async Task<bool> DeleteProduct(long id)
        {
            try
            {
                var product = _unitOfWork.ProductRepository.GetByID(id);
                if (product == null)
                {
                    throw new Exception("Product not found.");
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
