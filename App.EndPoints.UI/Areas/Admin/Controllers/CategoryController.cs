using App.Domain.Core.Contracts.AppServices;
using App.Domain.Core.Dtos;
using App.EndPoints.UI.Areas.Admin.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryAppService _categoryAppService;

        public CategoryController(ICategoryAppService categoryAppService)
        {
            _categoryAppService = categoryAppService;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var categories = await _categoryAppService.GetAll(cancellationToken);
            var categoriesModel = categories.Select(p => new CategoryViewModel()
            {
                Id = p.Id,
                Title = p.Title,
            }).ToList();
            return View(categoriesModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel category, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            var dto = new CategoryDto
            {
                Id = category.Id,
                Title = category.Title,
            };
            await _categoryAppService.Set(dto, cancellationToken);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id, CancellationToken cancellationToken)
        {

            var dto = await _categoryAppService.Get(id, cancellationToken);
            var viewModel = new CategoryViewModel
            {
                Id = dto.Id,
                Title = dto.Title,
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var dto = new CategoryDto
            {
                Id = model.Id,
                Title = model.Title,
            };
            await _categoryAppService.Update(dto, cancellationToken);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _categoryAppService.Delete(id, cancellationToken);
            return RedirectToAction("Index");
        }

        public async Task<bool> CheckName(string name, CancellationToken cancellationToken)
        {
            try
            {
                await _categoryAppService.Get(name, cancellationToken);
                return false;
            }
            catch (Exception)
            {
                return true;
            }

        }
    }
}
