using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GaHipHop_Repository.Entity
{
    [Table("Order")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long UserId { get; set; }

        public long AdminId { get; set; }

        [Required]
        public string OrderRequirement { get; set; }

        [Required]
        public string OrderCode { get; set; }

        [Required]
        public string PaymentMethod { get; set; }

        public DateTime CreateDate { get; set; }

        [Required]
        public double TotalPrice { get; set; }

        [Required]
        public string Status { get; set; }

        [ForeignKey("UserId")]
        public virtual UserInfo UserInfo { get; set; }

        [ForeignKey("AdminId")]
        public virtual Admin Admin { get; set; }
    }
}
