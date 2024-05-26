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
            CreateMap<CreateContactRequest, Contact>().ReverseMap();
            CreateMap<UpdateContactRequest, Contact>().ReverseMap();
            CreateMap<CreateDiscountRequest, Discount>().ReverseMap();
            CreateMap<UpdateContactRequest, Discount>().ReverseMap();
            CreateMap<CreateProductRequest, Product>().ReverseMap();
            CreateMap<UpdateProductRequest, Product>().ReverseMap();
            CreateMap<KindRequest, Kind>().ReverseMap();

            //Reponse
            CreateMap<Admin, AdminResponse>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
            /*CreateMap<Admin, AdminResponse>().ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role != null ? new RoleResponse { Id = src.Role.Id, RoleName = src.Role.RoleName } : null));*/
            CreateMap<Role, RoleResponse>();
            CreateMap<Admin, LoginResponse>();
            CreateMap<Product, ProductResponse>()
            .ForMember(dest => dest.Admin, opt => opt.MapFrom(src => src.Admin))
            .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));
            CreateMap<Kind, KindResponse>();

        }
    }
}