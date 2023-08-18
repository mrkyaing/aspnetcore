using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.DAO;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Models.ViewModels;

namespace RestaurantManagementSystem.Controllers {
    public class InvoiceController : Controller {
        private readonly RMSDBContext _rMSDBContext;
        private readonly IMapper _mapper;

        public InvoiceController(RMSDBContext rMSDBContext, IMapper mapper)
        {
            _rMSDBContext = rMSDBContext;
            this._mapper = mapper;
        }
        public IActionResult List() {
            var invoices=_rMSDBContext.Invoices.Select(x=>new InvoiceViewModel
            {
                EmployeeId = x.EmployeeId,
                OrderId = x.OrderId,
                PaymentWith=x.PaymentWith,
                TotalAmount=x.TotalAmount,
                No=x.No,
            });
            return View(invoices);
        }
       // public IActionResult Entry() => View();
            public IActionResult Entry(string OrderId) {
            ViewBag.Employees = _mapper.Map<List<EmployeeViewModel>>(_rMSDBContext.Employees.ToList());
            var orderToPay=(from o in _rMSDBContext.Orders
                                                    join od in _rMSDBContext.OrderDetails 
                                                   on o.Id equals od.OrderId
                                                   join p in _rMSDBContext.Products
                                                   on od.ProductId equals p.Id
                                                   where o.Id==OrderId select new InvoiceViewModel
                                                   {
                                                      OrderId=OrderId,
                                                      TotalAmount=(od.Quantity*p.UnitPrice),
                                                      OrderNo=o.No,
                                                      TableId=o.TableId
                                                   }).FirstOrDefault();
            return View(orderToPay);
        }

      //  [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Entry(InvoiceViewModel viewModel) {
            try {
                //DTO >> Data Transfer Object 
                //var entity = mapper.Map<TableEntity>(viewModel);
                var entity = new InvoiceEntity();
                entity.No = viewModel.No;
                entity.Id = Guid.NewGuid().ToString();
                entity.OrderId = viewModel.OrderId;
                entity.EmployeeId = viewModel.EmployeeId;
                entity.TotalAmount = viewModel.TotalAmount;
                entity.PaymentWith= viewModel.PaymentWith;
                _rMSDBContext.Invoices.Add(entity);//adding the record to the products of db context
                var table = _rMSDBContext.Tables.Where(x => x.Id == viewModel.TableId).SingleOrDefault();
              if(table is not null) {
                    table.IsAvailable = true;
                    _rMSDBContext.Entry(table).State = EntityState.Modified;//editing the record to the products of db context
                }
                _rMSDBContext.SaveChanges();// actually save to the database 
                TempData["Msg"] = "1 record is created successfully";
            }
            catch (Exception ex) {
                TempData["Msg"] = "Error occur when record is created because of " + ex.Message;
            }
            return RedirectToAction("List");
        }
    }
}
