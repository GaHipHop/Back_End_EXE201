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
    public interface IDiscountService
    {
        Task<DiscountResponse> CreateDiscount(CreateDiscountRequest createDiscountRequest);
        Task<DiscountResponse> DeleteDiscount(long id);
        Task<IEnumerable<Discount>> GetAllDiscount();
        Task<IEnumerable<Discount>> GetAllDiscountFalse();
        Task<Discount> GetDiscountById(long id);
        Task<DiscountResponse> UpdateDiscount(long id, UpdateDiscountRequest updateDiscountRequest);
    }
}
