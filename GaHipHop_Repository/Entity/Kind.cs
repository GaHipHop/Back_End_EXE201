using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GaHipHop_Repository.Entity
{
    [Table("Kind")]
    public class Kind
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long ProductId { get; set; }

        public string ColorName { get; set; }

        public string Image { get; set; }
        
        public int Quantity { get; set; }

        public bool Status { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

    }
}
