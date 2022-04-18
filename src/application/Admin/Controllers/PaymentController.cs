using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
