using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GaHipHop_Repository.Entity
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string CategoryName { get; set; }

        [Required]
        public bool Status { get; set; }
    }
}
