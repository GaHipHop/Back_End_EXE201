using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaHipHop_Repository.Entity
{
    public class CartItem
    {
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public double ProductPrice { get; set; }
        public string ProductName { get; set; }
        //public List<Img> Images { get; set; }
        public long ProductImage { get; set; }
    }

}
