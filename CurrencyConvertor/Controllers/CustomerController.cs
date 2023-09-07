using CurrencyConvertor.DAO;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConvertor.Controllers
{
    public class CustomerController : Controller
    {
        private readonly AppDbContext _context;

        public CustomerController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var Customers=_context.Customers;
            return View(Customers);
        }
    }
}
