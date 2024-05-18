using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GaHipHop_Repository.Entity
{
    [Table("Img")]
    public class Img
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long ProductId { get; set; }

        [Required]
        public string ColorName { get; set; }

        [Required]
        public string Image { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

    }
}
