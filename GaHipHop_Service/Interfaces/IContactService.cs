﻿using GaHipHop_Model.DTO.Request;
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
        Task<ContactReponse> DeleteContact(long id);
        Task<IEnumerable<Contact>> GetAllContacts();
        Task<Contact> GetContactById(long id);
        Task<ContactReponse> UpdateContact(long id,UpdateContactRequest updateContactRequest);
    }
}
