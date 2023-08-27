using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.DAO;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Models.ViewModels;
using RestaurantManagementSystem.Utilities;
using System.Text;
using RMSReportHelpers;
using Microsoft.Reporting.NETCore;

namespace RestaurantManagementSystem.Controllers {
    public class EmployeeController : Controller {
        private readonly RMSDBContext _rMSDBContext;
        private readonly IWebHostEnvironment _webHostEnvironment;//to be read rdlc file under wwwroot folder/reportFiles
        private readonly IMapper _mapper;
        //constructor injection  for RMSDBContext
        public EmployeeController(RMSDBContext context,IMapper mapper, IWebHostEnvironment webHostEnvironment) {
            _rMSDBContext = context;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }
        public IActionResult List() {
            var i = 10;
            decimal z = 10.3m;
         var  viewModels = _rMSDBContext.Employees.Select(x => new EmployeeViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Code=x.Code,
                Position = x.Position,// getting the employee's postion object 
                Email = x.Email,
                MobilePhone = x.MobilePhone,
                NRC = x.NRC,
                Gender = x.Gender,
                JoinedDate = x.JoinedDate,
                DOB = x.DOB,
                Address = x.Address,
            }).OrderBy(o => o.Code).ToList();
            return View(viewModels); 
        }
        public IActionResult Entry() {
            ViewBag.Positions= _rMSDBContext.Positions.ToList();
            return  View();
        }
            [HttpPost]
        public IActionResult Entry(EmployeeViewModel viewModel) {
            try {
              
                //DTO >> Data Transfer Object 
                var entity = _mapper.Map<EmployeeEntity>(viewModel);
                entity.Id = Guid.NewGuid().ToString();//for new id when uer create the record 36 char GUID  , UUID 
                entity.ProfileImageUrl = "c:\\ok.jpeg";
                _rMSDBContext.Employees.Add(entity);//adding the record to the products of db context
                _rMSDBContext.SaveChanges();// actually save to the database 
                TempData["Msg"] = "1 record is created successfully";
            }
            catch (Exception ex) {
                TempData["Msg"] = "Error occur when record is created because of " + ex.Message;
            }
            return RedirectToAction("List");
        }
        public IActionResult Delete(string Id) {
            try {
                var entity = _rMSDBContext.Employees.Where(x => x.Id.Equals(Id)).SingleOrDefault();
                if (entity == null) {
                    TempData["Msg"] = "There is no recrod that you select.";
                }
                _rMSDBContext.Employees.Remove(entity);// collect the data to remove
                _rMSDBContext.SaveChanges();// remove the record from the database 
                TempData["Msg"] = "delete process is completed successfully.";
            }
            catch (Exception e) {
                TempData["Msg"] = "error occur when record is deleted.";
            }
            return RedirectToAction("List");

        }

        public IActionResult Edit(string Id) {
            var viewModel = _rMSDBContext.Employees.Where(x => x.Id.Equals(Id)).Select(x => new EmployeeViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                PositionId= x.PositionId,
                Email = x.Email,
                MobilePhone = x.MobilePhone,
                NRC = x.NRC,
                Gender = x.Gender,
                JoinedDate = x.JoinedDate,
                DOB = x.DOB,
                Address = x.Address,
            }).SingleOrDefault();
            ViewBag.Positions = _rMSDBContext.Positions.ToList();
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Update(EmployeeViewModel viewModel) {
            try {
                //DTO >> Data Transfer Object 
                var entity = _mapper.Map<EmployeeEntity>(viewModel);
                entity.UpdatedAt = DateTime.Now;
                entity.ProfileImageUrl = "c:\\ok.jpeg";
                _rMSDBContext.Entry(entity).State = EntityState.Modified;//editing the record to the products of db context
                _rMSDBContext.SaveChanges();// actually update to the database 
                TempData["Msg"] = "update process is completed successfully.";
            }
            catch (Exception e) {
                TempData["Msg"] = "error occur when record is updated.";
            }
            return RedirectToAction("List");
        }

        public IActionResult EmployeeDetailReport() {
            ViewBag.FromCode =_rMSDBContext.Employees.OrderBy(o=>o.Code).ToList();
            ViewBag.ToCode = _rMSDBContext.Employees.OrderBy(o => o.Code).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult EmployeeDetailReport(string FromCode,string ToCode) {//s001 ,s003 
            var rdlcFilePath = Path.Combine(_webHostEnvironment.WebRootPath, "ReportFiles", "EmployeeReport.rdlc");
            IList<EmployeeReportModel> employees= _rMSDBContext.Employees.Where(w => w.Code.CompareTo(FromCode)>=0
                                                                                                                                        && w.Code.CompareTo(ToCode)<=0)
                .Select(x => new EmployeeReportModel
                {
                    Code = x.Code,
                    Name = x.Name,
                    Email = x.Email,
                    MobilePhone = x.MobilePhone,
                    NRC = x.NRC,
                    Gender = x.Gender,
                    JoinedDate = x.JoinedDate,
                    DOB = x.DOB, 
                    Address = x.Address,
                    Position = x.Position.Name,
                    BaseSalary = x.Position.Salary,
                    Age = DateTime.Now.Year - x.DOB.Year,
                }).ToList();
            Stream reportDefinition; 
            using var fs = new FileStream(rdlcFilePath, FileMode.Open);
            reportDefinition = fs;
            LocalReport report = new LocalReport();
            report.LoadReportDefinition(reportDefinition);
            report.DataSources.Add(new ReportDataSource("EmployeeReportDataSet", employees));
            report.SetParameters(new[] { new ReportParameter("rp1", $"From Code:{FromCode} To Code :{ToCode}") });
            byte[] pdfFile = report.Render("pdf");
            fs.Dispose();
            return File(pdfFile, "application/pdf");
        }
    }
}
