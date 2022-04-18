namespace Admin.Controllers;
public class UserController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [Authorize]
    public IActionResult Header(string userName)
    {
        return View();
    }
}
