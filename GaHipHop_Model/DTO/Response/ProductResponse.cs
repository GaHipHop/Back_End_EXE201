using GaHipHop_Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaHipHop_Model.DTO.Response
{
    public class ProductResponse
    {
        public long Id { get; set; }

        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public double ProductPrice { get; set; }

        //public double CurrentPrice { get; set; }
        public int ProductQuantity { get; set; }

        public bool Status { get; set; }

        public virtual Admin Admin { get; set; }

        public virtual Discount Discount { get; set; }

        public virtual Category Category { get; set; }
    }
}
