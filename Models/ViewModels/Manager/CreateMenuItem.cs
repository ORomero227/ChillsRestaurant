using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ChillsRestaurant.Models.ViewModels.Manager
{
    public class CreateMenuItem
    {
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
    }
}
