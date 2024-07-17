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
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DiscountService (IConfiguration configuration, IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task<IEnumerable<Discount>> GetAllDiscount()
        {
            var discount = _unitOfWork.DiscountRepository.Get(d => d.Status == true,
                pageIndex: 1,
                pageSize: 5);
            return discount;
        }

        public async Task<IEnumerable<Discount>> GetAllDiscountFalse()
        {
            var discount = _unitOfWork.DiscountRepository.Get(d => d.Status == false,
                pageIndex: 1,
                pageSize: 5);
            return discount;
        }

        public async Task<Discount> GetDiscountById(long id)
        {


            var discount = _unitOfWork.DiscountRepository.GetByID(id);
            return discount;
        }

        public async Task<DiscountResponse> CreateDiscount(CreateDiscountRequest createDiscountRequest)
        {

            var discounts = _mapper.Map<Discount>(createDiscountRequest);

            // set trạng thái luôn true
            discounts.Status = true;
            _unitOfWork.DiscountRepository.Insert(discounts);

            //map lại với cái response 
            DiscountResponse discountResponse = _mapper.Map<DiscountResponse>(discounts);
            return discountResponse;
        }

        public async Task<DiscountResponse> UpdateDiscount(long id, UpdateDiscountRequest updateDiscountRequest)
        {

            var existdiscount = _unitOfWork.DiscountRepository.GetByID(id);
            if (existdiscount == null)
            {
                throw new Exception("Discount ID is not exist");
            }
            //map với cái biến đang có giá trị id
            _mapper.Map(updateDiscountRequest, existdiscount);

            _unitOfWork.DiscountRepository.Update(existdiscount);
            _unitOfWork.Save();
            var discountReponse = _mapper.Map<DiscountResponse>(existdiscount);
            return discountReponse;
        }

        public async Task<DiscountResponse> DeleteDiscount(long id)
        {

            var deleteDiscount = _unitOfWork.DiscountRepository.GetByID(id);
            if (deleteDiscount == null)
            {
                throw new Exception("Discount ID is not exist");
            }

            deleteDiscount.Status = false;
            _unitOfWork.DiscountRepository.Update(deleteDiscount);
            _unitOfWork.Save();

            //map vào giá trị response
            var discountResponse = _mapper.Map<DiscountResponse>(deleteDiscount);
            return discountResponse;
        }
    }
}
