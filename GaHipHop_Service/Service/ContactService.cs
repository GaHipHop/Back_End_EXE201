using AutoMapper;
using GaHipHop_Model.DTO.Request;
using GaHipHop_Model.DTO.Response;
using GaHipHop_Repository;
using GaHipHop_Repository.Entity;
using GaHipHop_Repository.Repository;
using GaHipHop_Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace GaHipHop_Service.Service
{
    public class ContactService : IContactService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ContactService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<Contact>> GetAllContacts()
        {
            
            return _unitOfWork.ContactRepository.Get();
        }

        public async Task<Contact> GetContactById(long id)
        {
            return _unitOfWork.ContactRepository.GetByID(id);
        }

        public async Task<ContactResponse> CreateContact(CreateContactRequest createContactRequest)
        {

            var createcontact =  _mapper.Map<Contact>(createContactRequest);

             _unitOfWork.ContactRepository.Insert(createcontact);

            ContactResponse createContactReponse = _mapper.Map<ContactResponse>(createcontact);
            return createContactReponse;
        }

        public async Task<ContactResponse> UpdateContact(long id, UpdateContactRequest updateContactRequest)
        {

            var existcontact = _unitOfWork.ContactRepository.GetByID(id);
            if (existcontact == null)
            {
                throw new Exception("ID isn't exist");
            }
            //map với cái biến đang có giá trị id
            _mapper.Map(updateContactRequest, existcontact);

            _unitOfWork.ContactRepository.Update(existcontact);
            _unitOfWork.Save();
            var contactresponse = _mapper.Map<ContactResponse>(existcontact);
            return contactresponse;
        }

        public async Task<ContactResponse> DeleteContact(long id)
        {

            var deletesubcription = _unitOfWork.ContactRepository.GetByID(id);
            if (deletesubcription == null)
            {
                throw new Exception("Subcription ID is not exist");
            }
            _unitOfWork.ContactRepository.Delete(deletesubcription);

            //map vào giá trị response
            var contactresponse = _mapper.Map<ContactResponse>(deletesubcription);
            return contactresponse;
        }
    }
}
