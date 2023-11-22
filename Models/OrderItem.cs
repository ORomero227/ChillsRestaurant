using System.ComponentModel.DataAnnotations.Schema;

namespace ChillsRestaurant.Models
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public string Name{get; set;}
        public string Photo {get; set;}
        public int Quantity {get; set;}
        public decimal Price {get; set;}
        public decimal Amount {get; set;}


        //Relacion con menuItem
        public Guid MenuItemId {get; set;}
        public MenuItem menuItem {get; set;}

        //Relacion con order
        public Guid OrderId {get; set;}
        public Order order {get; set;}
    }
}
