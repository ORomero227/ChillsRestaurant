using System.ComponentModel.DataAnnotations;

namespace ChillsRestaurant.Models.AuthenticationModels
{
    public class Login
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
