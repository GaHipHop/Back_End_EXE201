using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaHipHop_Model.DTO.Response
{
    public class KindResponse
    {
        public long Id { get; set; }

        public long ProductId { get; set; }

        public required string ColorName { get; set; }

        public int Quantity { get; set; }

        public required bool Status { get; set; }

        public required string Image { get; set; }
    }
}
