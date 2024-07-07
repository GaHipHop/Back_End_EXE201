using GaHipHop_Repository.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaHipHop_Model.DTO.Request
{
    public class AdminRequest
    {

        [Required(ErrorMessage = "UserName is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]

        public string Email { get; set; }

        [Required(ErrorMessage = "FullName is required.")]
        public string FullName { get; set; }

        [Phone(ErrorMessage = "Invalid Phone Number.")]
        public string Phone { get; set; }

        public string Address { get; set; }


    }
}
