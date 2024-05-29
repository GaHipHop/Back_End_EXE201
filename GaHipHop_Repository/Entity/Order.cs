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

        public string OrderRequirement { get; set; }

        public string OrderCode { get; set; }

        public string PaymentMethod { get; set; }

        public DateTime CreateDate { get; set; }

        public double TotalPrice { get; set; }

        public string Status { get; set; }

        [ForeignKey("UserId")]
        public virtual UserInfo UserInfo { get; set; }

        [ForeignKey("AdminId")]
        public virtual Admin Admin { get; set; }
    }
}
