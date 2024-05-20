using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaHipHop_Model.DTO.Request
{
    public class CreateProductRequest
    {
        public long AdminId { get; set; }
        public long DiscountId { get; set; }
        public long CategoryId { get; set; }
        public required string ProductName { get; set; }
        public required string ProductDescription { get; set; }
        public required double ProductPrice { get; set; }
        public required int ProductQuantity { get; set; }
    }
}
