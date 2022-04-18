namespace Admin.Controllers;
public class CurrencyController : Controller
{
    public IActionResult Index()
    {
        ViewBag.CurrencyDatasource = new List<CurrencyViewModel>
        {

        };
        return View();
    }
}