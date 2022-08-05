using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.UI.Areas.Expert.Controllers
{
    [Area("Expert")]
    [Authorize(Roles ="Expert")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
