using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.DAO;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Models.ViewModels;
using RestaurantManagementSystem.Utilities;

namespace RestaurantManagementSystem.Controllers {
    public class PositionController : Controller {
        private readonly RMSDBContext rMSDBContext;
        private readonly IMapper mapper;

        //constructor injection  for RMSDBContext
        public PositionController(RMSDBContext context,IMapper mapper) {
            rMSDBContext = context;
            this.mapper = mapper;
        }
        public IActionResult List() {
            IList<PositionViewModel> positions =mapper.Map<List<PositionViewModel>>(rMSDBContext.Positions.OrderBy(o => o.Code).ToList());
            return View(positions);
        }
        public IActionResult Entry() => View();
        [HttpPost]
        public IActionResult Entry(PositionViewModel viewModel) {
            try {
                //DTO >> Data Transfer Object 
                var IsAlreadyExists = rMSDBContext.Positions.Any(x=>x.Code.Equals(viewModel.Code));
                if (IsAlreadyExists)
                {
                    TempData["Msg"] = $"Oh,Sorry {viewModel.Code} is already existed in system.";
                    return RedirectToAction("List");
                }
                
                var entity = mapper.Map<PositionEntity>(viewModel);
                entity.Id = Guid.NewGuid().ToString();
                rMSDBContext.Positions.Add(entity);//adding the record to the products of db context
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
                var entity = rMSDBContext.Positions.Where(x => x.Id.Equals(Id)).SingleOrDefault();
                if (entity == null) {
                    TempData["Msg"] = "There is no recrod that you select.";
                }
                rMSDBContext.Positions.Remove(entity);// collect the data to remove
                rMSDBContext.SaveChanges();// remove the record from the database 
                TempData["Msg"] = "delete process is completed successfully.";
            }
            catch (Exception e) {
                TempData["Msg"] = "error occur when record is deleted.";
            }
            return RedirectToAction("List");

        }

        public IActionResult Edit(string Id) {
            var viewModel =mapper.Map<PositionViewModel>( rMSDBContext.Positions.Where(x => x.Id.Equals(Id)).SingleOrDefault());

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Update(PositionViewModel viewModel) {
            try {
                var entity = mapper.Map<PositionEntity>(viewModel);
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
