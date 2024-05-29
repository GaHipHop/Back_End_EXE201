using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaHipHop_Repository.Entity
{
    public class CartItem
    {
        public long Id { get; set; }
        public int Quantity { get; set; }
        public double ProductPrice { get; set; }
        public string ProductName { get; set; }
        public String ProductImage { get; set; }
        public string Color { get; set; }
    }

}
