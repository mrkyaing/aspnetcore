using Microsoft.AspNetCore.Mvc;

namespace StudentCRUDMVC.Controllers {
    public class HomeController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
