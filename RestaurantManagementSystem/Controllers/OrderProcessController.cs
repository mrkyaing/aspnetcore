using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.DAO;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Models.ViewModels;
using RestaurantManagementSystem.Utilities;

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
            ViewBag.Products=_mapper.Map<List<ProductViewModel>>(rMSDBContext.Products.ToList());
            ViewBag.Tables = _mapper.Map<List<TableViewModel>>(rMSDBContext.Tables.Where(x=>x.IsAvailable).ToList());
            ViewBag.Employees = _mapper.Map<List<EmployeeViewModel>>(rMSDBContext.Employees.ToList());
            var viewModels =rMSDBContext.Orders.Select(s=>new OrderViewModel{
                Id=s.Id,
                No=s.No,
                IsParcel=s.IsParcel==true?"yes":"no",
                Status=s.Status,
               Employee=s.Employee,
                EmployeeId=s.EmployeeId,
                Table=s.Table,
                TableId=s.TableId,
                //orderDetails=rMSDBContext.OrderDetails.Where(x=>x.OrderId.Equals(s.Id)).ToArray(),
            }).OrderBy(o => o.No).ToList();
            return View(viewModels); 
        }
        public IActionResult Entry() { return View(); }
        [HttpPost]
        public JsonResult Entry(OrderViewModel anOrder) {
            try {
                    //DTO >> Data Transfer Object 
                    var entity = _mapper.Map<OrderEntity>(anOrder);
                    entity.Id = Guid.NewGuid().ToString();//for new id when uer create the record 36 char GUID  , UUID 
                    //entity.Ip = NetworkHelper.GetLocalIp();
                    rMSDBContext.Orders.Add(entity);//adding the record to the products of db context
                    rMSDBContext.SaveChanges();// actually save to the database 
                    foreach (var detail in anOrder.orderDetails) {
                        detail.Id = Guid.NewGuid().ToString();
                        detail.OrderId = entity.Id;
                    }
                    var detailEntity = _mapper.Map<List<OrderDetailEntity>>(anOrder.orderDetails);
                    rMSDBContext.OrderDetails.AddRange(detailEntity);//adding the record to the products of db context
                    rMSDBContext.SaveChanges();// actually save to the database 
                   var table=rMSDBContext.Tables.Where(x=>x.Id.Equals(anOrder.TableId)).SingleOrDefault();
                   table.IsAvailable = false;
                 rMSDBContext.Entry(table).State = EntityState.Modified;//editing the record to the products of db context
                rMSDBContext.SaveChanges();// actually save to the database 
                return Json(new { message = "success" });
            }
            catch (Exception ex) {
                ViewBag.Msg = "Error occur when record is created because of " + ex.Message;
                return Json(new { message = "error" });
            }
        }
        public IActionResult Delete(string Id) {
            try {
                var order = rMSDBContext.Orders.Where(x => x.Id.Equals(Id)).SingleOrDefault();
                var orderDetails = rMSDBContext.OrderDetails.Where(x => x.OrderId.Equals(Id)).ToList();
                if (order == null || orderDetails.Count<0) {
                    TempData["Msg"] = "There is no recrod that you select.";
                }
                rMSDBContext.OrderDetails.RemoveRange(orderDetails);
                rMSDBContext.Orders.Remove(order);// collect the data to remove
                rMSDBContext.SaveChanges();// remove the record from the database 
                TempData["Msg"] = "delete process is completed successfully.";
            }
            catch (Exception e) {
                TempData["Msg"] = "error occur when record is deleted.";
            }
            return RedirectToAction("List");
        }

        public IActionResult Edit(string Id) {
            var viewModel = rMSDBContext.Categories.Where(x => x.Id.Equals(Id)).Select(x => new CategoryViewModel{
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
                var entity = new CategoryEntity(){
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
