using App.Domain.Core.Contracts.AppServices;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.UI.Controllers
{
    [Authorize(Roles = "Admin,Expert")]
    public class ExpertController : Controller
    {
        private readonly IUserAppService _userAppService;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IOrderAppService _orderAppService;
        private readonly IOrderStatusAppServcie _statusAppServcie;
        private readonly IBidAppService _suggestAppService;
        private readonly ICommentAppService _commentAppService;

        public ExpertController(IUserAppService userAppService, IHttpContextAccessor httpContext, IOrderAppService orderAppService, IOrderStatusAppServcie statusAppServcie, IBidAppService suggestAppService, ICommentAppService commentAppService)
        {
            _userAppService = userAppService;
            _httpContext = httpContext;
            _orderAppService = orderAppService;
            _statusAppServcie = statusAppServcie;
            _suggestAppService = suggestAppService;
            _commentAppService = commentAppService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetExpertOrders(string name, CancellationToken cancellationToken)
        {
            var orders = await _orderAppService.GetAllExpertOrders(name, cancellationToken);

            return View("Orders", orders);
        }

        public async Task<IActionResult> OrderDetail(int id, CancellationToken cancellationToken)
        {
            var order = await _orderAppService.Get(id, cancellationToken);
            var user = await _userAppService.GetCurrentUserBriefInfo(cancellationToken);
            var statuses = await _statusAppServcie.GetAll(cancellationToken);
            ViewBag.UserId = user.Id;
            ViewBag.Statuses = statuses.Where(x => x.Id != 1 && x.Id != 5);
            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSuggest(int orderId, int suggestId, CancellationToken cancellationToken)
        {
            await _suggestAppService.Delete(suggestId, cancellationToken);

            return RedirectToAction("OrderDetail", new { id = orderId });
        }

        [HttpPost]
        public async Task<IActionResult> CreateSuggest(int orderId, int expertId, int price, string description, CancellationToken cancellationToken)
        {
            var suggestId = await _suggestAppService.CreateSuggest(orderId, expertId, price, description, cancellationToken);

            return RedirectToAction("OrderDetail", new { id = orderId });
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderComment(int orderId, int serviceId, string title, string description, CancellationToken cancellationToken)
        {
            var suggestId = await _commentAppService.CreateOrderComment(orderId, serviceId, title, description, cancellationToken);            
            return RedirectToAction("OrderDetail", new { id = orderId });            
        }

        public async Task<IActionResult> ChangeOrderStatus(int id, CancellationToken cancellationToken)
        {
            await _orderAppService.ChangeOrderStatus(id, cancellationToken);

            return RedirectToAction("OrderDetail", new { id = id });
        }

    }
}
