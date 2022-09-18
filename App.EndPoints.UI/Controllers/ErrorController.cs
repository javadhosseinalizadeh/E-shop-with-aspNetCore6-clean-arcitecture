using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.UI.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult Index(int statusCode)
        {
            var request=HttpContext.Request;
            if(HttpContext.Request.Headers["Content-Type"] == "application/json")
            {
                
            }
            if (statusCode == 404)
                return View("Error","Home");
            if (statusCode == 500)
                return View("Error", "Home");
            return RedirectToAction("Index", "Home");

        }
    }
}
