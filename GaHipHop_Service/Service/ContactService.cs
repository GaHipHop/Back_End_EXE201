using AutoMapper;
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

        public async Task<IEnumerable<Contact>> GetAllContacts()
        {
            
            return _unitOfWork.ContactRepository.Get();
        }

        public async Task<Contact> GetContactById(long id)
        {
            return _unitOfWork.ContactRepository.GetByID(id);
        }

        public async Task<ContactReponse> CreateContact(CreateContactRequest createContactRequest)
        {
            var createcontact =  _mapper.Map<Contact>(createContactRequest);

             _unitOfWork.ContactRepository.Insert(createcontact);

            ContactReponse createContactReponse = _mapper.Map<ContactReponse>(createcontact);
            return createContactReponse;
        }

        public async Task<ContactReponse> UpdateContact(long id, UpdateContactRequest updateContactRequest)
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
            var contactresponse = _mapper.Map<ContactReponse>(existcontact);
            return contactresponse;
        }

        public async Task<ContactReponse> DeleteContact(long id)
        {
            var deletesubcription = _unitOfWork.ContactRepository.GetByID(id);
            if (deletesubcription == null)
            {
                throw new Exception("Subcription ID is not exist");
            }
            _unitOfWork.ContactRepository.Delete(deletesubcription);

            //map vào giá trị response
            var contactresponse = _mapper.Map<ContactReponse>(deletesubcription);
            return contactresponse;
        }
    }
}
