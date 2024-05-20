using AutoMapper;
using GaHipHop_Model.DTO.Request;
using GaHipHop_Model.DTO.Response;
using GaHipHop_Repository.Entity;
using GaHipHop_Repository.Repository;
using GaHipHop_Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GaHipHop_Service.Service
{
    public class AdminService : IAdminService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AdminService(IConfiguration configuration, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<(string Token, LoginResponse loginResponse)> AuthorizeUser(LoginRequest loginRequest)
        {
            var member = _unitOfWork.AdminRepository
                .Get(filter: a => a.Username == loginRequest.UserName && a.Password == loginRequest.Password && a.Status == true).FirstOrDefault();
            if (member != null)
            {
                string token = GenerateToken(member);
                var adminResponse = _mapper.Map<LoginResponse>(member);
                return (token, adminResponse);
            }
            return (null, null);
        }



        public IEnumerable<AdminResponse> GetAllAdmin()
        {
            var listAdmin = _unitOfWork.AdminRepository.Get(
                filter: s => s.Status == true && s.RoleId == 1,
                includeProperties: "Role"
            ).ToList();
            var adminResponses = _mapper.Map<IEnumerable<AdminResponse>>(listAdmin);
            return adminResponses;
        }


        public async Task<AdminResponse> GetAdminById(long id)
        {
            try
            {
                var admin = _unitOfWork.AdminRepository.Get(
                    filter: a => a.Id == id && a.Status == true && a.RoleId == 1, includeProperties: "Role"
                ).FirstOrDefault();

                if (admin == null)
                {
                    throw new Exception("Admin not found");
                }

                var adminResponse = _mapper.Map<AdminResponse>(admin);
                return adminResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public async Task<AdminResponse> CreateAdmin(AdminRequest adminRequest)
        {
            try
            {
                bool usernameExists = _unitOfWork.AdminRepository.Exists(a => a.Username == adminRequest.UserName);
                if (usernameExists)
                {
                    return new AdminResponse
                    {
                        UserName = "Username already exists.",
                        Status = false
                    };
                }

                var admin = _mapper.Map<Admin>(adminRequest);

                admin.Status = true;
                admin.RoleId = 1;

                _unitOfWork.AdminRepository.Insert(admin);
                _unitOfWork.Save();

                var adminResponse = _mapper.Map<AdminResponse>(admin);
                return adminResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<AdminResponse> UpdateAdmin(long id, AdminRequest adminRequest)
        {
            try
            {
                var existingAdmin = _unitOfWork.AdminRepository.GetByID(id);

                if (existingAdmin == null)
                {
                    throw new Exception("Admin not found.");
                }

                _mapper.Map(adminRequest, existingAdmin);

                _unitOfWork.AdminRepository.Update(existingAdmin);
                _unitOfWork.Save();

                var adminResponse = _mapper.Map<AdminResponse>(existingAdmin);
                return adminResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteAdmin(long id)
        {
            try
            {
                var admin = _unitOfWork.AdminRepository.GetByID(id);
                if (admin == null)
                {
                    throw new Exception("Admin not found.");
                }

                admin.Status = false;
                _unitOfWork.AdminRepository.Update(admin);
                _unitOfWork.Save();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string GenerateToken(Admin info)
        {
            List<Claim> claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sub, info.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
            new Claim("name", info.Username),
        };

            if (info.RoleId != 0)
            {
                var role = _unitOfWork.RoleRepository.Get(filter: r => r.Id == info.RoleId).FirstOrDefault();
                claims.Add(new Claim("role", role.RoleName));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }


    }
}
