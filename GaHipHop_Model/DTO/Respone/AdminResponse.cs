using GaHipHop_Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaHipHop_Model.DTO.Respone
{
    public class AdminResponse
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public bool Status { get; set; }
        public Role Role { get; set; }
    }
}
