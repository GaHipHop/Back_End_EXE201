using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GaHipHop_Repository.Entity
{
    [Table("Discount")]
    public class Discount
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public float Percent { get; set; }

        [Required]
        public DateTime ExpiredDate { get; set; }

        [Required]
        public bool Status { get; set; }
    }
}
