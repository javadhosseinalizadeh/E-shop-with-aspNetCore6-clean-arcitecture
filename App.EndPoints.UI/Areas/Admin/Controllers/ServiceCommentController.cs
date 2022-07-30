using App.Domain.Core.Contracts.Repositories;
using App.EndPoints.UI.Areas.Admin.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceCommentController : Controller
    {
        private readonly IServiceCommentRepository _serviceCommentRepository;
        public ServiceCommentController(IServiceCommentRepository serviceCommentRepository)
        {
            _serviceCommentRepository = serviceCommentRepository;
        }

        // GET: ServiceCommentController
        public async Task<ActionResult> Index(CancellationToken cancellationToken)
        {
            var cm = await _serviceCommentRepository.GetAll(cancellationToken);
            var cmModel = cm.Select(c=>new ServiceCommentVM()
            {
                Id = c.Id,
                ServiceId = c.ServiceId,
                OrderId = c.OrderId,
                CreatedUserId = c.CreatedUserId,
                CreatedAt = c.CreatedAt
            }).ToList();

            return View(cmModel);
        }

        public async Task<ActionResult> Delete(int id,CancellationToken cancellationToken)
        {
            await _serviceCommentRepository.Delete(id, cancellationToken);
            return RedirectToAction("Index");
        }        
    }
}
