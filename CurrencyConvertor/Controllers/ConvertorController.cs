using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CurrencyConvertor.Controllers {
    public class ConvertorController : Controller {
        public IActionResult ChangeCurrency() {
            return View();
        }
        [HttpPost]
        public IActionResult ChangeCurrency(string fromCurrency,decimal amount) {
            decimal result = 0;
            ViewBag.SelectedCurrency = fromCurrency;
            ViewBag.Amount = amount;
            switch (fromCurrency) {
                case "usd":result = amount * 2010.85m;break;
                case "sdg": result = amount * 1920.85m; break;
                case "baht": result = amount * 990.35m; break;
            }
            ViewBag.Result=result;
            return View();
        }
    }
}
