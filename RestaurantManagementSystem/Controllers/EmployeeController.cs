using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.DAO;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Models.ViewModels;
using RestaurantManagementSystem.Utilities;

namespace RestaurantManagementSystem.Controllers {
    public class EmployeeController : Controller {
        private readonly RMSDBContext rMSDBContext;
        private readonly IMapper _mapper;
        //constructor injection  for RMSDBContext
        public EmployeeController(RMSDBContext context,IMapper mapper) {
            rMSDBContext = context;
            _mapper = mapper;
        }
        public IActionResult List() {
            IList<EmployeeViewModel> viewModels = rMSDBContext.Employees.Select(x => new EmployeeViewModel
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
            ViewBag.Positions= rMSDBContext.Positions.ToList();
            return  View();
        }
            [HttpPost]
        public IActionResult Entry(EmployeeViewModel viewModel) {
            try {
              
                //DTO >> Data Transfer Object 
                var entity = _mapper.Map<EmployeeEntity>(viewModel);
                entity.Id = Guid.NewGuid().ToString();//for new id when uer create the record 36 char GUID  , UUID 
                entity.Ip = NetworkHelper.GetLocalIp();
                entity.ProfileImageUrl = "c:\\ok.jpeg";
                rMSDBContext.Employees.Add(entity);//adding the record to the products of db context
                rMSDBContext.SaveChanges();// actually save to the database 
                TempData["Msg"] = "1 record is created successfully";
            }
            catch (Exception ex) {
                TempData["Msg"] = "Error occur when record is created because of " + ex.Message;
            }
            return RedirectToAction("List");
        }
        public IActionResult Delete(string Id) {
            try {
                var entity = rMSDBContext.Employees.Where(x => x.Id.Equals(Id)).SingleOrDefault();
                if (entity == null) {
                    TempData["Msg"] = "There is no recrod that you select.";
                }
                rMSDBContext.Employees.Remove(entity);// collect the data to remove
                rMSDBContext.SaveChanges();// remove the record from the database 
                TempData["Msg"] = "delete process is completed successfully.";
            }
            catch (Exception e) {
                TempData["Msg"] = "error occur when record is deleted.";
            }
            return RedirectToAction("List");

        }

        public IActionResult Edit(string Id) {
            var viewModel = rMSDBContext.Employees.Where(x => x.Id.Equals(Id)).Select(x => new EmployeeViewModel
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
            ViewBag.Positions = rMSDBContext.Positions.ToList();
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Update(EmployeeViewModel viewModel) {
            try {
                //DTO >> Data Transfer Object 
                var entity = _mapper.Map<EmployeeEntity>(viewModel);
                entity.Ip = NetworkHelper.GetLocalIp();
                entity.UpdatedAt = DateTime.Now;
                entity.ProfileImageUrl = "c:\\ok.jpeg";
                rMSDBContext.Entry(entity).State = EntityState.Modified;//editing the record to the products of db context
                rMSDBContext.SaveChanges();// actually update to the database 
                TempData["Msg"] = "update process is completed successfully.";
            }
            catch (Exception e) {
                TempData["Msg"] = "error occur when record is updated.";
            }
            return RedirectToAction("List");
        }

    }
}
