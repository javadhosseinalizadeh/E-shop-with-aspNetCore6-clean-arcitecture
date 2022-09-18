using App.Domain.Core.Contracts.AppServices;
using App.Domain.Core.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App.EndPoints.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = ("Admin"))]
    public class ServiceController : Controller
    {
        private readonly IServiceAppService _serviceAppService;
        private readonly ICategoryAppService _categoryAppService;
        public ServiceController(IServiceAppService serviceAppService, ICategoryAppService categoryAppService)
        {
            _serviceAppService = serviceAppService;
            _categoryAppService = categoryAppService;
        }

        public async Task<IActionResult> Index(int id, CancellationToken cancellationToken)
        {
            var services = await _serviceAppService.GetAll(id, cancellationToken);
            var categories = await _categoryAppService.GetAll(cancellationToken);
            ViewBag.Category = id;
            ViewBag.Categories = categories.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Title
            });
            return View(services);
        }

        public async Task<IActionResult> Create(int id, CancellationToken cancellationToken)
        {
            var categories = await _categoryAppService.GetAll(cancellationToken);
            ViewBag.Category = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ServiceDto serviceDTO, List<IFormFile> files, CancellationToken cancellationToken)
        {
            var result = await _serviceAppService.Add(serviceDTO, files, cancellationToken);
            return RedirectToAction(nameof(Index), new { id = serviceDTO.CategoryId });
        }

        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var service = await _serviceAppService.Get(id, cancellationToken);
            ViewBag.CategoryId = service.CategoryId;
            var categories = await _categoryAppService.GetAll(cancellationToken);
            ViewBag.Categories = categories.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Title
            });
            return View(service);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ServiceDto serviceDTO, CancellationToken cancellationToken)
        {
            await _serviceAppService.Update(serviceDTO, cancellationToken);
            return RedirectToAction(nameof(Index), new { id = serviceDTO.CategoryId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int serviceId, int categoryId, CancellationToken cancellationToken)
        {
            await _serviceAppService.Delete(serviceId, cancellationToken);
            return RedirectToAction(nameof(Index), new { id = categoryId });
        }


        public async Task<IActionResult> Files(int id, CancellationToken cancellationToken)
        {
            var service = await _serviceAppService.Get(id, cancellationToken);
            ViewBag.CategoryId = service.CategoryId;
            ViewBag.ServiceId = id;
            var files = await _serviceAppService.GetAllFiles(id, cancellationToken);
            return View(files);
        }

        public async Task<IActionResult> DeleteFile(int imageId, int serviceId, CancellationToken cancellationToken)
        {
            await _serviceAppService.DeleteServiceFile(imageId, cancellationToken);
            return RedirectToAction("Files", new { id = serviceId });
        }

        public async Task<IActionResult> AddServiceFile(int id, CancellationToken cancellationToken)
        {
            return View(id);
        }
        [HttpPost]
        public async Task<IActionResult> AddServiceFile(int id, List<IFormFile> files, CancellationToken cancellationToken)
        {
            await _serviceAppService.AddServiceFile(id, files, cancellationToken);
            return RedirectToAction(nameof(Files), new { id = id });
        }
    }
}
