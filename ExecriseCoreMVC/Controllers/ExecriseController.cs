using Microsoft.AspNetCore.Mvc;

namespace ExecriseCoreMVC.Controllers {
    public class ExecriseController : Controller {

        //declare to know the files under the projects
        private readonly IWebHostEnvironment _webHostEnvironment;
        //Constructor injection for _webHostEnvironment object 
        public ExecriseController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index() {
            return View();
        }

        //to represne UI 
        public IActionResult Calculate() {
            return View();
        }
      //  [NonAction]
        //to calcualte logic 
        public ViewResult Add(int n1,int n2) {//10 20
            int result=n1+n2;
            ViewBag.result=result;//30
            return View("Calculate");
        }

        public IActionResult Register()=>View();
        public IActionResult FullName(string firstName,string lastName) {
            string FullName=firstName+" "+lastName;
            ViewBag.FullName=FullName;
            return View("Register");
        }

        [ActionName("ok")]
        public FileResult DownloadApp() {
            string filename = "nrc.pdf";
            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "DownloadFiles\\") + filename;
            byte[] fileInByte = System.IO.File.ReadAllBytes(filePath);
            return File(fileInByte, "application/pdf", filename);

        }
    }
}
