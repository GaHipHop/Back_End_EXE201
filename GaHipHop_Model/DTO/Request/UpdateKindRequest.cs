using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaHipHop_Model.DTO.Request
{
    public class UpdateKindRequest
    {
        public required string ColorName { get; set; }

        public int? Quantity { get; set; }

        public IFormFile? File { get; set; }
    }
}
