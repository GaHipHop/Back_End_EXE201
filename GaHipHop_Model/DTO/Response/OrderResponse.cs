using GaHipHop_Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaHipHop_Model.DTO.Response
{
    public class OrderResponse
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public long AdminId { get; set; }

        public string OrderRequirement { get; set; }

        public string OrderCode { get; set; }

        public string PaymentMethod { get; set; }

        public DateTime CreateDate { get; set; }

        public double TotalPrice { get; set; }

        public string Status { get; set; }

        public UserInfoResponse UserInfo { get; set; }

        public virtual ICollection<OrderDetailResponse> OrderDetails { get; set; } = new List<OrderDetailResponse>();
    }
}
