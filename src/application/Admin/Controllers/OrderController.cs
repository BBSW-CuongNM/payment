using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
