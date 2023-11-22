using ChillsRestaurant.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using Microsoft.AspNetCore.Session;
using Newtonsoft.Json;
using ChillsRestaurant.Models.ViewModels.Order;
using System.Xml.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ChillsRestaurant.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ChillsRestaurantDBContext dbContext;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ILogger<OrderController> logger;
        private readonly UserManager<ApplicationUser> userManager;
    
        public OrderController(ChillsRestaurantDBContext context, IHttpContextAccessor httpContextAccessor, ILogger<OrderController> logger, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = context;
            this.httpContextAccessor = httpContextAccessor;
            this.logger = logger;
            this.userManager = userManager;
        }
        
        [HttpGet]
        public IActionResult CurrentOrder()
        {
            //Recupero la cadena de serializacion de la sesion
            var orderString = httpContextAccessor.HttpContext.Session.GetString("OrderList");
           
            //Una lista para guardar la lista que esta en la cadena
            List<OrderItem> orders;

            //Verificacion si esta vacia la cadena
            if (string.IsNullOrEmpty(orderString))
            {
                //Si esta vacia creo asigno la lista vacia
                orders = new List<OrderItem>();
            }
            else
            {
                //Si no esta vacia lleno con la lista que esta en la sesion
                orders = JsonConvert.DeserializeObject<List<OrderItem>>(orderString);
            }

            CurrentOrderViewModel model = new CurrentOrderViewModel();
            if (orders != null)
            {
                model.itemsInOrder = orders;
                foreach (var item in orders)
                {
                    item.Amount = item.Price * item.Quantity;
                    model.Total += item.Amount;
                }
            }

            return View(model);
        }
        
        [HttpPost]
        public IActionResult AddToOrder(string itemName)
        {
            if (string.IsNullOrEmpty(itemName))
            {
                TempData["ErrorAddToCart"] = "Error occurred";
                return RedirectToAction("Index", "Home");
            }

            var orderString = httpContextAccessor.HttpContext.Session.GetString("OrderList");
            List<OrderItem> orderItems;

            if (string.IsNullOrEmpty(orderString))
            {
                orderItems = new List<OrderItem>();
            }
            else
            {
                orderItems = JsonConvert.DeserializeObject<List<OrderItem>>(orderString);
            }

            var menuItem = dbContext.MenuItems.FirstOrDefault(x => x.Name == itemName);

            if (menuItem == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var existingOrderItem = orderItems.FirstOrDefault(oi => oi.Name == itemName);

            if (existingOrderItem != null)
            {
                existingOrderItem.Quantity++;
                existingOrderItem.Amount = existingOrderItem.Price * existingOrderItem.Quantity;
            }
            else
            {
                orderItems.Add(
                    new OrderItem { 
                        Name = menuItem.Name, 
                        Photo = menuItem.Photo, 
                        Price = menuItem.Price, 
                        Quantity = 1, 
                        Amount = menuItem.Price,
                        MenuItemId = menuItem.Id
                });
            }


            orderString = JsonConvert.SerializeObject(orderItems);
            httpContextAccessor.HttpContext.Session.SetString("OrderList", orderString);

            TempData["SuccessAddToCart"] = "Added to your order";

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> PayOrder(decimal total)
        {
            var user = await userManager.GetUserAsync(httpContextAccessor.HttpContext.User);

            var orderFromSession = httpContextAccessor.HttpContext.Session.GetString("OrderList");
            List<OrderItem> orderItems;

            if (orderFromSession == null)
            {
                TempData["ErrorInPaymentPhase"] = "Cannot pay, the order is empty.";
                return RedirectToAction("CurrentOrder");
            }
            else
            {
                orderItems = JsonConvert.DeserializeObject<List<OrderItem>>(orderFromSession);
            }

            if (orderItems != null && orderItems.Any())
            {
                var order = new Order
                {
                    Owner = "Server",
                    orderItems = orderItems, // Asignar la lista de OrderItems
                    orderTotal = total,
                    KitchenStatus = "In-Queue",
                    GeneralStatus = "Paid",
                    User = user,
                    EmployeeName = "N/A"
                };

                // Agregar la nueva orden al contexto de Entity Framework
                dbContext.Orders.Add(order);

                // Guardar los cambios en la base de datos
                await dbContext.SaveChangesAsync();

                // Limpiar el carrito de compras en la sesión
                httpContextAccessor.HttpContext.Session.Remove("OrderList");

                // Redirigir a la vista de la orden recién creada o a donde sea apropiado
                return View("PaymentConfirmation");
            }
            else
            {
                TempData["ErrorInPaymentPhase"] = "An error occurred while checking out";
                return RedirectToAction("CurrentOrder");
            }
        }
        
        public async Task<IActionResult> SaveOrder(decimal total)
        {
            var user = await userManager.GetUserAsync(httpContextAccessor.HttpContext.User);

            var orderFromSession = httpContextAccessor.HttpContext.Session.GetString("OrderList");
            List<OrderItem> orderItems;
            
            if (orderFromSession == null)
            {
                TempData["ErrorSavingPhase"] = "Cannot save, the order is empty.";
                return RedirectToAction("CurrentOrder");
            }
            else
            {
                orderItems = JsonConvert.DeserializeObject<List<OrderItem>>(orderFromSession);
            }

            if (orderItems != null && orderItems.Any())
            {
                var order = new Order
                {
                    Owner = "Client",
                    orderItems = orderItems, // Asignar la lista de OrderItems
                    orderTotal = total,
                    KitchenStatus = "In-Queue",
                    GeneralStatus = "In-Progress",
                    User = user,
                    EmployeeName = "N/A"
                };

                // Agregar la nueva orden al contexto de Entity Framework
                dbContext.Orders.Add(order);

                // Guardar los cambios en la base de datos
                await dbContext.SaveChangesAsync();

                // Limpiar el carrito de compras en la sesión
                httpContextAccessor.HttpContext.Session.Remove("OrderList");

                // Redirigir a la vista del history
                return RedirectToAction("OrdersHistory");
            }
            else
            {
                TempData["ErrorSavingPhase"] = "An error occurred while saving the order";
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult RemoveItemFromList(string itemName)
        {
            var orderFromSession = httpContextAccessor.HttpContext.Session.GetString("OrderList");
            List<OrderItem> order;

            if (orderFromSession == null)
            {
                TempData["RemoveItem"] = "Cannot remove, the order is empty.";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                order = JsonConvert.DeserializeObject<List<OrderItem>>(orderFromSession);
            }

            var itemExistInOrder = order.FirstOrDefault(oi => oi.Name == itemName);

            if (itemExistInOrder != null)
            {
                order.Remove(itemExistInOrder);
            }

            orderFromSession = JsonConvert.SerializeObject(order);
            httpContextAccessor.HttpContext.Session.SetString("OrderList", orderFromSession);

            TempData["RemoveItemSuccess"] = "Item removed";

            return RedirectToAction("CurrentOrder");
        }

        [HttpPost]
        public IActionResult EditOrder(string itemName, int quantity)
        {
            var orderFromSession = httpContextAccessor.HttpContext.Session.GetString("OrderList");

            if (orderFromSession == null)
            {
                TempData["EditItem"] = "Cannot edit, the order is empty.";
                return RedirectToAction("Index", "Home");
            }

            List<OrderItem> order = JsonConvert.DeserializeObject<List<OrderItem>>(orderFromSession);

            var itemExistInOrder = order.FirstOrDefault(oi => oi.Name == itemName);

            if (itemExistInOrder != null)
            {
                // Actualizar la cantidad directamente, evitando lógica confusa
                itemExistInOrder.Quantity = quantity;

                // Eliminar el elemento si la cantidad es cero
                if (itemExistInOrder.Quantity == 0)
                {
                    order.Remove(itemExistInOrder);
                }

                // Actualizar la sesión
                httpContextAccessor.HttpContext.Session.SetString("OrderList", JsonConvert.SerializeObject(order));

                TempData["EditItemSuccess"] = "Order Edited";
                return RedirectToAction("CurrentOrder");
            }

            return RedirectToAction("CurrentOrder");
        }

        public async Task<IActionResult> OrdersHistory()
        {
            var user = await userManager.GetUserAsync(httpContextAccessor.HttpContext.User);

            //Ordenes en proceso desde la base de datos
            List<Order> inprogOrders = dbContext.Orders.Include(o => o.orderItems).Where(gs =>gs.GeneralStatus == "In-Progress" && gs.User == user).ToList();
            
            //Ordenes que estan pagadas
            List<Order> paidOrders = dbContext.Orders.Include(o => o.orderItems).Where(gs => gs.GeneralStatus == "Paid" && gs.User == user).ToList();
            
            //Ordenes canceladas 
            List<Order> cancelOrders = dbContext.Orders.Include(o => o.orderItems).Where(gs => gs.GeneralStatus == "Cancel" && gs.User == user).ToList();

            //Ordenes pasadas
            List<Order> oldOrders = dbContext.Orders.Include(o => o.orderItems).Where(gs => gs.GeneralStatus == "Closed" && gs.User == user).ToList();

            OrdersHistoryViewModel model= new OrdersHistoryViewModel();
            model.inProgressOrder = inprogOrders;
            model.paidOrders = paidOrders;
            model.cancelOrders = cancelOrders;
            model.oldOrders = oldOrders;

            return View(model);
        }

        public async Task<IActionResult> PaySelectedOrder(Guid orderId)
        {
            if (orderId == Guid.Empty)
            {
                return RedirectToAction("OrdersHistory");
            }

            var orderExists = dbContext.Orders.FirstOrDefault(o => o.Id == orderId);

            if (orderExists == null)
            {
                return RedirectToAction("OrdersHistory");
            }

            orderExists.GeneralStatus = "Paid";
            orderExists.KitchenStatus = "In-Queue";
            orderExists.Owner = "Server";

            dbContext.Orders.Update(orderExists);
            await dbContext.SaveChangesAsync();

            return View("PaymentConfirmation");
        }
        
        public async Task<IActionResult> CancelSelectedOrder(Guid orderId)
        {
            if (orderId == Guid.Empty)
            {
                return RedirectToAction("OrdersHistory");
            }

            var orderExists = dbContext.Orders.FirstOrDefault(o => o.Id == orderId);

            if (orderExists == null)
            {
                return RedirectToAction("OrdersHistory");
            }

            orderExists.GeneralStatus = "Cancel";
            orderExists.Owner = "Server";
            orderExists.KitchenStatus = "Closed";

            dbContext.Orders.Update(orderExists);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("OrdersHistory");
        }
    }
}
