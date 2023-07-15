using Microsoft.AspNetCore.Mvc;

namespace AJAXJQueryTestMVC.Controllers {
    public class HomeController : Controller {
        public IActionResult Index() {
            return View();
        }

        public IActionResult TellMeNow() {
            ViewBag.MeNow = DateTime.Now.ToShortDateString();
            return View("Index");
        }
    }
}
