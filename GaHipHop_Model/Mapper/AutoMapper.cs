using AutoMapper;
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
            //CreateMap<BidaClubRequest, BidaClub>().ReverseMap();

            //Reponse
            //CreateMap<BidaClubReponse, BidaClub>().ReverseMap();
        }
    }
}