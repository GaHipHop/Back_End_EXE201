using GaHipHop_Model.DTO.Request;
using GaHipHop_Model.DTO.Respone;
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
        Task<ContactReponse> CreateContact(CreateContactRequest createContactRequest);
        Task<IEnumerable<Contact>> getAllContacts();
    }
}
