using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaHipHop_Model.DTO.Request
{
    public class CategoryRequest
    {
        [Required(ErrorMessage = "Category name is required")]
        public required string CategoryName { get; set; }
    }
}
