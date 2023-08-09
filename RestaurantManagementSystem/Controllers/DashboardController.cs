using Microsoft.AspNetCore.Mvc;

namespace RestaurantManagementSystem.Controllers {
    public class DashboardController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
