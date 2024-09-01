using System.ComponentModel.DataAnnotations;

namespace booking_app_template.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Date required and needs to be selected!")]
        public DateOnly DateOnly { get; set; } 
        
        public Product Product { get; set; }
        public double Amount { get; set; }

        public string ShoppingCartId { get; set; }
    }
}
