using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaHipHop_Model.DTO.Request
{
    public class OrderRequest
    {

        /*public long UserId { get; set; }*/

        public string UserName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; }

        [RegularExpression("^(0?)(3[2-9]|5[6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])[0-9]{7}$", ErrorMessage = "Invalid phone. Please check and press the valid phone!")]
        public string Phone { get; set; }

        public string Province { get; set; }

        public string Wards { get; set; }

        public string Address { get; set; }

        public string OrderRequirement { get; set; }

        public string PaymentMethod { get; set; }

    }
}
