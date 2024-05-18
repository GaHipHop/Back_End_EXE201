using AutoMapper;
using CoreApiResponse;
using GaHipHop_Model.DTO.Request;
using GaHipHop_Model.DTO.Respone;
using GaHipHop_Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GaHipHop_API.Controllers.Contact
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : BaseController
    {
        private readonly IContactService _contactService;
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet("getAllContacts")]
        public async Task<IActionResult> getAllContacts()
        {
            try
            {
                var contacts = await _contactService.getAllContacts();
                if (contacts == null)
                {
                    return CustomResult("This ID isn't exist", HttpStatusCode.NotFound);
                }
                return CustomResult("Load Successfull", contacts, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact([FromBody] CreateContactRequest createContactRequest)
        {
            try
            {
                ContactReponse createcontact = await _contactService.CreateContact(createContactRequest);
                return CustomResult("Create Successfull", createcontact, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }

        }


    }
}
