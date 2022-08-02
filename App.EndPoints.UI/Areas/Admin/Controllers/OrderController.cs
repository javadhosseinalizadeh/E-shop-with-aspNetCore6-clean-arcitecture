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
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderStatusRepository _orderStatusRepository;
        public OrderController(IOrderRepository orderRepository, IOrderStatusRepository orderStatusRepository)
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
                //  Status = o.Status,
            }).ToList();
            //   return View();
            return View(orderViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderViewModel order, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View(order);
            }
            var dto = new OrderDto
            {
                Id = order.Id,
                StatusId = order.StatusId,
                ServiceId = order.ServiceId,
                ServiceBasePrice = order.ServiceBasePrice,
                CustomerUserId = order.CustomerUserId,
                FinalExpertUserId = order.FinalExpertUserId,
                CreatedAt = order.CreatedAt,
                //    Status = order.Status,
            };
            await _orderRepository.Add(dto, cancellationToken);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id, CancellationToken cancellationToken)
        {
            var result = (await _orderStatusRepository.GetAll(cancellationToken)).ToList();
            ViewBag.statusList = new SelectList(result, "Id", "Title");
            var dto = await _orderRepository.Get(id, cancellationToken);

            var viewModel = new OrderUpdateViewModel
            {
                Id = dto.Id,
                StatusId = dto.StatusId,
                ServiceId = dto.ServiceId,
                ServiceBasePrice = dto.ServiceBasePrice,
                CustomerUserId = dto.CustomerUserId,
                FinalExpertUserId = dto.FinalExpertUserId,
                CreatedAt = dto.CreatedAt,
                //  Status = dto.Status,
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(OrderUpdateViewModel model, CancellationToken cancellationToken)
        {
            //   var result = (await _orderStatusRepository.GetAll(cancellationToken)).ToList();
            // ViewBag.statusList = new SelectList(result, "Id", "Title");


            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var statuses = await _orderStatusRepository.GetAll(cancellationToken);
            var selectedStatuses = statuses.Where(s => model.StatusIds.Contains(s.Id)).ToList();
            var dto = new OrderDto
            {
                Id = model.Id,
                StatusId = (byte)model.StatusIds.FirstOrDefault(),
                ServiceId = model.ServiceId,
                ServiceBasePrice = model.ServiceBasePrice,
                CustomerUserId = model.CustomerUserId,
                FinalExpertUserId = model.FinalExpertUserId,
                CreatedAt = model.CreatedAt,
                Statuses = selectedStatuses,
            };
            await _orderRepository.Update(dto, cancellationToken);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _orderRepository.Delete(id, cancellationToken);
            return RedirectToAction("Index");
        }


    }
}
