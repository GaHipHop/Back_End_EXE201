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
    public class DiscountService : IDiscountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DiscountService (IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Discount>> GetAllDiscount()
        {
            var discount = _unitOfWork.DiscountRepository.Get();
            return discount;
        }

        public async Task<Discount> GetContactById(long id)
        {
            var discount = _unitOfWork.DiscountRepository.GetByID(id);
            return discount;
        }

        public async Task<DiscountResponse> CreateDiscount(CreateDiscountRequest createDiscountRequest)
        {
            var dícounts = _mapper.Map<Discount>(createDiscountRequest);

            // set trạng thái luôn true
            dícounts.Status = true;
            _unitOfWork.DiscountRepository.Insert(dícounts);

            //map lại với cái response 
            DiscountResponse discountResponse = _mapper.Map<DiscountResponse>(dícounts);
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
