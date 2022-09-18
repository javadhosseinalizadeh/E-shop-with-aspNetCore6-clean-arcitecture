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
    public class BidController : Controller
    {
        private readonly IBidAppService _bidAppService;

        public BidController(IBidAppService bidAppService)
        {
            _bidAppService = bidAppService;
        }
        public async Task<IActionResult> Index(int id, CancellationToken cancellationToken)
        {
            int orderId = id;
            var suggests = await _bidAppService.GetAll(orderId, cancellationToken);
            return View(suggests);
        }


        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var suggest = await _bidAppService.Get(id, cancellationToken);
            return View(suggest);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(BidDto suggestDTO, CancellationToken cancellationToken)
        {
            var id = suggestDTO.OrderId;
            await _bidAppService.Update(suggestDTO, cancellationToken);
            return RedirectToAction("OrderDetail", "Order", new { id = id });
        }

        [HttpPost]
        public async Task<IActionResult> EditSuggest(int suggestId, int orderId, int price, string description, CancellationToken cancellationToken)
        {
            await _bidAppService.EditSuggest(suggestId, price, description, cancellationToken);
            return RedirectToAction("OrderDetail", "Order", new { id = orderId });
        }

        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var suggest = await _bidAppService.Get(id, cancellationToken);
            return View(suggest);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int suggestId, int orderId, CancellationToken cancellationToken)
        {
            await _bidAppService.Delete(suggestId, cancellationToken);
            return RedirectToAction("Index", new { id = orderId });
        }
    }
}

