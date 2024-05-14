using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GaHipHop_Repository.Entity
{
    [Table("Admin")]
    public class Admin
    {
        [Key]
        public long Id { get; set; }

        public long RoleId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Passưord { get; set; }

        [Required]
        public bool Status { get; set; }

        [ForeignKey("RoleId")]
        public required Role Role { get; set; } 
    }
}
