using Microsoft.AspNetCore.Mvc;

namespace HelloWorld.Controllers {
    public class ProfileController : Controller {

        public string Now() {
            DateTime now=DateTime.Now;
            string nowMessage;
            if (now.Hour < 12) 
                nowMessage="Hi,Good Morning :Time is " + now.ToString("yyyy-MM-dd HH:mm:ss");
            else nowMessage = "Hi,Good Afternoon :Time is " + now.ToString("yyyy-MM-dd HH:mm:ss");
            return nowMessage;
        }

        // Me Action
        public IActionResult Me() {
            ViewBag.MyName = "mr Kyaing";
            ViewBag.now = Now();
            return View();
        }
        public IActionResult CV() {
            return View();
        }
        //Photo Action
        public IActionResult Photo() {
            string[] myPhotos = {"profile.img","memories.img","birthday.png" };
            ViewBag.Photos = myPhotos;
            return View();
        }
        //Gurop
        public IActionResult Group() =>View();
    }
}
