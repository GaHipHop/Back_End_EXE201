using GaHipHop_Model.DTO.Request;
using GaHipHop_Model.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaHipHop_Service.Interfaces
{
    public interface IProductService
    {
        IEnumerable<ProductResponse> GetAllProduct();
        Task<ProductResponse> CreateProduct(ProductRequest productRequest);
        Task<ProductResponse> UpdateProduct(long id, ProductRequest productRequest);
        Task<bool> DeleteProduct(long id);
        Task<ProductResponse> GetProductById(long id);
    }
}
