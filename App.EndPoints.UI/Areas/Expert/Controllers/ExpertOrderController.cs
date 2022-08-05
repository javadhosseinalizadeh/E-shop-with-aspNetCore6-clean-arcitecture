using App.Domain.Core.Contracts.Repositories;
using App.EndPoints.UI.Areas.Admin.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App.EndPoints.UI.Areas.Expert.Controllers
{
    [Area("Expert")]
    [Authorize(Roles = "Expert")]
    public class ExpertOrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderStatusRepository _orderStatusRepository;

        public ExpertOrderController(IOrderRepository orderRepository, IOrderStatusRepository orderStatusRepository)
        {
            _orderRepository = orderRepository;
            _orderStatusRepository = orderStatusRepository;
        }
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {

            var result = (await _orderStatusRepository.GetAll(cancellationToken)).ToList();
            ViewBag.statusList = new SelectList(result, "Id", "Title");

            var orders = await _orderRepository.GetAll(cancellationToken);
            var orderViewModel = orders.Select(o => new OrderViewModel()
            {
                Id = o.Id,
                StatusId = o.StatusId,
                ServiceId = o.ServiceId,
                ServiceBasePrice = o.ServiceBasePrice,
                CustomerUserId = o.CustomerUserId,
                FinalExpertUserId = o.FinalExpertUserId,
                CreatedAt = o.CreatedAt,
            }).ToList();
            return View(orderViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> MyOrders(int id,CancellationToken cancellationToken)
        {

            var result = (await _orderStatusRepository.GetAll(cancellationToken)).ToList();
            ViewBag.statusList = new SelectList(result, "Id", "Title");

            var orders = await _orderRepository.GetAll(cancellationToken);
            var orderViewModel = orders.Select(o => new OrderViewModel()
            {
                Id = o.Id,
                StatusId = o.StatusId,
                ServiceId = o.ServiceId,
                ServiceBasePrice = o.ServiceBasePrice,
                CustomerUserId = o.CustomerUserId,
                FinalExpertUserId = o.FinalExpertUserId,
                CreatedAt = o.CreatedAt,
            }).ToList();
            return View(orderViewModel);
        }
    }
}
