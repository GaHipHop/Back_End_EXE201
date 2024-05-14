using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GaHipHop_Repository.Entity
{
    [Table("Img")]
    public class Img
    {
        [Key]
        public long Id { get; set; }

        public long ProductId { get; set; }

        [Required]
        public long ColorName { get; set; }

        [Required]
        public string Image { get; set; }

        [ForeignKey("Product_Id")]
        public required Product Product { get; set; }

    }
}
