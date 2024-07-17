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
        [Required(ErrorMessage = "ProudctId is required")]
        public long ProductId { get; set; }

        [Required(ErrorMessage = "Color name is required")]
        public string ColorName { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity must greater than or equal to 0")]
        public int Quantity { get; set; }

        public IFormFile? File { get; set; }
    }
}
