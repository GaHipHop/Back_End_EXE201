using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaHipHop_Model.DTO.Response
{
    public class CategoryResponse
    {
        public long Id { get; set; }

        public string CategoryName { get; set; }

        public bool Status { get; set; }
    }
}
