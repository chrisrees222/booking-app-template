using booking_app_template.Models;

namespace booking_app_template.Data.ViewModels
{
    public class ProductOrderVM
    {
        public List<OrderItem> OrderItems { get; set; }
        public Product Product { get; set; }   
        
        public List<DateOnly> DateList {  get; set; } = new List<DateOnly>();
    }
}
