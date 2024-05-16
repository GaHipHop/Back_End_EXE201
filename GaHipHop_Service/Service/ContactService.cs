using GaHipHop_Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaHipHop_Service.Service
{
    public class ContactService : IContactService
    {
        private readonly IContactService _contactService;
        public ContactService(IContactService contactService)
        {
            _contactService = contactService;
        }
    }
}
