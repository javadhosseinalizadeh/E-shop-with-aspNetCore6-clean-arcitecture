using App.Domain.Core.Contracts.Repositories;
using App.EndPoints.UI.Areas.Admin.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace App.EndPoints.UI.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = "Customer")]
    public class CustomerBidController : Controller
    {
        private readonly IBidRepository _bidRepository;
        private readonly IOrderStatusRepository _orderStatusRepository;
        private readonly IDistributedCache _distributedCache;

        public CustomerBidController(IBidRepository bidRepository, IOrderStatusRepository orderStatusRepository, IDistributedCache distributedCache)
        {
            _bidRepository = bidRepository;
            _orderStatusRepository = orderStatusRepository;
            _distributedCache = distributedCache;
        }
        public async Task<IActionResult> Index(int id, CancellationToken cancellationToken)
        {
            _distributedCache.Set("TotalBid",null);
            var totalBid = _distributedCache.Get("TotalBid");
            _distributedCache.Remove("TotalBid");

            var result = (await _orderStatusRepository.Get(id,cancellationToken));
            ViewBag.statusList = new SelectList("Id", "Title");

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
