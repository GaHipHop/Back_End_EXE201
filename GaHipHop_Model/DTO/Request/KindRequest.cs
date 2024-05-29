using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaHipHop_Model.DTO.Request
{
    public class KindRequest
    {
        public long ProductId { get; set; }

        public required string ColorName { get; set; }

        public required int Quantity { get; set; }

        public IFormFile? File { get; set; }
    }
}
