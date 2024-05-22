using GaHipHop_Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaHipHop_Model.DTO.Response
{
    public class CartResponse
    {
        public List<CartItem> Items { get; set; }
        public double TotalPrice { get; set; }
    }

}
