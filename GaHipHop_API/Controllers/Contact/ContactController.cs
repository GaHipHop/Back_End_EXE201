﻿using AutoMapper;
using CoreApiResponse;
using GaHipHop_Model.DTO.Request;
using GaHipHop_Model.DTO.Response;
using GaHipHop_Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tools;

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
                return CustomResult("Load Successfull", contacts, HttpStatusCode.OK);
            }
            catch (CustomException.DataNotFoundException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.NotFound);
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

        [HttpGet("GetContactBy/{id}")]
        [Authorize(Roles = "Admin,Manager")]
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
            catch (CustomException.DataNotFoundException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.NotFound);
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


        [HttpPost("CreateContact")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> CreateContact([FromBody] CreateContactRequest createContactRequest)
        {
            try
            {
                ContactResponse createcontact = await _contactService.CreateContact(createContactRequest);
                return CustomResult("Create Successfull", createcontact, HttpStatusCode.OK);
            }
            catch (CustomException.ForbbidenException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.Forbidden);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPatch("UpdateContact/{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> UpdateContact(long id, [FromBody] UpdateContactRequest updateContactRequest)
        {
            try
            {
                ContactResponse subcription = await _contactService.UpdateContact(id, updateContactRequest);
                return CustomResult("Create Successfull", subcription, HttpStatusCode.OK);
            }
            catch (CustomException.ForbbidenException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.Forbidden);
            }
            catch (CustomException.DataNotFoundException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.NotFound);
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

        [HttpDelete("DeleteContact/{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> DeleteContact(long id)
        {
            try
            {
                var deletecontact = await _contactService.DeleteContact(id);
                return CustomResult("Delete Successfull (Xóa luôn)", DeleteContact, HttpStatusCode.OK);
            }
            catch (CustomException.ForbbidenException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.Forbidden);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

    }
}
