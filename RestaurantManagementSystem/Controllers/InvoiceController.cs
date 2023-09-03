using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.DAO;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Models.ViewModels;

namespace RestaurantManagementSystem.Controllers {
    [Authorize(Roles = "Admin")]
    public class InvoiceController : Controller {
        private readonly RMSDBContext _rMSDBContext;
        private readonly IMapper _mapper;

        public InvoiceController(RMSDBContext rMSDBContext, IMapper mapper)
        {
            _rMSDBContext = rMSDBContext;
            this._mapper = mapper;
        }
        public IActionResult List() {
            IQueryable<InvoiceViewModel> invoices =_rMSDBContext.Invoices.Select(x=>new InvoiceViewModel{
                Id=x.Id,
                EmployeeId = x.EmployeeId,
                EmployeeNo=x.Employee.Code+":"+x.Employee.Name,
                OrderId = x.OrderId,
                OrderNo=x.Order.No,
                TableNo=x.Order.Table.No,
                PaymentWith=x.PaymentWith,
                TotalAmount=x.TotalAmount,
                No=x.No,
            });
            return View(invoices);
        }
       // public IActionResult Entry() => View();
            public IActionResult Entry(string OrderId) {
            /*
             * select * from order o
            join orderDetail od
            on o.id=od.orderid
            join product p
            on p.Id=od.productId
            where o.Id='yourorderid'
             */
            ViewBag.Employees =_rMSDBContext.Employees.Where(x=>x.Position.Name.Equals("Cashier")).Select(s=>new EmployeeViewModel
            {
                Id=s.Id,
                Name=s.Code +":" +s.Name
            }).ToList();
            InvoiceViewModel orderToPay =(from o in _rMSDBContext.Orders
                                                       join od in _rMSDBContext.OrderDetails 
                                                      on o.Id equals od.OrderId
                                                       join p in _rMSDBContext.Products
                                                      on od.ProductId equals p.Id 
                                                      where o.Id==OrderId select new InvoiceViewModel{
                                                      OrderId=OrderId,
                                                      TotalAmount=(od.Quantity*p.UnitPrice),
                                                      OrderNo=o.No,
                                                      TableNo=o.Table.No,
                                                      No="INV"+DateTime.Now.ToString("ddMMyyyyHHmmssfff")
                                                   }).FirstOrDefault();
            return View(orderToPay);
        }
        [HttpPost]
        public IActionResult Entry(InvoiceViewModel viewModel) {
            try {
                var entity = new InvoiceEntity();
                entity.No = viewModel.No;
                entity.Id = Guid.NewGuid().ToString();//new GUD ID for new invoice Id
                entity.OrderId = viewModel.OrderId;
                entity.EmployeeId = viewModel.EmployeeId;
                entity.TotalAmount = viewModel.TotalAmount;
                entity.PaymentWith= viewModel.PaymentWith;
                _rMSDBContext.Invoices.Add(entity);//adding the record to the Invoices of db context
               
                var order=_rMSDBContext.Orders.Where(x=>x.Id==viewModel.OrderId).FirstOrDefault();
                if (order is not null) {
                    order.IsPaid = true;
                    _rMSDBContext.Entry(order).State = EntityState.Modified;//editing the record to the Orders of db context
                }

                var table = _rMSDBContext.Tables.Where(x => x.Id == order.TableId).SingleOrDefault();
                if(table is not null) {
                    table.IsAvailable = true;
                    _rMSDBContext.Entry(table).State = EntityState.Modified;//editing the record to the Tables of db context
                }
                _rMSDBContext.SaveChanges();// actually save to the database 
                TempData["Msg"] = "Invoice Payment is created successfully";
            }
            catch (Exception ex) {
                TempData["Msg"] = "Error occur when record is created because of " + ex.Message;
            }
            return RedirectToAction("List");
        }
    }
}
