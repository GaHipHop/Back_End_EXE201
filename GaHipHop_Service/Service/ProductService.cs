using AutoMapper;
using GaHipHop_Model.DTO.Request;
using GaHipHop_Model.DTO.Response;
using GaHipHop_Repository.Entity;
using GaHipHop_Repository.Repository;
using GaHipHop_Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<ProductResponse> CreateProduct(ProductRequest productRequest)
        {
            try
            {
                bool productnameExists = _unitOfWork.ProductRepository.Exists(a => a.ProductName == productRequest.ProductName);
                if (productnameExists)
                {
                    return new ProductResponse
                    {
                        ProductName = "ProductName already exists.",
                        Status = false
                    };      
                }

                var product = _mapper.Map<Product>(productRequest);

                product.Status = true;

                _unitOfWork.ProductRepository.Insert(product);
                _unitOfWork.Save();

                var productResponse = _mapper.Map<ProductResponse>(product);
                return productResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

        public IEnumerable<ProductResponse> GetAllProduct()
        {
            var listProduct = _unitOfWork.ProductRepository.Get(
                filter: s => s.Status == true,
                includeProperties: "Admin, Discount, Category"
                ).ToList();
            var productResponse = _mapper.Map<IEnumerable<ProductResponse>>(listProduct);
            return productResponse;
        }

        public async Task<ProductResponse> GetProductById(long id)
        {
            try
            {
                var product = _unitOfWork.ProductRepository.Get(
                    filter: p => p.Id == id && p.Status == true, includeProperties: "Admin, Discount, Category"
                ).FirstOrDefault();

                if (product == null)
                {
                    throw new Exception("Product not found");
                }

                var productResponse = _mapper.Map<ProductResponse>(product);
                return productResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProductResponse> UpdateProduct(long id, ProductRequest productRequest)
        {
            try
            {
                var existingProduct = _unitOfWork.ProductRepository.GetByID(id);

                if (existingProduct == null)
                {
                    throw new Exception("Product not found.");
                }

                _mapper.Map(productRequest, existingProduct);

                _unitOfWork.ProductRepository.Update(existingProduct);
                _unitOfWork.Save();

                var productResponse = _mapper.Map<ProductResponse>(existingProduct);
                return productResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }   
        }
    }
}
