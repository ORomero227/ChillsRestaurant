using System.ComponentModel.DataAnnotations;

namespace ChillsRestaurant.Models.AuthenticationModels
{
    public class Login
    {
        [Required]
        public string UserName { get; set; }


        public string? Password { get; set; }

        public string? PinNumber {get; set; }

        public string ReturnUrl { get; set; }
    }
}
