using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace ExerciseCoreMVC.Controllers
{
    //[Authorize]
    public class ExerciseController : Controller
    {
        //declare to know the files under the projects
        private readonly IWebHostEnvironment _webHostEnvironment;

        //constructor injection for _webHostEnvironment object
        public ExerciseController(IWebHostEnvironment webHostEnvironment) 
        { 
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        //To represent UI
        public IActionResult Calculate()
        {
            return View();
        }

        [NonAction]
        public ViewResult Add(int n1, int n2){
            int result = n1 + n2;
            ViewBag.result = result;
            return View("Calculate");
        }
        public IActionResult Registration()
        {
            return View();
        }
        public IActionResult Register(string firstName, string lastName)
        {
            string fullName = firstName +" "+ lastName;
            ViewBag.fullName = fullName;
            return View("Registration");
        }

        [ActionName("ok")]
        public FileResult DownloadApp()
        {
            string filename = "me.txt";
            string filepath = Path.Combine(_webHostEnvironment.WebRootPath, "DownloadFiles\\") + filename;
            byte[] fileInByte = System.IO.File.ReadAllBytes(filepath);
            return File(fileInByte, "application/txt",filename);
        }
        public FileResult DownloadTxt()
        {
            string filename = "gg.txt";
            string filepath = Path.Combine(_webHostEnvironment.WebRootPath, "DownloadFiles\\") + filename;
            byte[] fileInByte = System.IO.File.ReadAllBytes(filepath);
            return File(fileInByte, "application/txt", filename);
        }
        public FileResult DownloadPdf()
        {
            string filename = "gg.pdf";
            string filepath = Path.Combine(_webHostEnvironment.WebRootPath, "DownloadFiles\\") + filename;
            byte[] fileInByte = System.IO.File.ReadAllBytes(filepath);
            return File(fileInByte, "application/pdf", filename);
        }
        public int Sum(int n1, int n2, int n3)
        {
            return n1 + n2 + n3;
        }

        public IActionResult GetMutiple(int n1,int n2) {
            ViewBag.Result=n1 * n2;
            return View("Multiple");
        }

        [HttpPost]
        public int Add2Num(int a, int b) {
            return a + b;
        }

        public IActionResult Login() {
        return  View();
        }
        [HttpPost]
        public IActionResult Login(string UserName,string Password) {
            ViewData["success"] = $"Hello,{UserName} , You logined successfully";

            if(UserName.Equals("admin") && Password.Equals("admin123")) {
                TempData["about"] = $"{UserName}";
                return View("home");
            }
            else {
                ViewData["failed"] = "User Name or Passwrod is incorrect.";
                return View();
            }
        }

        public IActionResult Home()=>View();

       public IActionResult About() {
            return View();
        }
    }
}
