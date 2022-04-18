using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers
{
    public class NavigationPermissionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
