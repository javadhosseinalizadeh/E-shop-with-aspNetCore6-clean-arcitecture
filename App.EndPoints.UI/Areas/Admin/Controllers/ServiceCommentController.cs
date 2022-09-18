using App.Domain.Core.Contracts.Repositories;
using App.Domain.Core.Dtos;
using App.EndPoints.UI.Areas.Admin.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceCommentController : Controller
    {
        private readonly IServiceCommentRepository _serviceCommentAppService;
        public ServiceCommentController(IServiceCommentRepository serviceCommentAppService)
        {
            _serviceCommentAppService = serviceCommentAppService;
        }

        public async Task<IActionResult> Index(int id, CancellationToken cancellationToken)
        {
            var comments = await _serviceCommentAppService.GetAll(id, cancellationToken);
            return View(comments);
        }

        public async Task<IActionResult> GetAllOrderComments(int id, CancellationToken cancellationToken)
        {
            var comments = await _serviceCommentAppService.GetAllOrderComments(id, cancellationToken);
            return View("OrderComments", comments);
        }

        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var comment = await _serviceCommentAppService.Get(id, cancellationToken);
            return View(comment);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ServiceCommentDto commentDTO, CancellationToken cancellationToken)
        {
            await _serviceCommentAppService.Update(commentDTO, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        //[HttpPost]
        //public async Task<IActionResult> ChangeCommentStatus(int commentId, int orderId, bool status, CancellationToken cancellationToken)
        //{
        //    await _serviceCommentAppService.ChangeCommentStatus(commentId, status, cancellationToken);
        //    return RedirectToAction("OrderDetail", "Order", new { id = orderId });
        //}


        public async Task<IActionResult> EditOrderComments(int id, CancellationToken cancellationToken)
        {
            var comment = await _serviceCommentAppService.Get(id, cancellationToken);

            return View("EditOrderComment", comment);
        }

        [HttpPost]
        public async Task<IActionResult> EditOrderComments(ServiceCommentDto commentDTO, CancellationToken cancellationToken)
        {
            await _serviceCommentAppService.Update(commentDTO, cancellationToken);
            return RedirectToAction("GetAllOrderComments", new { id = commentDTO.OrderId.ToString() });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int commentId, int orderId, CancellationToken cancellationToken)
        {
            await _serviceCommentAppService.Delete(commentId, cancellationToken);
            return RedirectToAction("OrderDetail", "Order", new { id = orderId });
        }
    }
}
