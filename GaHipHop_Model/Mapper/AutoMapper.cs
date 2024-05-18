using AutoMapper;
using GaHipHop_Model.DTO.Request;
using GaHipHop_Model.DTO.Respone;
using GaHipHop_Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GaHipHop_Model.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Request
            CreateMap<AdminRequest, Admin>().ReverseMap();
            CreateMap<CreateContactRequest, CreateContactRequest>().ReverseMap();

            //Reponse
            CreateMap<Admin, AdminResponse>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
            CreateMap<ContactReponse, ContactReponse>().ReverseMap();
        }
    }
}