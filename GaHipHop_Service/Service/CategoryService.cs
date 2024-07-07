using AutoMapper;
using GaHipHop_Model.DTO.Request;
using GaHipHop_Model.DTO.Response;
using GaHipHop_Repository.Entity;
using GaHipHop_Repository.Repository;
using GaHipHop_Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace GaHipHop_Service.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoryService(IConfiguration configuration, IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task<CategoryResponse> CreateCategory(CategoryRequest categoryRequest)
        {

            var existingCategory = _unitOfWork.CategoryRepository.Get().FirstOrDefault(p => 
                                            p.CategoryName.ToLower() == categoryRequest.CategoryName.ToLower());

            if (existingCategory != null)
            {
                throw new CustomException.DataExistException($"Kind with ColorName '{categoryRequest.CategoryName}' already exists.");
            }

            var categoryResponse = _mapper.Map<CategoryResponse>(existingCategory);
            var newCategory = _mapper.Map<Category>(categoryRequest);
            newCategory.Status = true;
            _unitOfWork.CategoryRepository.Insert(newCategory);
            _unitOfWork.Save();

            _mapper.Map(newCategory, categoryResponse);
            return categoryResponse;

        }

        public async Task<bool> DeleteCategory(long id)
        {
            try
            {
                var category = _unitOfWork.CategoryRepository.GetByID(id);
                if (category == null)
                {
                    throw new CustomException.DataNotFoundException("Category not found.");
                }
                
                var products = _unitOfWork.ProductRepository.Get()
                                .Where(p => p.CategoryId == category.Id && p.Status == true);
                if (products.Any())
                {
                    throw new CustomException.DataExistException("Product still exist in Category");
                }

                category.Status = false;
                _unitOfWork.CategoryRepository.Update(category);
                _unitOfWork.Save();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<CategoryResponse>> GetAllCategory(QueryObject queryObject)
        {
            var categories = _unitOfWork.CategoryRepository.Get(
                filter: p => queryObject.SearchText == null || p.CategoryName.Contains(queryObject.SearchText),
                pageIndex: 1, 
                pageSize: 5)
                .Where(k => k.Status == true)
                .ToList();

            if (!categories.Any())
            {
                throw new CustomException.DataNotFoundException("No Category in Database");
            }

            var categoryResponses = _mapper.Map<List<CategoryResponse>>(categories);

            return categoryResponses;
        }

        public async Task<List<CategoryResponse>> GetAllCategoryFalse(QueryObject queryObject)
        {


            var categories = _unitOfWork.CategoryRepository.Get(
                filter: p => queryObject.SearchText == null || p.CategoryName.Contains(queryObject.SearchText),
                pageIndex: 1,
                pageSize: 5)
                .Where(k => k.Status == false)
                .ToList();
            if (!categories.Any())
            {
                throw new CustomException.DataNotFoundException("No Category False in Database");
            }

            var categoryResponses = _mapper.Map<List<CategoryResponse>>(categories);

            return categoryResponses;
        }

        public async Task<CategoryResponse> GetCategoryById(long id)
        {
            var category = _unitOfWork.CategoryRepository.Get(filter: p => p.Id == id).FirstOrDefault();

            if (category == null)
            {
                throw new CustomException.DataNotFoundException("Category not found");
            }

            var categoryResponse = _mapper.Map<CategoryResponse>(category);
            return categoryResponse;
        }

        public async Task<CategoryResponse> UpdateCategory(long id, CategoryRequest categoryRequest)
        {

            var existingCategory = _unitOfWork.CategoryRepository.GetByID(id);

            if (existingCategory == null)
            {
                throw new CustomException.DataNotFoundException($"Category with ID {id} not found.");
            }

            if (!existingCategory.Status)
            {
                throw new CustomException.InvalidDataException($"Category with ID {id} was DeActive.");
            }

            var duplicateExists = _unitOfWork.CategoryRepository.Exists(p =>
                p.Id != id &&
                p.CategoryName.ToLower() == categoryRequest.CategoryName.ToLower()
            );

            if (duplicateExists)
            {
                throw new CustomException.DataExistException($"Color with name '{categoryRequest.CategoryName}' already exists for this product.");
            }
            _mapper.Map(categoryRequest, existingCategory);
            _unitOfWork.Save();

            var categoryResponse = _mapper.Map<CategoryResponse>(existingCategory);
            return categoryResponse;
        }
    }
}
