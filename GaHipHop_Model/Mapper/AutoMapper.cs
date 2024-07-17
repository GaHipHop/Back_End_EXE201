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
            CreateMap<UpdateDiscountRequest, Discount>().ReverseMap();
            CreateMap<ProductRequest, Product>().ReverseMap();
            CreateMap<UpdateProductRequest, Product>().ReverseMap();
            CreateMap<KindRequest, Kind>().ReverseMap();
            CreateMap<ProductRequest, Kind>().ReverseMap();
            CreateMap<UpdateKindRequest, Kind>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null && !srcMember.Equals(0)));
            CreateMap<CategoryRequest, Category>().ReverseMap();

            CreateMap<OrderRequest, Order>().ReverseMap();

            //Reponse
            CreateMap<Admin, AdminResponse>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
            /*CreateMap<Admin, AdminResponse>().ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role != null ? new RoleResponse { Id = src.Role.Id, RoleName = src.Role.RoleName } : null));*/
            CreateMap<Role, RoleResponse>();
            CreateMap<Admin, LoginResponse>();
            CreateMap<Product, ProductResponse>()
                .ForMember(dest => dest.Percent, opt => opt.MapFrom(src => src.Discount.Percent))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Kind.FirstOrDefault().Image))
                .ForMember(dest => dest.ColorName, opt => opt.MapFrom(src => src.Kind.FirstOrDefault().ColorName));

            CreateMap<Product, ProductAnyKindResponse>()
                .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
                .ForMember(dest => dest.Kinds, opt => opt.MapFrom(src => src.Kind))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ReverseMap();
            CreateMap<Discount,  DiscountResponse>();
            CreateMap<Kind, KindResponse>();
            CreateMap<Category, CategoryResponse>();

            CreateMap<Order, OrderResponse>().ReverseMap();
            CreateMap<OrderDetails, OrderDetailResponse>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Kind.Product.ProductName))
                .ForMember(dest => dest.ColorName, opt => opt.MapFrom(src => src.Kind.ColorName))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Kind.Image))
                .ReverseMap();
            CreateMap<CartItem, OrderDetails>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<UserInfo, UserInfoResponse>().ReverseMap();
            CreateMap<Contact, ContactResponse>().ReverseMap();
        }
    }
}