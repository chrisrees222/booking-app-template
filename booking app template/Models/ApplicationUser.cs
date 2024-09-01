using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace booking_app_template.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Display(Name = "Full name")]
        public string FullName { get; set; }

        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }

        public string? StripePayId { get; set; } //set as  ? nullable for registration

        [Display(Name = "First Line of Address")]
        public string Address { get; set; }

        [Display(Name = "City/Town")]
        public string City { get; set; }

        [Display(Name = "County/state")]
        public string State { get; set; }

        [Display(Name = "Post code")]
        public string PostalCode { get; set; }
    }
}
