
using App.Domain.Core.Contracts.AppServices;
using App.Domain.Core.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

namespace App.EndPoints.UI.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryAppService _categoryAppService;
        private readonly IServiceAppService _serviceAppService;
        private readonly IOrderAppService _orderAppService;
        private readonly ICommentAppService _commentAppService;
        private readonly IUserAppService _userAppService;

        public HomeController(ILogger<HomeController> logger
            , ICategoryAppService categoryAppService
            , IServiceAppService serviceAppService
            , IOrderAppService orderAppService
            , IUserAppService userAppService
            , ICommentAppService commentAppService)
        {
            _logger = logger;
            _categoryAppService = categoryAppService;
            _serviceAppService = serviceAppService;
            _orderAppService = orderAppService;
            _userAppService = userAppService;
            _commentAppService = commentAppService;
        }



        public IActionResult Index(int id, CancellationToken cancellationToken)
        {
            return View();
        }

        public async Task<IActionResult> GetListofServices(int id, CancellationToken cancellationToken)
        {
            var services = await _serviceAppService.GetAll(id, cancellationToken);
            if (id == 0)
                services = null;
            return View("Services", services);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderComment(int orderId, int serviceId, string title, string description, CancellationToken cancellationToken)
        {
            var suggestId = await _commentAppService.CreateOrderComment(orderId, serviceId, title, description, cancellationToken);
            return RedirectToAction("Profile", "Account");
        }




        public async Task<IActionResult> UserOrder(string name, CancellationToken cancellationToken)
        {

            var categories = await _categoryAppService.GetAllWithServices(cancellationToken);
            return View(categories);
        }

        public async Task<IActionResult> NewOrder(int id, CancellationToken cancellationToken)
        {

            ViewBag.ServiceId = id;
            var service = await _serviceAppService.Get(id, cancellationToken);
            ViewBag.ServiceName = service.Title;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewOrder(OrderDto order, List<IFormFile> files, CancellationToken cancellationToken)
        {

            var result = await _orderAppService.AddNewOrder(order, files, cancellationToken);
            return RedirectToAction("Profile", "Account");
        }

        public async Task<IActionResult> AcceptSuggest(int id, CancellationToken cancellationToken)
        {
            await _orderAppService.AcceptOrderSuggest(id, cancellationToken);
            return RedirectToAction("Profile", "Account");
        }

        public IActionResult AccessDenied()
        {


            return View();
        }

        
        public IActionResult Error()
        {
            return View();
        }

    }
}