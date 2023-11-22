using System.ComponentModel.DataAnnotations;

namespace ChillsRestaurant.Models.ViewModels.Manager
{
    public class EditMenuItem
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Photo { get; set; }

        [Required]
        public string Status {get; set; }
    }
}
