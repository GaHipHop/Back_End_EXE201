using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaHipHop_Model.DTO.Response
{
    public class DiscountResponse
    {
        public long Id { get; set; }
        
        public float Percent { get; set; }
        
        public DateTime ExpiredDate { get; set; }

        public bool Status { get; set; }
    }
}
