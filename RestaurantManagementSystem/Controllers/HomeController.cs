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
            ViewData["AvailableCategoryes"] = _rMSDBContext.Categories.Select(x => new CategoryViewModel
            {
                Code=x.Code,
                Name=x.Name,
                ProductCounts=x.Products.Count()
            }).ToList();
            ViewData["Employees"] = _rMSDBContext.Employees.Where(x =>( !x.Position.Name.Equals("Cashier"))).Select(s => new EmployeeViewModel
            {
                Id = s.Id,
                Name = s.Code + ":" + s.Name,
            }).ToList();

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}