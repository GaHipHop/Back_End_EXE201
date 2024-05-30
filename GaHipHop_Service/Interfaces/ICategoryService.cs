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
    public interface ICategoryService
    {
        Task<List<CategoryResponse>> GetAllCategory(QueryObject queryObject);
        Task<List<CategoryResponse>> GetAllCategoryFalse(QueryObject queryObject);
        Task<CategoryResponse> GetCategoryById(long id);
        Task<CategoryResponse> CreateCategory(CategoryRequest categoryRequest);
        Task<CategoryResponse> UpdateCategory(long id, CategoryRequest categoryRequest);
        Task<bool> DeleteCategory(long id);
    }
}
