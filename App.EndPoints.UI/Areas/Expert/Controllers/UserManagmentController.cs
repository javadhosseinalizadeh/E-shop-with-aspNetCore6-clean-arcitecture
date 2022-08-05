using App.Domain.Core.Entities;
using App.EndPoints.UI.Areas.Expert.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.EndPoints.UI.Areas.Expert.Controllers
{
    [Area("Expert")]
    [Authorize(Roles = "Expert")]
    public class UserManagmentController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public UserManagmentController(UserManager<AppUser> userManager,
            RoleManager<IdentityRole<int>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index(string? name, CancellationToken cancellationToken)
        {
            List<ExpertUserMngmntViewModel> model;
            if (string.IsNullOrEmpty(name))
            {
                model = await _userManager.Users.Select(x => new ExpertUserMngmntViewModel
                {
                    Id = x.Id,
                    Name = x.UserName,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                }).ToListAsync(cancellationToken);
            }
            else
            {
                model = await _userManager.Users
                    .Where(x => x.UserName.Contains(name))
                    .Select(x => new ExpertUserMngmntViewModel
                    {
                        Id = x.Id,
                        Name = x.UserName,
                        Email = x.Email,
                        PhoneNumber = x.PhoneNumber,
                    }).ToListAsync(cancellationToken);
            }

            foreach (var user in model)
            {
                user.Roles =
                    await _userManager.GetRolesAsync(await _userManager.Users.FirstAsync(x => x.Id == user.Id));
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeletAccount(int userId)
        {
            var user = await _userManager.Users.FirstAsync(x => x.Id == userId);
            await _userManager.DeleteAsync(user);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int userId)
        {
            var user = await _userManager.Users.FirstAsync(x => x.Id == userId);
            var model = new ExpertUserMngmntViewModel
            {
                Id = user.Id,
                Name = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ExpertUserMngmntViewModel model)
        {
            var user = await _userManager.Users.FirstAsync(x => x.Id == model.Id);
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.UserName = model.Name;
            await _userManager.UpdateAsync(user);
            if (!string.IsNullOrEmpty(model.Password))
            {
                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, model.Password);
            }

            return RedirectToAction("Index");
        }
    }
}
