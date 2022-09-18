using App.Domain.Core.Contracts.AppServices;
using App.Domain.Core.Contracts.Repositories;
using App.Domain.Core.Dtos;
using App.EndPoints.UI.Areas.Admin.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App.EndPoints.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderAppService _orderAppService;
        private readonly IOrderStatusAppServcie _statusAppServcie;
        private readonly IUserAppService _userAppService;
        public OrderController(IOrderAppService orderAppService, IOrderStatusAppServcie statusAppServcie, IUserAppService userAppService)
        {
            _orderAppService = orderAppService;
            _statusAppServcie = statusAppServcie;
            _userAppService = userAppService;
        }
        public async Task<IActionResult> Index(int id, CancellationToken cancellationToken)
        {
            var orders = await _orderAppService.GetAll(id, cancellationToken);
            return View(orders);
        }

        public async Task<IActionResult> OrderDetail(int id, CancellationToken cancellationToken)
        {
            var order = await _orderAppService.Get(id, cancellationToken);
            var user = await _userAppService.GetCurrentUserFullInfo(cancellationToken);
            ViewBag.UserId = user.Id;
            return View(order);
        }

        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var statuses = await _statusAppServcie.GetAll(cancellationToken);
            ViewBag.Statuses = statuses.Select(c => new SelectListItem()
            {
                Value = c.Id.ToString(),
                Text = c.Name
            });
            var order = await _orderAppService.Get(id, cancellationToken);
            return View(order);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(OrderDto orderDTO, CancellationToken cancellationToken)
        {
            await _orderAppService.Update(orderDTO, cancellationToken);
            return RedirectToAction("OrderDetail", new { id = orderDTO.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int orderId, CancellationToken cancellationToken)
        {
            await _orderAppService.Delete(orderId, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Files(int id, CancellationToken cancellationToken)
        {
            var files = await _orderAppService.GetAllFiles(id, cancellationToken);
            return View(files);
        }
        public async Task<IActionResult> DeleteFile(int orderId, int imageId, CancellationToken cancellationToken)
        {
            await _orderAppService.DeleteOrderFile(imageId, cancellationToken);
            return RedirectToAction("OrderDetail", new { id = orderId });
        }


    }
}
