using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaHipHop_Model.DTO.Response
{
    public class UserInfoResponse
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Province { get; set; }
        public string Wards { get; set; }
        public string Address { get; set; }
    }
}
