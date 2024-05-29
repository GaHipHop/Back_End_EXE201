using GaHipHop_Model.DTO.Request;
using GaHipHop_Model.DTO.Response;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaHipHop_Service.Interfaces
{
    public interface IKindService
    {
        Task<KindResponse> CreateKind(KindRequest kindRequest);
        Task<KindResponse> UpdateKind(long id, KindRequest kindRequest);
        Task<bool> DeleteKind(long id);
    }
}
