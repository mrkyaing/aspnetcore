using Microsoft.AspNetCore.Mvc;
using RestaurantManagementSystem.DAO;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Models.ViewModels;
using System.Net.Sockets;
using System.Net;

namespace RestaurantManagementSystem.Controllers {
    public class ProductController : Controller {
        private readonly RMSDBContext rMSDBContext;
        //constructor injection  for RMSDBContext
        public ProductController(RMSDBContext context)
        {
           rMSDBContext = context;
        }
        public IActionResult Index() {
            return View();
        }

        public IActionResult Entry()=>View();
        [HttpPost]
        public IActionResult Entry(ProductViewModel productViewModel) {
            try {
                Product product = new Product()
                {
                    Id = Guid.NewGuid().ToString(),//for new id when uer create the record 36 char
                    Name = productViewModel.Name,//c101
                    Code = productViewModel.Code,
                    UnitPrice = productViewModel.UnitPrice,
                    IsAvailable = productViewModel.IsAvailable,
                    IsTodaySpecial = productViewModel.IsTodaySpecial,
                    Category = productViewModel.Category,
                    Ip = GetLocalIp(),
                };
                rMSDBContext.Products.Add(product);
                rMSDBContext.SaveChanges();
                ViewBag.Msg = "1 record is created successfully";
            }catch(Exception ex) {
                ViewBag.Msg = "Error occur when 1 record is created because of " +ex.Message;
            }
            return View();
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
