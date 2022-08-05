using App.Domain.Core.Contracts.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App.EndPoints.UI.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = "Customer")]
    public class CustomerBidController : Controller
    {
        private readonly IBidRepository _bidRepository;
        private readonly IOrderStatusRepository _orderStatusRepository;

        public CustomerBidController(IBidRepository bidRepository, IOrderStatusRepository orderStatusRepository)
        {
            _bidRepository = bidRepository;
            _orderStatusRepository = orderStatusRepository;
        }
        public async Task<IActionResult> Index(int id , CancellationToken cancellationToken)
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
    }
}
