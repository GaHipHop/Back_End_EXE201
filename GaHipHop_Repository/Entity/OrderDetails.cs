using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GaHipHop_Repository.Entity
{
    [Table("OrderDetails")]
    public class OrderDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long KindId { get; set; }

        public long OrderId { get; set; }

        public int OrderQuantity { get; set; }

        public double OrderPrice { get; set; }

        [ForeignKey("KindId")]
        public virtual Kind Kind { get; set; }

       [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
    }
}
