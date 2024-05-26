using GaHipHop_Model.DTO.Request;
using GaHipHop_Model.DTO.Response;
using GaHipHop_Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaHipHop_Service.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductResponse>> GetAllProduct(QueryObject queryObject);
        Task<ProductResponse> CreateProduct(CreateProductRequest createProductRequest);
        Task<ProductResponse> UpdateProduct(long id, UpdateProductRequest updateProductRequest);
        Task<bool> DeleteProduct(long id);
        Task<ProductResponse> GetProductById(long id);
    }
}
