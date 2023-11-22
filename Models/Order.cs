namespace ChillsRestaurant.Models
{
    public class Order
    {
        public Guid Id { get; set; }

        public string Owner { get; set; }

        public string EmployeeName {get; set; }

        public List<OrderItem> orderItems { get; set; }

        public decimal orderTotal {get; set; }

        public string KitchenStatus {get; set; }

        public string GeneralStatus {get; set; }

        public ApplicationUser User { get; set; }
    }
}
