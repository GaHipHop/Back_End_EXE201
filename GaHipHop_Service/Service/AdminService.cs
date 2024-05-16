﻿using AutoMapper;
using GaHipHop_Model.DTO.Request;
using GaHipHop_Model.DTO.Respone;
using GaHipHop_Repository;
using GaHipHop_Repository.Entity;
using GaHipHop_Service.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaHipHop_Service.Service
{
    public class AdminService : IAdminService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AdminService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
                    throw new Exception("Admin not found");
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
                    throw new Exception("Admin not found");
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



    }
}