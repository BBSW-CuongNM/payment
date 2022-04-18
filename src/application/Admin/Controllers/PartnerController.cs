namespace Admin.Controllers;
public class PartnerController : Controller
{
    public IActionResult Index()
    {
        ViewBag.DataSource = new List<PartnerViewModel>
        {
            new PartnerViewModel 
            { 
                Id = "INSURE", 
                Name = "GoTrust insurance", 
                Email = "cskh@gotrust.vn", 
                Phone = "19006819", 
                IPNUrl = "https://gotrust.vn/",
                Website = "https://gotrust.vn/",
                PartnerPublicKey = "", 
                InternalPrivateKey = "", 
                InternalPublicKey = "" 
            },
            new PartnerViewModel
            {
                Id = "AICARE",
                Name = "GoTrust AICARE",
                Email = "cskh@gotrust.vn",
                Phone = "19006819",
                IPNUrl = "https://gotrust.vn/",
                Website = "https://gotrust.vn/",
                PartnerPublicKey = "",
                InternalPrivateKey = "",
                InternalPublicKey = ""
            },
            new PartnerViewModel
            {
                Id = "SHOPDI",
                Name = "SHOPDI",
                Email = "cskh@gotrust.vn",
                Phone = "19006819",
                IPNUrl = "https://gotrust.vn/",
                Website = "https://gotrust.vn/",
                PartnerPublicKey = "",
                InternalPrivateKey = "",
                InternalPublicKey = ""
            }
        };
        return View();
    }
}
