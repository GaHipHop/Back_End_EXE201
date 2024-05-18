﻿using AutoMapper;
using GaHipHop_Model.DTO.Request;
using GaHipHop_Model.DTO.Respone;
using GaHipHop_Repository;
using GaHipHop_Repository.Entity;
using GaHipHop_Repository.Repository;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ContactService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Contact>> getAllContacts()
        {
            return null;
            //return await _unitOfWork.ContactRepository.Get();
        }
        public async Task<ContactReponse> CreateContact(CreateContactRequest createContactRequest)
        {
            var createcontact =  _mapper.Map<Contact>(createContactRequest);

             _unitOfWork.ContactRepository.Insert(createcontact);

            ContactReponse createContactReponse = _mapper.Map<ContactReponse>(createcontact);
            return createContactReponse;
        }
    }
}
