using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.UI.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
