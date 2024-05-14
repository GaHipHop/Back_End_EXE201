using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GaHipHop_Repository.Entity
{
    [Table("OrderDetails")]
    public class OrderDetails
    {
        [Key]
        public long Id { get; set; }

        public long ProductId { get; set; }

        public long OrderId { get; set; }

        [Required]
        public int OrderQuantity { get; set; }

        [Required]
        public double OrderPrice { get; set; }

        [ForeignKey("ProductId")]
        public required Product Product { get; set; }

        [ForeignKey("orderId")]
        public Order order { get; set; }
    }
}
