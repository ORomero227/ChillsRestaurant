using System.ComponentModel.DataAnnotations.Schema;

namespace ChillsRestaurant.Models
{
    [NotMapped]
    public class OrdersHistoryViewModel
    {
        public List<Order> inProgressOrder { get; set; }
        public List<Order> paidOrders { get; set; }
        public List<Order> cancelOrders { get; set; }
        public List<Order> oldOrders {get; set; }
    }
}
