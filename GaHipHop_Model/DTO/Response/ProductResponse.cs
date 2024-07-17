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

        public long AdminId { get; set; }

        public long DiscountId { get; set; }

        public float Percent { get; set; }

        public long CategoryId { get; set; }

        public string ProductName { get; set; }

        public string Image {  get; set; }

        public string ColorName { get; set; }

        public string ProductDescription { get; set; }

        public double ProductPrice { get; set; }

        public double CurrentPrice { get; set; }

        public int StockQuantity { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool Status { get; set; }
    }
}
