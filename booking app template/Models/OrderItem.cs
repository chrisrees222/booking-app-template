using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace booking_app_template.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        public double Amount { get; set; }
        public double Price { get; set; }

        public DateOnly HireDay { get; set; } 

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }
    }
    
}
