using AutoMapper;
using GaHipHop_Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GaHipHop_API.Controllers.Contact
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        public ContactController(IContactService contactService) { 
            _contactService = contactService;
        }

        [HttpGet("getAllContacts")]
        public async Task<IActionResult> getAllContacts()
        {
            try
            {
                var contacts = await _contactService.getAllContacts();
                if(contacts == null || !contacts.Any())
                {   
                    return NotFound();
                }
                return Ok(contacts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
