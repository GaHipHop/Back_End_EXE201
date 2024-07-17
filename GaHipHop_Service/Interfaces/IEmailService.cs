using GaHipHop_Model.DTO.Response;
using GaHipHop_Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaHipHop_Service.Interfaces
{
    public interface IEmailService
    {
        Task SendConfirmedOrderEmailAsync(string toEmail, OrderResponse orderResponse);
        Task SendRejectedOrderEmailAsync(string toEmail, OrderResponse orderResponse);
    }
}
