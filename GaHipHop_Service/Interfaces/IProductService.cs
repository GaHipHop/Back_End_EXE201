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
        Task<List<ProductAnyKindResponse>> GetAllProductFalse(QueryObject queryObject);
        Task<List<ProductResponse>> GetAllProductByCategoryId(long id);
        /*Task<ProductResponse> CreateProduct(ProductRequest productRequest);*/
        Task<ProductAnyKindResponse> CreateProduct(ProductRequest productRequest);
        Task<ProductResponse> UpdateProduct(long id, ProductRequest productRequest);
        Task<bool> DeleteProduct(long id);
        Task<ProductAnyKindResponse> GetProductById(long id);
    }
}
