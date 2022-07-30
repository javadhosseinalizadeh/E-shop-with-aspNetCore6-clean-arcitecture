using App.Domain.Core.Contracts.Repositories;
using App.Domain.Core.Dtos;
using App.EndPoints.UI.Areas.Admin.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.UI.Areas.Admin.Controllers
{
    public class BidController : Controller
    {
        private readonly IBidRepository _bidRepository;
        public BidController(IBidRepository bidRepository)
        {
            _bidRepository = bidRepository;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            
            var bids = await _bidRepository.GetAll(cancellationToken);
            var bidsModel = bids.Select(b => new BidViewModel()
            {
                Id = b.Id,
                OrderId=b.OrderId,
                ExpertUserId=b.ExpertUserId,
                SuggestedPrice=b.SuggestedPrice,
                IsApproved = b.IsApproved,
                CreatedAt=b.CreatedAt,
            }).ToList();
            return View(bidsModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BidViewModel bid, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View(bid);
            }
            var dto = new BidDto
            {
                Id = bid.Id,
                OrderId = bid.OrderId,
                ExpertUserId = bid.ExpertUserId,
                SuggestedPrice = bid.SuggestedPrice,
                IsApproved = bid.IsApproved,
                CreatedAt = bid.CreatedAt,
            };
            await _bidRepository.Add(dto, cancellationToken);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id, CancellationToken cancellationToken)
        {

            var dto = await _bidRepository.Get(id, cancellationToken);
            var viewModel = new BidViewModel
            {
                Id = dto.Id,
                OrderId = dto.OrderId,
                ExpertUserId = dto.ExpertUserId,
                SuggestedPrice = dto.SuggestedPrice,
                IsApproved = dto.IsApproved,
                CreatedAt = dto.CreatedAt,
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(BidViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var dto = new BidDto
            {
                Id = model.Id,
                OrderId = model.OrderId,
                ExpertUserId = model.ExpertUserId,
                SuggestedPrice = model.SuggestedPrice,
                IsApproved = model.IsApproved,
                CreatedAt = model.CreatedAt,
            };
            await _bidRepository.Update(dto, cancellationToken);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _bidRepository.Delete(id, cancellationToken);
            return RedirectToAction("Index");
        }
    }
}

