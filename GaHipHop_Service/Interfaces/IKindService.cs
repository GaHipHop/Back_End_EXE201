using GaHipHop_Model.DTO.Request;
using GaHipHop_Model.DTO.Response;
using GaHipHop_Repository.Entity;
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
        Task<List<KindResponse>> GetAllKind(QueryObject queryObject);
        Task<List<KindResponse>> GetAllKindFalse(QueryObject queryObject);
        Task<KindResponse> GetKindById(long id);
        Task<List<KindResponse>> GetAllKindByProductId(long id);
        Task<KindResponse> CreateKind(KindRequest kindRequest);
        Task<KindResponse> UpdateKind(long id, UpdateKindRequest updateKindRequest);
        Task<bool> DeleteKind(long id);
    }
}
