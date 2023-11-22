namespace ChillsRestaurant.Models.ViewModels.Order
{
    public class CurrentOrderViewModel
    {
        public List<OrderItem> itemsInOrder {get;set;}

        public decimal Total {get;set;} 
    }
}
