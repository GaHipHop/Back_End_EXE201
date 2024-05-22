using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GaHipHop_Repository.Entity
{
    [Table("Product")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        public int StockQuantity { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifiedDate { get; set ; }

        [Required]
        public bool Status { get; set; }

        [ForeignKey("AdminId")]
        public virtual Admin Admin { get; set; }

        [ForeignKey("DiscountId")]
        public virtual Discount Discount { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public virtual ICollection<Kind> Images { get; set; }
    }
}
