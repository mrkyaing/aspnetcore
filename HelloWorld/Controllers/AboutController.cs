using Microsoft.AspNetCore.Mvc;

namespace HelloWorld.Controllers {
    public class AboutController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
