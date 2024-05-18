using AutoMapper;
using GaHipHop_Model.DTO.Request;
using GaHipHop_Model.DTO.Response;
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
<<<<<<< HEAD
            CreateMap<ProductRequest, Product>().ReverseMap();
=======
            CreateMap<CreateContactRequest, CreateContactRequest>().ReverseMap();
>>>>>>> Dante

            //Reponse
            CreateMap<Admin, AdminResponse>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
<<<<<<< HEAD

            CreateMap<Role, RoleResponse>();
            /*CreateMap<Admin, AdminResponse>().ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role != null ? new RoleResponse { Id = src.Role.Id, RoleName = src.Role.RoleName } : null));*/

            CreateMap<Product, ProductResponse>();
=======
            CreateMap<ContactReponse, ContactReponse>().ReverseMap();
>>>>>>> Dante
        }
    }
}