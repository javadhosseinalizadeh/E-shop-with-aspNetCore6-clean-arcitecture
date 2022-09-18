using App.Domain.Core.Contracts.AppServices;
using App.Domain.Core.Dtos;
using App.Domain.Core.Entities;
using App.EndPoints.UI.Areas.Admin.Models.ViewModels;
using App.EndPoints.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace App.EndPoints.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserManagmentController : Controller
    {
        private readonly IUserAppService _userAppService;
        private readonly ICategoryAppService _categoryAppService;
        private readonly ILogger<UserManagmentController> _logger;

        public UserManagmentController(IUserAppService userAppService, ILogger<UserManagmentController> logger, ICategoryAppService categoryAppService)
        {
            _userAppService = userAppService;
            _logger = logger;
            _categoryAppService = categoryAppService;
        }

        public async Task<IActionResult> Index(int id, string? search, CancellationToken cancellationToken)
        {
            ViewBag.Search = search;
            var users = await _userAppService.GetAll(id, search, cancellationToken);
            return View(users);

        }

        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var roles = await _userAppService.GetRoles();
            var user = await _userAppService.Get(id);
            ViewBag.Roles = roles.Where(t => !user.Roles.Contains(t.Name)).Select(x => new SelectListItem() { Value = x.Name, Text = x.Name }).ToList();
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(AppUserDto userDTO, string? oldPassword, string? newPassword, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View(userDTO);
            }
            await _userAppService.Update(userDTO, oldPassword, newPassword, cancellationToken);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int userId, CancellationToken cancellationToken)
        {
            await _userAppService.Delete(userId);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Detail(int id, CancellationToken cancellationToken)
        {
            var user = await _userAppService.Get(id);
            return View(user);
        }
        public async Task<IActionResult> EditExpertSkill(int id, CancellationToken cancellationToken)
        {
            var user = await _userAppService.Get(id);
            var categories = await _categoryAppService.GetAll(cancellationToken);
            ViewBag.Categories = categories.Where(t => !user.expertCategories.Any(r => r.Title == t.Title)).Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Title }).ToList();
            ViewBag.UserId = user.Id;
            var userCats = user.expertCategories;
            return View(userCats);
        }
        [HttpPost]
        public async Task<IActionResult> EditExpertSkill(List<int> categories, int userId, CancellationToken cancellationToken)
        {
            await _userAppService.UpdateExpertSkills(userId, categories, cancellationToken);
            return RedirectToAction("Index");
        }
    }
}
