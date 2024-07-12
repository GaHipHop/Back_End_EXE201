using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaHipHop_Model.DTO.Response
{
    public class ContactResponse
    {
        public long Id {  get; set; }
        public string Phone {  get; set; }
        public string Email { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Tiktok { get; set; }
        public string Shoppee { get; set; }
    }
}
