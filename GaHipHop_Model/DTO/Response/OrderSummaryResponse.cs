using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaHipHop_Model.DTO.Response
{
    public class OrderSummaryResponse
    {
        public virtual ICollection<OrderResponse> Orders { get; set; } = new List<OrderResponse>();
        public int Count { get; set; }
        public double TotalAmount { get; set; }
        public int quantitySold { get; set; }
        public string MostSoldProduct { get; set; }
        public int MostSoldProductQuantity { get; set; }

        public int CountPreviousMonth { get; set; }

        public double TotalAmountPreviousMonth { get; set;}
    }
}
