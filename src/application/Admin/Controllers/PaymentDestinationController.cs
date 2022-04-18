using Common.Models;

namespace Admin.Controllers;

public class PaymentDestinationController : Controller
{
    private readonly HttpClient httpClient;
    private readonly ILogger<PaymentDestinationController> logger;
    private readonly string UrlGetAll = "https://dev-api-paygw.gotrust.vn/api/v1/payment_destinations";
    public PaymentDestinationController(HttpClient httpClient,
        ILogger<PaymentDestinationController> logger)
    {
        this.httpClient = httpClient;
        this.logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var result = new List<PaymentDestinationViewModel>();
        var respone = await httpClient.ExecuteGet<CommonCommandResultHasData<List<PaymentDestinationViewModel>>>(
         url: $"{UrlGetAll}"
         );
        result = respone.Data.OrderBy(x=>x.SortIndex).ToList();
        return View(result);
    }
    [HttpPost]
    public async Task<IActionResult> Create(PaymentDestinationViewModel model)
    {
        
        return Json(null);
    }
    [HttpPost]
    public async Task<IActionResult> Update(PaymentDestinationViewModel model)
    {

        return Json(null);
    }
}
