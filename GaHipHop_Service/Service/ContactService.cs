using AutoMapper;
using GaHipHop_Repository;
using GaHipHop_Repository.Entity;
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
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ContactService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Contact>> getAllContacts()
        {
            return await _unitOfWork.ContactRepository.Get();
        }
    }
}
