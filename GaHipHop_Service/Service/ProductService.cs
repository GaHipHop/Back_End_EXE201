using AutoMapper;
using GaHipHop_Model.DTO.Request;
using GaHipHop_Model.DTO.Response;
using GaHipHop_Repository.Entity;
using GaHipHop_Repository.Repository;
using GaHipHop_Service.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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

        public async Task<IEnumerable<Product>> GetAllProduct()
        {
            // Fetch products with related entities
            var products = _unitOfWork.ProductRepository.Get(
                includeProperties: "Admin,Discount,Category"  // Eager load related entities
            );

            return await Task.FromResult(products);
        }

        public async Task<ProductResponse> GetProductById(long id)
        {
            try
            {
                var product = _unitOfWork.ProductRepository.Get(
                    filter: p => p.Id == id, includeProperties: "Admin,Discount,Category"
                    ).FirstOrDefault();

                if (product == null)
                {
                    throw new CustomException.DataNotFoundException("Product not found");
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
            var existingProduct = _unitOfWork.ProductRepository.Get(filter: p => p.Id == id, includeProperties: "Admin,Discount,Category").FirstOrDefault();
            if (existingProduct == null)
            {
                throw new ArgumentException($"Product with ID {id} not found.");
            }
            existingProduct = _unitOfWork.ProductRepository.Get().FirstOrDefault(p => p.ProductName.ToLower() == updateProductRequest.ProductName.ToLower());

            if (existingProduct != null)
            {
                throw new ArgumentException($"Product with name '{updateProductRequest.ProductName}' already exists.");
            }
            _mapper.Map(updateProductRequest, existingProduct);

            existingProduct.ModifiedDate = DateTime.UtcNow;

            _unitOfWork.Save();

            var productResponse = _mapper.Map<ProductResponse>(existingProduct);
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
