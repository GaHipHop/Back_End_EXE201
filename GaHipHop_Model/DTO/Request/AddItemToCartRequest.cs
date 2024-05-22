using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaHipHop_Model.DTO.Request
{
    public class AddItemToCartRequest
    {
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public long imageId { get; set; }
    }

}
