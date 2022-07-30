using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.UI.Areas.Admin.Controllers
{
    public class BidController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
