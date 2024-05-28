using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GaHipHop_Repository.Entity
{
    [Table("Contact")]
    public class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Facebook { get; set; }

        public string Instagram { get; set; }

        public string Tiktok { get; set; }

        public string Shoppee { get; set; }
    }
}
