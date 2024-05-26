using AutoMapper;
using Firebase.Auth;
using Firebase.Storage;
using GaHipHop_Model.DTO.Request;
using GaHipHop_Model.DTO.Response;
using GaHipHop_Repository.Repository;
using GaHipHop_Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net.Sockets;
using System.Text;

namespace GaHipHop_Service.Service
{
    public class KindService : IKindService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        /*private readonly string _apiKey = "AIzaSyAklaGpff9jFMDeQquEvV95oC0yQ5Kv55U";
        private readonly string _storage = "gahiphop-4de10.appspot.com";
        private readonly string _authEmail = "phamdat720749pd@gmail.com";
        private readonly string _authPassword = "123456";*/

        public KindService(IConfiguration configuration, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            var _apiKey = _configuration["Firebase:ApiKey"];
            var _storage = _configuration["Firebase:Storage"];
            var _authEmail = _configuration["Firebase:AuthEmail"];
            var _authPassword = _configuration["Firebase:AuthPassword"];
            var auth = new FirebaseAuthProvider(new FirebaseConfig(_apiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(_authEmail, _authPassword);

            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var storage = new FirebaseStorage(_storage, new FirebaseStorageOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                ThrowOnCancel = true
            });

            using (var stream = file.OpenReadStream())
            {
                var storageReference = storage.Child("images").Child(fileName);
                await storageReference.PutAsync(stream);

                // Get the public download URL of the uploaded image
                return await storageReference.GetDownloadUrlAsync();
            }
        }
        /*if (file == null || file.Length == 0)
        {
            return null;
        }

        {
            // Authentication
            var auth = new FirebaseAuthProvider(new FirebaseConfig(_apiKey));
            var authLink = await auth.SignInWithEmailAndPasswordAsync(_authEmail, _authPassword);

            // Upload to Firebase Storage
            var task = new FirebaseStorage(
                _bucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(authLink.FirebaseToken),
                    ThrowOnCancel = true,
                })
                .Child("images")
                .Child(Guid.NewGuid().ToString() + Path.GetExtension(file.FileName))
                .PutAsync(stream);

            // Track progress of the upload
            task.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} %");

            // Await the task to wait until upload completes and get the download URL
            var downloadUrl = await task;

            // Assuming default values for properties, update as necessary
            var newKind = _mapper.Map<Kind>(kindRequest);

            _unitOfWork.KindRepository.Insert(newKind);
            _unitOfWork.Save();

            var kindResponse = _mapper.Map<KindResponse>(newKind);
            return kindResponse;
        }*/
    }
}
