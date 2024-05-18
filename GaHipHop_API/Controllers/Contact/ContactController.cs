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
        public async Task<IActionResult> GetAllContacts()
        {
            try
            {
                var contacts = await _contactService.GetAllContacts();
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactById(long id)
        {
            try
            {
                var contact = await _contactService.GetContactById(id);
                if (contact == null)
                {
                    return CustomResult("Id is not exist", contact, HttpStatusCode.NotFound);
                }
                return CustomResult("ID found: ", contact, HttpStatusCode.OK);
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

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateContact(long id, [FromBody] UpdateContactRequest updateContactRequest)
        {
            try
            {
                ContactReponse subcription = await _contactService.UpdateContact(id, updateContactRequest);
                return CustomResult("Create Successfull", subcription, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(long id)
        {
            try
            {
                var deletecontact = await _contactService.DeleteContact(id);
                return CustomResult("Delete Successfull (Xóa luôn)", DeleteContact, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

    }
}
