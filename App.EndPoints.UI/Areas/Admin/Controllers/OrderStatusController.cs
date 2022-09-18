using App.Domain.Core.Contracts.AppServices;
using App.Domain.Core.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = ("Admin"))]
    public class OrderStatusController : Controller
    {
        private readonly IOrderStatusAppServcie _statusAppServcie;
        public OrderStatusController(IOrderStatusAppServcie statusAppServcie)
        {
            _statusAppServcie = statusAppServcie;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var categories = await _statusAppServcie.GetAll(cancellationToken);
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderStatusDto statusDTO, CancellationToken cancellationToken)
        {
            var result = await _statusAppServcie.Add(statusDTO, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var category = await _statusAppServcie.Get(id, cancellationToken);
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OrderStatusDto statusDTO, CancellationToken cancellationToken)
        {
            await _statusAppServcie.Update(statusDTO, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int statusId, CancellationToken cancellationToken)
        {
            await _statusAppServcie.Delete(statusId, cancellationToken);
            return RedirectToAction(nameof(Index));
        }
    }
}
