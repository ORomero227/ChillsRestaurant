using ChillsRestaurant.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ChillsRestaurant.Controllers
{ 
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ChillsRestaurantDBContext _dbContext;

        public HomeController(ILogger<HomeController> logger, ChillsRestaurantDBContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            ViewBag.ListOfBurgers = _dbContext.MenuItems.Where(m => m.Category == "Burgers").ToList();
            ViewBag.ListOfPasta = _dbContext.MenuItems.Where(m => m.Category == "Pasta").ToList();
            ViewBag.ListOfDesserts = _dbContext.MenuItems.Where(m => m.Category == "Desserts").ToList();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}