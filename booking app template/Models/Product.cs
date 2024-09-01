using booking_app_template.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace booking_app_template.Models
{
    public class Product : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        [StringLength(210, MinimumLength = 10, ErrorMessage = "The Description must be between 10 and 210 characters.")]
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageURL { get; set; }
        
        public DateOnly HireDate { get; set; }         
    }
}
