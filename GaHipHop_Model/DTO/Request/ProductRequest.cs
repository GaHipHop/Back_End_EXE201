using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaHipHop_Model.DTO.Request
{
    public class ProductRequest
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

        /*public string ColorName { get; set; }

        public int KindQuantity { get; set; }

        public IFormFile? File { get; set; }*/

        /*public List<KindRequest> Kinds { get; set; }*/
    }
}
