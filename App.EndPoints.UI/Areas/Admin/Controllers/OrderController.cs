using App.Domain.Core.Contracts.Repositories;
using App.Domain.Core.Dtos;
using App.EndPoints.UI.Areas.Admin.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.UI.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var categories = await _orderRepository.GetAll(cancellationToken);
            var categoriesModel = categories.Select(o => new OrderViewModel()
            {
                Id = o.Id,
                StatusId = o.StatusId,
                ServiceId = o.ServiceId,
                ServiceBasePrice = o.ServiceBasePrice,
                CustomerUserId = o.CustomerUserId,
                FinalExpertUserId=o.FinalExpertUserId,
                CreatedAt = o.CreatedAt,
            }).ToList();
            return View(categoriesModel);
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
            };
            await _orderRepository.Add(dto, cancellationToken);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id, CancellationToken cancellationToken)
        {

            var dto = await _orderRepository.Get(id, cancellationToken);
            var viewModel = new OrderViewModel
            {
                Id = dto.Id,
                StatusId = dto.StatusId,
                ServiceId = dto.ServiceId,
                ServiceBasePrice = dto.ServiceBasePrice,
                CustomerUserId = dto.CustomerUserId,
                FinalExpertUserId = dto.FinalExpertUserId,
                CreatedAt = dto.CreatedAt,
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(OrderViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var dto = new OrderDto
            {
                Id = model.Id,
                StatusId = model.StatusId,
                ServiceId = model.ServiceId,
                ServiceBasePrice = model.ServiceBasePrice,
                CustomerUserId = model.CustomerUserId,
                FinalExpertUserId = model.FinalExpertUserId,
                CreatedAt = model.CreatedAt,
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
