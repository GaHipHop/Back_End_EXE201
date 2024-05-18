using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GaHipHop_Model.DTO.Request;
using GaHipHop_Model.DTO.Response;
using GaHipHop_Repository.Entity;

namespace GaHipHop_Service.Interfaces
{
    public interface IAdminService
    {
        Task<(string Token, LoginResponse loginResponse)> AuthorizeUser(LoginRequest loginRequest);
        IEnumerable<AdminResponse> GetAllAdmin();

        Task<AdminResponse> CreateAdmin(AdminRequest adminRequest);
        Task<AdminResponse> UpdateAdmin(long id, AdminRequest adminRequest);
        Task<bool> DeleteAdmin(long adminId);
        Task<AdminResponse> GetAdminById(long id);
    }
}
