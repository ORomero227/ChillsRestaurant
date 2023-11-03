using ChillsRestaurant.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using Microsoft.AspNetCore.Session;
using Newtonsoft.Json;
using ChillsRestaurant.Models.ViewModels.Order;

namespace ChillsRestaurant.Controllers
{
    public class OrderController : Controller
    {
        private readonly ChillsRestaurantDBContext dbContext;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ILogger<OrderController> logger;

        public OrderController(ChillsRestaurantDBContext context, IHttpContextAccessor httpContextAccessor, ILogger<OrderController> logger)
        {
            this.dbContext = context;
            this.httpContextAccessor = httpContextAccessor;
            this.logger = logger;
        }
        
        [HttpGet]
        public IActionResult OrderIndex()
        {

            var orderString = httpContextAccessor.HttpContext.Session.GetString("OrderList");
            List<MenuItem> orders;

            if (string.IsNullOrEmpty(orderString))
            {
                orders = new List<MenuItem>();
            }
            else
            {
                orders = JsonConvert.DeserializeObject<List<MenuItem>>(orderString);
            }

            OrderIndexViewModel model = new OrderIndexViewModel();
            model.itemsInOrder = orders;


            return View(model);
        }


        [HttpPost]
        public IActionResult AddToOrder(string itemName)
        {
            if (itemName == null)
            {
                TempData["ErrorAddToCart"] = "Error ocurred";
                return RedirectToAction("Index", "Home");
            }

            var orderString = httpContextAccessor.HttpContext.Session.GetString("OrderList");
            List<MenuItem> orders;

            if (string.IsNullOrEmpty(orderString))
            {
                orders = new List<MenuItem>();
            }
            else
            {
                orders = JsonConvert.DeserializeObject<List<MenuItem>>(orderString);
            }

            var item = dbContext.MenuItems.FirstOrDefault(x => x.Name == itemName);

            if (item == null)
            {
                return RedirectToAction("Index","Home");
            }
            
            orders.Add(item);

            orderString = JsonConvert.SerializeObject(orders);
            httpContextAccessor.HttpContext.Session.SetString("OrderList", orderString);

            TempData["SuccessAddToCart"] = "Success Adding";

            return RedirectToAction("Index", "Home");
        }

    }
}
