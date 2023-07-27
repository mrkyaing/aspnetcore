using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.DAO;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Models.ViewModels;
using RestaurantManagementSystem.Utilities;
using System.Collections.Generic;
namespace RestaurantManagementSystem.Controllers {
    public class OrderProcessController : Controller {
        private readonly RMSDBContext rMSDBContext;
        private readonly IMapper _mapper;
        //constructor injection  for RMSDBContext
        public OrderProcessController(RMSDBContext context,IMapper mapper) {
            rMSDBContext = context;
            _mapper = mapper;
        }
        public IActionResult List() {
            var viewModels = _mapper.Map<List<OrderDetailViewModel>>(rMSDBContext.OrderDetails.OrderBy(o => o.CreatedAt).ToList());
            return View(viewModels); 
        }
        public IActionResult Entry() => View();
        [HttpPost]
        public IActionResult Entry(OrderViewModel order,List<OrderDetailViewModel> orderDetails) {
            try {
                //DTO >> Data Transfer Object 
                var entity = _mapper.Map<OrderEntity>(order);
                entity.Id = Guid.NewGuid().ToString();//for new id when uer create the record 36 char GUID  , UUID 
                entity.Ip = NetworkHelper.GetLocalIp();
                rMSDBContext.Orders.Add(entity);//adding the record to the products of db context
                rMSDBContext.SaveChanges();// actually save to the database 
                foreach (var detail in orderDetails) {
                    detail.Id = Guid.NewGuid().ToString();
                    detail.OrderId = entity.Id;
                }
                var detailEntity = _mapper.Map<List<OrderDetailEntity>>(order);
                rMSDBContext.OrderDetails.AddRange(detailEntity);//adding the record to the products of db context
                rMSDBContext.SaveChanges();// actually save to the database 
                ViewBag.Msg = "1 record is created successfully";
            }
            catch (Exception ex) {
                ViewBag.Msg = "Error occur when record is created because of " + ex.Message;
            }
            return View();
        }
        public IActionResult Delete(string Id) {
            try {
                var entity = rMSDBContext.Categories.Where(x => x.Id.Equals(Id)).SingleOrDefault();
                if (entity == null) {
                    TempData["Msg"] = "There is no recrod that you select.";
                }
                rMSDBContext.Categories.Remove(entity);// collect the data to remove
                rMSDBContext.SaveChanges();// remove the record from the database 
                TempData["Msg"] = "delete process is completed successfully.";
            }
            catch (Exception e) {
                TempData["Msg"] = "error occur when record is deleted.";
            }
            return RedirectToAction("List");

        }

        public IActionResult Edit(string Id) {
            var viewModel = rMSDBContext.Categories.Where(x => x.Id.Equals(Id)).Select(x => new CategoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
            }).SingleOrDefault();

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Update(CategoryViewModel viewModel) {
            try {
                //DTO >> Data Transfer Object 
                var entity = new CategoryEntity()
                {
                    Id = viewModel.Id,//not to generate new id because this is update processs 
                    Name = viewModel.Name,//c101
                    Code = viewModel.Code,
                    Ip = NetworkHelper.GetLocalIp()
                };
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
