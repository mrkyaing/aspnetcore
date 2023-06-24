using Microsoft.AspNetCore.Mvc;

namespace HelloWorld.Controllers {
    public class HomeController : Controller {
        public IActionResult Index() {
            return View();
        }

        [HttpGet]
        public int Sum(int n1,int n2,int n3) {
            return n1 + n2+n3;
        }

        [HttpPost]
        public int Add(int a,int b) 
            { 
            return a + b; 
        }

    }
}
