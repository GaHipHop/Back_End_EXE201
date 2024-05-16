using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GaHipHop_Repository.Entity
{
    [Table("Contact")]
    public class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Facebook { get; set; }

        [Required]
        public string Instagram { get; set; }

        [Required ]
        public string Tiktok { get; set; }

        [Required]
        public string Shoppee { get; set; }
    }
}
