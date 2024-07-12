using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaHipHop_Model.DTO.Response
{
    public class OrderDetailResponse
    {
        public long Id { get; set; }

        public long KindId { get; set; }

        public long OrderId { get; set; }

        public int OrderQuantity { get; set; }

        public double OrderPrice { get; set; }

        public string ProductName { get; set; }

        public string ColorName { get; set; }

        public string Image { get; set; }
    }
}
