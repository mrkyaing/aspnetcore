using Microsoft.AspNetCore.Mvc;

namespace CurrencyConvertor.Controllers {
    public class HomeController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
