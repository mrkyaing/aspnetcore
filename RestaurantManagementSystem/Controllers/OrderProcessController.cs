using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.DAO;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Models.ViewModels;
using RestaurantManagementSystem.Utilities;
using System.Collections.Generic;

namespace RestaurantManagementSystem.Controllers {
    [Authorize]
    public class OrderProcessController : Controller {
        private readonly RMSDBContext rMSDBContext;
        private readonly IMapper _mapper;
        //constructor injection  for RMSDBContext
        public OrderProcessController(RMSDBContext context,IMapper mapper) {
            rMSDBContext = context;
            _mapper = mapper;
        }
        public IActionResult List() {
            ViewBag.Products = rMSDBContext.Products.Select(s => new ProductViewModel
            {
                Id= s.Id,
                Name= s.Name,
            });
            ViewBag.Tables = rMSDBContext.Tables.Where(x=>x.IsAvailable).Select(s => new TableViewModel
            {
                Id= s.Id,
                No= s.No,
            });
            ViewBag.Employees = rMSDBContext.Employees.Where(x => x.Position.Name.Equals("Waiter")).Select(s => new EmployeeViewModel
            {
                Id = s.Id,
                Name= s.Code +":"+s.Name,
            });

            var viewModels =rMSDBContext.Orders.Where(x=>x.IsPaid==false).Select(s=>new OrderViewModel{
                Id=s.Id,
                No=s.No,
                IsParcel=s.IsParcel==true?"YES":"NO",
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
       
        public JsonResult GetUnitPriceByProductId(string productId) {
            var Product=rMSDBContext.Products.Where(x=>x.Id.Equals(productId)).FirstOrDefault();
            return Json(Product.UnitPrice);
        }

        [HttpPost]
        public JsonResult Entry(OrderViewModel anOrder) {
            try {
                var entity = new OrderEntity(){
                    Id = Guid.NewGuid().ToString(),//for new id when uer create the record 36 char GUID  , UUID ,  (primary key)
                    No = anOrder.No,
                    TableId = anOrder.TableId,
                    EmployeeId= anOrder.EmployeeId,
                    IsParcel=anOrder.IsParcel=="Yes"?true:false,
                    Status=anOrder.Status,
                    IsPaid=false//is paid is always false when order create process
                };
                //adding the record to the Orders of db context
                rMSDBContext.Orders.Add(entity);
                
                var orderDetails = new List<OrderDetailEntity>();
                foreach (var detail in anOrder.orderDetails) {
                       OrderDetailEntity orderDetail = new OrderDetailEntity(){
                        Id= Guid.NewGuid().ToString(),//order master Id (primary key)
                        OrderId=entity.Id,//get the foreign key 
                        ProductId=detail.ProductId,
                        Quantity=detail.Quantity,
                        Remark=detail.Remark,
                         };
                    orderDetails.Add(orderDetail);//collecting the order details
                    }
                    rMSDBContext.OrderDetails.AddRange(orderDetails);//adding the records to the OrderDetails of db context

                var table=rMSDBContext.Tables.Where(x=>x.Id.Equals(anOrder.TableId)).SingleOrDefault();
                  if(table is not null) {
                    table.IsAvailable = false;
                    rMSDBContext.Entry(table).State = EntityState.Modified;
                }

                rMSDBContext.SaveChanges();//finally actually save to the database 
                return Json(new { response = "success" });
            }
            catch (Exception ex) {
                ViewBag.Msg = "Error occur when record is created because of " + ex.Message;
                return Json(new { response = "error" });
            }
        }
        public IActionResult CheckOrderAndOrderDetails(string orderId) {
            OrderViewModel orderAndOrderDetails=rMSDBContext.Orders.Where(x=>x.Id==orderId).Select(o=>new OrderViewModel{
                No = o.No,
                TableNo = o.Table.No,
                IsParcel = o.IsParcel.Equals(true) ? "YES" : "NO",
                EmployeeNo = o.Employee.Code + ":" + o.Employee.Name,
                orderDetails=rMSDBContext.OrderDetails.Where(od=>od.OrderId==orderId).Select(s=>new OrderDetailViewModel {
                      Products=rMSDBContext.Products.Where(p=>p.Id==s.ProductId).Select(pp=>new ProductViewModel{
                       Code = pp.Code,
                       Name = pp.Name,
                       Category = pp.Category,
                       UnitPrice = pp.UnitPrice
                   }).ToList(),
                  Quantity=s.Quantity,
                  Remark=s.Remark,
                }).ToArray()
            }).SingleOrDefault();
            return View(orderAndOrderDetails);
        }

        public IActionResult Delete(string Id) {
            try {
                var order = rMSDBContext.Orders.Where(x => x.Id.Equals(Id)).SingleOrDefault(); //getting selected the order
                
                var orderDetails = rMSDBContext.OrderDetails.Where(x => x.OrderId.Equals(Id)).ToList(); //selecting the order detail according to order id 
                if (order == null || orderDetails.Count<0) {
                    TempData["Msg"] = "There is no recrod that your selected order.";
                    return RedirectToAction("List");
                }
                rMSDBContext.OrderDetails.RemoveRange(orderDetails);//remove order details
                rMSDBContext.Orders.Remove(order);//remove the order

                var table = rMSDBContext.Tables.Where(x => x.Id.Equals(order.TableId)).SingleOrDefault();
                if (table is not null) {
                    table.IsAvailable = true;
                    rMSDBContext.Entry(table).State = EntityState.Modified;
                }
                rMSDBContext.SaveChanges();//finally remove the records(Order & order details ) from the database 
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
