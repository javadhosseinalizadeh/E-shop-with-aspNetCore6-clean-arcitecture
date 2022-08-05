using App.Domain.Core.Contracts.Repositories;
using App.EndPoints.UI.Areas.Admin.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.UI.Areas.Expert.Controllers
{
    [Area("Expert")]
    [Authorize(Roles = "Expert")]
    public class ExpertCommentController : Controller
    {
        private readonly IServiceCommentRepository _serviceCommentRepository;

        public ExpertCommentController(IServiceCommentRepository serviceCommentRepository)
        {
            _serviceCommentRepository = serviceCommentRepository;
        }
        public async Task<ActionResult> Index(int id,CancellationToken cancellationToken)
        {
            var cm = await _serviceCommentRepository.GetAll(cancellationToken);
            var cmModel = cm.Select(c => new ServiceCommentVM()
            {
                Id = c.Id,
                ServiceId = c.ServiceId,
                OrderId = c.OrderId,
                CreatedUserId = c.CreatedUserId,
                CreatedAt = c.CreatedAt
            }).ToList();

            return View(cmModel);
        }

        public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _serviceCommentRepository.Delete(id, cancellationToken);
            return RedirectToAction("Index");
        }
    }
}
