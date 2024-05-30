using AutoMapper;
using GaHipHop_Model.DTO.Request;
using GaHipHop_Model.DTO.Response;
using GaHipHop_Repository.Entity;
using GaHipHop_Repository.Repository;
using Microsoft.Extensions.Configuration;
using GaHipHop_Service.Interfaces;
using Tools;

namespace GaHipHop_Service.Service
{
    public class KindService : IKindService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly Tools.Firebase _firebase;

        public KindService(IUnitOfWork unitOfWork, IMapper mapper, Tools.Firebase firebase)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _firebase = firebase;
        }

        public async Task<List<KindResponse>> GetAllKind(QueryObject queryObject)
        {
            var kinds = _unitOfWork.KindRepository.Get(
                filter: p => queryObject.SearchText == null || p.ColorName.Contains(queryObject.SearchText))
                .Where(k => k.Quantity != 0 && k.Status == true)
                .ToList();

            var kindResponses = _mapper.Map<List<KindResponse>>(kinds);

            return kindResponses;
        }

        public async Task<List<KindResponse>> GetAllKindFalse(QueryObject queryObject)
        {
            var kinds = _unitOfWork.KindRepository.Get(
                filter: p => queryObject.SearchText == null || p.ColorName.Contains(queryObject.SearchText))
                .Where(k => k.Quantity != 0 && k.Status == false)
                .ToList();
            var kindResponses = _mapper.Map<List<KindResponse>>(kinds);

            return kindResponses;
        }
        public async Task<List<KindResponse>> GetAllKindByProductId(long id)
        {
            var product = _unitOfWork.ProductRepository.GetByID(id);

            if (product == null)
            {
                throw new CustomException.DataNotFoundException($"Product not found with ID: {id}");
            }

            var kinds = _unitOfWork.KindRepository.Get(
                filter: k => k.ProductId == id && k.Status == true).ToList();
            if (!kinds.Any())
            {
                throw new CustomException.DataNotFoundException($"Product with ID: {id} does not have any Kind");
            }

            var kindResponse = _mapper.Map<List<KindResponse>>(kinds);
            return kindResponse;
        }

        public async Task<KindResponse> GetKindById(long id)
        {
            var kind = _unitOfWork.KindRepository.Get(filter: p => p.Id == id).FirstOrDefault();

            if (kind == null)
            {
                throw new CustomException.DataNotFoundException("Kind not found");
            }

            var kindResponse = _mapper.Map<KindResponse>(kind);
            return kindResponse;
        }

        public async Task<KindResponse> CreateKind(KindRequest kindRequest)
        {
            var existingKind = _unitOfWork.KindRepository.Get().FirstOrDefault(p => p.ProductId == kindRequest.ProductId &&
                                                                p.ColorName.ToLower() == kindRequest.ColorName.ToLower());

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

            if (kindRequest.File != null)
            {
                if (kindRequest.File.Length >= 10 * 1024 * 1024)
                {
                    throw new CustomException.InvalidDataException("File size exceeds the maximum allowed limit.");
                }
                string imageDownloadUrl = await _firebase.UploadImage(kindRequest.File);
                newKind.Image = imageDownloadUrl;
            }


            newKind.Status = true;
            _unitOfWork.KindRepository.Insert(newKind);
            _unitOfWork.Save(); // Lưu thay đổi không đồng bộ

            _mapper.Map(newKind, kindResponse);
            return kindResponse;
        }

        public async Task<KindResponse> UpdateKind(long id, UpdateKindRequest updateKindRequest)
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

            // Check for duplicates (excluding the current Kind being updated)
            var duplicateExists = _unitOfWork.KindRepository.Exists(p =>
                p.Id != id &&
                p.ProductId == existingKind.ProductId &&
                p.ColorName.ToLower() == updateKindRequest.ColorName.ToLower()
            );

            if (duplicateExists)
            {
                throw new CustomException.DataExistException($"Color with name '{updateKindRequest.ColorName}' already exists for this product.");
            }

            var product = _unitOfWork.ProductRepository.GetByID(existingKind.ProductId);

            if (updateKindRequest.Quantity.HasValue && updateKindRequest.Quantity.Value != 0)
            {
                int change = updateKindRequest.Quantity.Value - existingKind.Quantity;
                product.StockQuantity += change;
                existingKind.Quantity = updateKindRequest.Quantity.Value;
            }
            /*else
            { 
                existingKind.Quantity =
            }*/

            // Handle file upload (same as before)
            if (updateKindRequest.File != null)
            {
                if (updateKindRequest.File.Length >= 10 * 1024 * 1024)
                {
                    throw new CustomException.InvalidDataException("File size exceeds the maximum allowed limit.");
                }

                string imageDownloadUrl = await _firebase.UploadImage(updateKindRequest.File);

                if (!string.IsNullOrEmpty(imageDownloadUrl))
                {
                    existingKind.Image = imageDownloadUrl;
                }
            }
            _unitOfWork.ProductRepository.Update(product);
            _mapper.Map(updateKindRequest, existingKind);
            existingKind.Status = true;

            _unitOfWork.Save(); // Save all changes (existingKind and product) together

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

                var product = _unitOfWork.ProductRepository.GetByID(kind.ProductId);
                if (product == null)
                {
                    throw new CustomException.DataNotFoundException("Product not found.");
                }

                product.StockQuantity -= kind.Quantity;
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
