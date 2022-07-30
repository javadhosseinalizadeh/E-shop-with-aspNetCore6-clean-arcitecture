using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
