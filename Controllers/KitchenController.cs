using ChillsRestaurant.Models;
using ChillsRestaurant.Models.AuthenticationModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChillsRestaurant.Controllers
{
    [Authorize(Roles = "Manager,Employee,Kitchen")]
    public class KitchenController : Controller
    {
        private readonly ChillsRestaurantDBContext dbContext;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<ApplicationUser> userManager;

        public KitchenController(ChillsRestaurantDBContext context, IHttpContextAccessor httpContextAccessor, ILogger<OrderController> logger, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public IActionResult KitchenIndex()
        {
            List<Order> paidOrders = dbContext.Orders.Include(o => o.orderItems).Where(gs => gs.GeneralStatus == "Paid" && gs.KitchenStatus == "In-Queue").ToList();

            KitchenViewModel model = new KitchenViewModel();
            model.paidOrders = paidOrders;

            return View(model);
        }

        public async Task<IActionResult> WorkingOrders()
        {
            var user = await userManager.GetUserAsync(httpContextAccessor.HttpContext.User);

            if (user == null)
            {
                return RedirectToAction("KitchenIndex");
            }

            List<Order> workinOrders = dbContext.Orders.Include(oi => oi.orderItems).Where(un => un.EmployeeName == user.Name && un.GeneralStatus != "Closed").ToList();

            return View(workinOrders);
        }

        public async Task<IActionResult> ClaimOwnership(Guid orderId)
        {
            var user = await userManager.GetUserAsync(httpContextAccessor.HttpContext.User);

            if (orderId == Guid.Empty)
            {
                return RedirectToAction("KitchenIndex", "Kitchen");
            }

            var orderExists = dbContext.Orders.FirstOrDefault(o => o.Id == orderId);

            if (orderExists == null)
            {
                return RedirectToAction("KitchenIndex", "Kitchen");
            }

            orderExists.KitchenStatus = "In-Process";
            orderExists.Owner = "Staff";
            orderExists.EmployeeName = user.Name;

            if (user != null)
            {
                orderExists.EmployeeName = user.Name;
            }

            dbContext.Orders.Update(orderExists);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("WorkingOrders");
        }

        public async Task<IActionResult> MarkCompleted(Guid orderId)
        {
            var user = await userManager.GetUserAsync(httpContextAccessor.HttpContext.User);

            if (orderId == Guid.Empty)
            {
                return RedirectToAction("WorkingOrders");
            }

            var orderExists = dbContext.Orders.FirstOrDefault(o => o.Id == orderId);

            if (orderExists == null)
            {
                return RedirectToAction("WorkingOrders");
            }

            orderExists.KitchenStatus = "Completed";
            orderExists.GeneralStatus = "Closed";
            orderExists.Owner = "Server";

            if (user != null)
            {
                orderExists.EmployeeName = user.Name;
            }

            dbContext.Orders.Update(orderExists);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("WorkingOrders");
        }
    }
}
