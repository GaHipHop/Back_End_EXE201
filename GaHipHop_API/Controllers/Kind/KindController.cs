using AutoMapper;
using CoreApiResponse;
using GaHipHop_Model.DTO.Request;
using GaHipHop_Model.DTO.Response;
using GaHipHop_Repository.Repository;
using GaHipHop_Service.Interfaces;
using GaHipHop_Service.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tools;

namespace GaHipHop_API.Controllers.Kind
{
    [Route("api/[controller]")]
    [ApiController]
    public class KindController : BaseController
    {
        private readonly IKindService _kindService;

        public KindController(IKindService kindService)
        {
            _kindService = kindService;
        }

        [HttpPost("CreateKind")]
        public async Task<IActionResult> CreateKind([FromForm] KindRequest kindRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return CustomResult(ModelState, HttpStatusCode.BadRequest);
                }

                KindResponse kind = await _kindService.CreateKind(kindRequest);
                return CustomResult("Create Successful", kind, HttpStatusCode.OK);
            }
            catch (CustomException.DataNotFoundException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }

        }

        [HttpPatch("UpdateKind/{id}")]
        public async Task<IActionResult> UpdateKind(long id, [FromForm] KindRequest kindRequest)
        {
            try
            {
                KindResponse kind = await _kindService.UpdateKind(id, kindRequest);
                return CustomResult("Update Sucessfully", kind, HttpStatusCode.OK);
            }
            catch (CustomException.DataNotFoundException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.NotFound);
            }
            catch (CustomException.DataExistException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.Conflict);
            }
            catch (CustomException.InvalidDataException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("DeleteKind/{id}")]
        public async Task<IActionResult> DeleteKind(long id)
        {
            try
            {
                var kind = await _kindService.DeleteKind(id);
                return CustomResult("Delete Kind Successfull (Status)", kind, HttpStatusCode.OK);
            }
            catch (CustomException.DataNotFoundException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }

        }

    }
}
