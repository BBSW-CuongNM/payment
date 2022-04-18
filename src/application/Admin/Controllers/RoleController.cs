using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers
{
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
