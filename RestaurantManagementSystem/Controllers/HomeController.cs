using Microsoft.AspNetCore.Mvc;
using RestaurantManagementSystem.DAO;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Models.ViewModels;
using System.Diagnostics;

namespace RestaurantManagementSystem.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private readonly RMSDBContext _rMSDBContext;

        public HomeController(ILogger<HomeController> logger,RMSDBContext rMSDBContext) {
            _logger = logger;
            _rMSDBContext = rMSDBContext;
        }

        public IActionResult Index() {
            ViewBag.TodaySpecialProducts = _rMSDBContext.Products.Where(x => x.IsTodaySpecial).Select(
                s=>new ProductViewModel
                {
                    Name=s.Name,
                    UnitPrice=s.UnitPrice,
                    Category=s.Category,
                }).ToList();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}