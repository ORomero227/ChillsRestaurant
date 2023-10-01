using System.ComponentModel.DataAnnotations;

namespace ChillsRestaurant.Models.ViewModels
{
    public class ManagerCreateAccount
    {
        [Required]
        [MinLength(3, ErrorMessage = "Name is too short")]
        [RegularExpression("^[a-zA-ZñÑ ]+$", ErrorMessage = "The First Name entered is invalid")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Primary Phone Number field is required")]
        [RegularExpression("^\\d{3}-\\d{3}-\\d{4}$", ErrorMessage = "Primary phone number is not valid")]
        public string PrimaryPhoneNumber { get; set; }

        [RegularExpression("^\\d{3}-\\d{3}-\\d{4}$", ErrorMessage = "Secondary phone number is not valid")]
        public string? SecondaryPhoneNumber { get; set; }

        public string? Address { get; set; }

        public string? Photo { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string Role {get;set; }
    }
}
