using CoreApiResponse;
using GaHipHop_Model.DTO.Request;
using GaHipHop_Model.DTO.Response;
using GaHipHop_Service.Interfaces;
using GaHipHop_Service.Service;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GaHipHop_API.Controllers.Discont
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : BaseController
    {
        private readonly IDiscountService _discountService;

        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpGet("GetAllDiscount")]
        public async Task<IActionResult> GetAllDiscount()
        {
            try
            {
                var discount = await _discountService.GetAllDiscount();
                return CustomResult("Load Successfull", discount, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("GetDiscountBy/{id}")]
        public async Task<IActionResult> GetDiscountById(long id)
        {
            try
            {
                var discount = await _discountService.GetContactById(id);
                if (discount == null)
                {
                    return CustomResult("Id is not exist", discount, HttpStatusCode.NotFound);
                }
                return CustomResult("ID found: ", discount, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("CreateDiscount")]
        public async Task<IActionResult> CreateDiscount([FromBody] CreateDiscountRequest createDiscountRequest)
        {
            try
            {
                DiscountResponse discount = await _discountService.CreateDiscount(createDiscountRequest);
                return CustomResult("Created Successful", discount, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPatch("UpdateDiscount/{id}")]
        public async Task<IActionResult> UpdateDiscount(long id, [FromBody] UpdateDiscountRequest updateDiscountRequest)
        {
            try
            {
                DiscountResponse subcription = await _discountService.UpdateDiscount(id, updateDiscountRequest);
                return CustomResult("updated Successful", subcription, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("DeleteDiscount/{id}")]
        public async Task<IActionResult> DeleteDiscount(long id)
        {
            try
            {
                var deletediscount = await _discountService.DeleteDiscount(id);
                return CustomResult("Delete Successfull (Status)", deletediscount, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
