using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaHipHop_Model.DTO.Request
{
    public class CreateDiscountRequest
    {
        [Required]
        public float Percent { get; set; }

        [Required]
        public DateTime ExpiredDate { get; set; }

        [Required]
        public bool Status { get; set; }
    }
}
