using Microsoft.AspNetCore.Mvc;
using StudentCRUDMVC.DAO;
using StudentCRUDMVC.Models;

namespace StudentCRUDMVC.Controllers {
    public class StudentController : Controller {

        private readonly StudentDbContext _studentDbContext;

        //constructor injection for student db context for database operations 
        public StudentController(StudentDbContext studentDbContext)
        {
            _studentDbContext = studentDbContext;
        }

        public IActionResult Register() {
            return View();
        }
        [HttpPost]
        public IActionResult Register(Student studentModel) {
            try {
                _studentDbContext.Students.Add(studentModel);//adding the data to the db context 
                _studentDbContext.SaveChanges(); // actually  save the record to the database  
                ViewBag.Msg = "success successfully";
            }
            catch (Exception ex) {
                ViewBag.Msg = "error occur because of " +ex.Message;
            }
            return View();
        }
    }
}
