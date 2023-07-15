using Microsoft.AspNetCore.Mvc;
using AJAXJQueryTestMVC.Models;
namespace AJAXJQueryTestMVC.Controllers {
    public class OrderController : Controller {
        public IActionResult Index() {
            return View();
        }

        public JsonResult TellMeNow() {
            string now = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
            return Json(now);
        }

        public IActionResult MakeOrder()=>View();

        [HttpPost]
        public JsonResult MakeOrder(Order order) {
            Order anOrder = order;
            return Json(anOrder);
        }
    }
}
