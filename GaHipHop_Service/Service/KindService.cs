using AutoMapper;
using Firebase.Auth;
using Firebase.Storage;
using GaHipHop_Model.DTO.Request;
using GaHipHop_Model.DTO.Response;
using GaHipHop_Repository.Entity;
using GaHipHop_Repository.Repository;
using GaHipHop_Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data.Entity.Validation;
using System.Net.Sockets;
using System.Text;
using Tools;

namespace GaHipHop_Service.Service
{
    public class KindService : IKindService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly Tools.Firebase _firebase;

        public KindService(IConfiguration configuration, IUnitOfWork unitOfWork, IMapper mapper, Tools.Firebase firebase)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _firebase = firebase;
        }

        public async Task<KindResponse> CreateKind(KindRequest kindRequest)
        {
            try
            {
                var existingKind = _unitOfWork.KindRepository.Get().FirstOrDefault(p => p.ColorName.ToLower() == kindRequest.ColorName.ToLower());

                if (existingKind != null)
                {
                    throw new CustomException.DataExistException($"Kind with ColorName '{kindRequest.ColorName}' already exists.");
                }

                var product = _unitOfWork.ProductRepository.GetByID(kindRequest.ProductId);
                if (product == null)
                {
                    throw new CustomException.DataNotFoundException("Product not found.");
                }

                product.StockQuantity += kindRequest.Quantity;

                var kindResponse = _mapper.Map<KindResponse>(existingKind);
                var newKind = _mapper.Map<Kind>(kindRequest);
                newKind.Status = true;

                _unitOfWork.KindRepository.Insert(newKind);
                _unitOfWork.Save(); // Lưu thay đổi không đồng bộ

                _mapper.Map(newKind, kindResponse);
                return kindResponse;
            }
            catch (DbUpdateException dbEx)
            {
                // Xử lý lỗi cập nhật cơ sở dữ liệu
                throw new Exception("There was a problem updating the database. " + dbEx.Message, dbEx);
            }
            catch (DbEntityValidationException valEx)
            {
                // Xử lý lỗi xác thực thực thể
                var errorMessages = valEx.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);
                var exceptionMessage = string.Concat(valEx.Message, " The validation errors are: ", fullErrorMessage);
                throw new Exception(exceptionMessage, valEx);
            }
            catch (Exception ex)
            {
                // Xử lý các lỗi chung khác
                throw new Exception("An error occurred while creating the kind: " + ex.Message, ex);
            }
        }

        public async Task<KindResponse> UpdateKind(long id, KindRequest kindRequest)
        {
            var existingKind = _unitOfWork.KindRepository.GetByID(id);

            if (existingKind == null)
            {
                throw new CustomException.DataNotFoundException($"Kind with ID {id} not found.");
            }

            if (!existingKind.Status)
            {
                throw new CustomException.InvalidDataException($"Kind with ID {id} was DeActive.");
            }

            if (existingKind.ColorName.ToLower() != kindRequest.ColorName.ToLower())
            {
                var duplicateExists = _unitOfWork.KindRepository.Get(p =>
                    p.Id != id &&
                    p.ProductId == existingKind.ProductId &&
                    p.ColorName.ToLower() == kindRequest.ColorName.ToLower());

                if (duplicateExists != null)
                {
                    throw new CustomException.DataExistException($"Color with name '{kindRequest.ColorName}' already exists for this product.");
                }
            }

            var product = _unitOfWork.ProductRepository.GetByID(existingKind.ProductId);

            product.StockQuantity -= existingKind.Quantity - kindRequest.Quantity;

            if (kindRequest.File != null && kindRequest.File.Length < 10 * 1024 * 1024)
            {
                string imageDownloadUrl = await _firebase.UploadImage(kindRequest.File);
                existingKind.Image = imageDownloadUrl;
            }
            else
            {
                throw new CustomException.InvalidDataException("File size exceeds the maximum allowed limit.");
            }
            _unitOfWork.ProductRepository.Update(product);
            _mapper.Map(kindRequest, existingKind);
            existingKind.Status = true;

            _unitOfWork.Save();

            var kindResponse = _mapper.Map<KindResponse>(existingKind);
            return kindResponse;
        }

        public async Task<bool> DeleteKind(long id)
        {
            try
            {
                var kind = _unitOfWork.KindRepository.GetByID(id);
                if (kind == null)
                {
                    throw new CustomException.DataNotFoundException("Kind not found.");
                }

                kind.Status = false;
                _unitOfWork.KindRepository.Update(kind);
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
