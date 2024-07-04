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
    public interface IContactService
    {
        Task<ContactResponse> CreateContact(CreateContactRequest createContactRequest);
        Task<ContactResponse> DeleteContact(long id);
        Task<IEnumerable<Contact>> GetAllContacts();
        Task<Contact> GetContactById(long id);
        Task<ContactResponse> UpdateContact(long id,UpdateContactRequest updateContactRequest);
    }
}
