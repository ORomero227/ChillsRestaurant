using System.ComponentModel.DataAnnotations.Schema;

namespace ChillsRestaurant.Models
{
    public class MenuItem
    {
        public Guid Id {get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal Price { get; set; }

        public string Category {get; set; }

        public string Photo { get; set; }

        public string Status {get;set; }
    }
}
