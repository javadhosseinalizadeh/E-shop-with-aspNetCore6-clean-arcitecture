using App.Domain.Core.Contracts.Repositories;
using App.EndPoints.UI.Areas.Admin.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App.EndPoints.UI.Areas.Expert.Controllers
{
    [Area("Expert")]
    [Authorize(Roles = "Expert")]
    public class ExpertBidController : Controller
    {
        private readonly IBidRepository _bidRepository;
        private readonly IOrderStatusRepository _orderStatusRepository;

        public ExpertBidController(IBidRepository bidRepository, IOrderStatusRepository orderStatusRepository)
        {
            _bidRepository = bidRepository;
            _orderStatusRepository = orderStatusRepository;
        }
        public async Task<IActionResult> Index(int id,CancellationToken cancellationToken)
        {
            var result = (await _orderStatusRepository.GetAll(cancellationToken)).ToList();
            ViewBag.statusList = new SelectList(result, "Id", "Title");

            var bids = await _bidRepository.GetAll(cancellationToken);
            var bidsModel = bids.Select(b => new BidViewModel()
            {
                Id = b.Id,
                OrderId = b.OrderId,
                ExpertUserId = b.ExpertUserId,
                SuggestedPrice = b.SuggestedPrice,
                IsApproved = b.IsApproved,
                CreatedAt = b.CreatedAt,
            }).ToList();
            return View(bidsModel);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _bidRepository.Delete(id, cancellationToken);
            return RedirectToAction("Index");
        }
    }
}
