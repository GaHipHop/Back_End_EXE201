using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaHipHop_Model.DTO.Request
{
    public class UpdateProductRequest
    {
        [Required(ErrorMessage = "DiscountId is required")]
        public long DiscountId { get; set; }

        [Required(ErrorMessage = "CategoryId is required")]
        public long CategoryId { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Product price must be greater than or euqal to 0")]

        public double ProductPrice { get; set; }
    }
}
