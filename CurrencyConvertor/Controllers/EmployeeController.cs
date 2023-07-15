using CurrencyConvertor.Models;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConvertor.Controllers {
    public class EmployeeController : Controller {
        public IActionResult Index() {
            return View();
        }

        public IActionResult Entry() {
            return View();
        }

        [HttpPost]
        public IActionResult Entry(EmployeeModel employeeModel) {
            ViewBag.FullName = employeeModel.FirstName + " " + employeeModel.LastName;
            ViewBag.BirthPlace = "Your Birth Place is :" + employeeModel.NativeAddress.BirthPlace;
            return View();
        }

        public IActionResult MultiEntry() {
            return View();
        }
        [HttpPost]
        public IActionResult MultiEntry(IList<string> Id,IList<string> FirstName,IList<string> LastName) {
            IList<string> records = new List<string>();
            for(int i = 0; i < Id.Count;i++) {
                records.Add(Id[i] + " " + FirstName[i] + " " + LastName[i]);
            }
            ViewBag.Records = records;
            return View();
        }
    }
}
