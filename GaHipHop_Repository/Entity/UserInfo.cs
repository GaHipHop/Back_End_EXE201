using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GaHipHop_Repository.Entity
{
    [Table("UserInfo")]
    public class UserInfo
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set;}

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Province { get; set; }

        [Required]
        public string Wards { get; set; }

        [Required]
        public string Address { get; set; }

    }
}
