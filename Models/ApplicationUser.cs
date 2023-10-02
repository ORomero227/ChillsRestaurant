using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ChillsRestaurant.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public string? SecondaryPhoneNumber { get; set; }

        public string? Address { get; set; }

        public string? Photo { get; set; }

        public string Role {get; set; }

        public string AccountStatus {get; set; } 
    }
}
