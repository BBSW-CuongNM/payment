namespace Admin.Controllers;
public class AuthenticateController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Login(LoginViewModel model)
    {
        return View();
    }
}
