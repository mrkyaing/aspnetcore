using Microsoft.AspNetCore.Mvc;
using RestaurantManagementSystem.DAO;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Models.ViewModels;
using System.Net.Sockets;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace RestaurantManagementSystem.Controllers {
    public class ProductController : Controller {
        private readonly RMSDBContext rMSDBContext;
        //constructor injection  for RMSDBContext
        public ProductController(RMSDBContext context)
        {
           rMSDBContext = context;
        }
        public IActionResult List() {
            int i = 0;
          IList<ProductViewModel> products = rMSDBContext.Products.Select(x => new ProductViewModel
          //data exchange between View Model and Model >> DTO  
          {
               SrNo=i,
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                UnitPrice = x.UnitPrice,
                Category = x.Category,
                IsAvailable = x.IsAvailable == true ? "Yes" : "No",
                IsTodaySpecial = x.IsTodaySpecial == true ? "Yes" : "No"
            }).OrderBy(o=>o.Code).ToList();
            return View(products);
        }

        public IActionResult Entry()=>View();
        [HttpPost]
        public IActionResult Entry(ProductViewModel productViewModel) {
            try {
                //DTO >> Data Transfer Object 
                Product product = new Product(){
                    Id = Guid.NewGuid().ToString(),//for new id when uer create the record 36 char GUID  , UUID 
                    Name = productViewModel.Name,//c101 
                    Code = productViewModel.Code,
                    UnitPrice = productViewModel.UnitPrice,
                    IsAvailable = productViewModel.IsAvailable.Equals("y")?true : false,
                    IsTodaySpecial = productViewModel.IsTodaySpecial.Equals("y") ? true : false,
                    Category = productViewModel.Category,
                    Ip = GetLocalIp(),
                };
                rMSDBContext.Products.Add(product);//adding the record to the products of db context
                rMSDBContext.SaveChanges();// actually save to the database 
                ViewBag.Msg = "1 record is created successfully";
            }catch(Exception ex) {
                ViewBag.Msg = "Error occur when 1 record is created because of " +ex.Message;
            }
            return View();
        }

        public IActionResult Delete(string Id) {
            try {
                Product product = rMSDBContext.Products.Where(x => x.Id.Equals(Id)).SingleOrDefault();
                if (product == null) {
                    TempData["Msg"] = "There is no recrod that you select.";
                }
                rMSDBContext.Products.Remove(product);// collect the data to remove
                rMSDBContext.SaveChanges();// remove the record from the database 
                TempData["Msg"] = "delete process is completed successfully.";
            }
            catch(Exception e) {
                TempData["Msg"] = "error occur when record is deleted.";
            }
            return RedirectToAction("List");

        }

        public IActionResult Edit(string Id) {
            ProductViewModel  productViewModel = rMSDBContext.Products.Where(x => x.Id.Equals(Id)).Select(x=>new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                UnitPrice = x.UnitPrice,
                Category = x.Category,
                IsAvailable = x.IsAvailable == true ? "y" : "n",
                IsTodaySpecial = x.IsTodaySpecial == true ? "y" : "n"
            }).SingleOrDefault();

            return View(productViewModel);
        }
        [HttpPost]
        public IActionResult Update(ProductViewModel productViewModel) {
            try {
                //DTO >> Data Transfer Object 
                Product product = new Product()
                {
                    Id =productViewModel.Id,//not to generate new id because this is update processs 
                    Name = productViewModel.Name,//c101
                    Code = productViewModel.Code,
                    UnitPrice = productViewModel.UnitPrice,
                    IsAvailable = productViewModel.IsAvailable.Equals("y") ? true : false,
                    IsTodaySpecial = productViewModel.IsTodaySpecial.Equals("y") ? true : false,
                    Category = productViewModel.Category,
                    UpdatedAt=DateTime.Now,
                    Ip = GetLocalIp(),
                };
                rMSDBContext.Entry(product).State=EntityState.Modified;//editing the record to the products of db context
                rMSDBContext.SaveChanges();// actually update to the database 
                TempData["Msg"] = "update process is completed successfully.";
            }
            catch(Exception e) {
                TempData["Msg"] = "error occur when record is updated.";
            }
            return RedirectToAction("List");
        }
        public string GetLocalIp() {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList) {
                if (ip.AddressFamily == AddressFamily.InterNetwork) {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
