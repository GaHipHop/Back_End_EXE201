using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GaHipHop_Repository.Entity
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public long Id { get; set; }

        public long AdminId { get; set; }

        public long DiscountId { get; set; }
        
        public long CategoryId { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public string ProductDescription { get; set; }

        [Required]
        public double ProductPrice { get; set; }

        [Required]
        public int ProductQuantity { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifiedDate { get; set ; }

        [Required]
        public bool Status { get; set; }

        [ForeignKey("AdminId")]
        public required Admin Admin { get; set; }

        [ForeignKey("DiscountId")]
        public required Discount Discount { get; set; }

        [ForeignKey("CategoryId")]
        public required Category Category { get; set; }

    }
}
