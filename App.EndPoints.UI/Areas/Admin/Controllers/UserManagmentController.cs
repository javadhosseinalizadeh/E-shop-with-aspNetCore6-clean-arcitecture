using App.Domain.Core.Entities;
using App.EndPoints.UI.Areas.Admin.Models.ViewModels;
using App.EndPoints.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.EndPoints.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
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
            List<UserMangmntVM> model;
            if (string.IsNullOrEmpty(name))
            {
                model = await _userManager.Users.Select(x => new UserMangmntVM
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
                    .Select(x => new UserMangmntVM
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
     
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                    }
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Remove(int userId)
        {
            var user = await _userManager.Users.FirstAsync(x => x.Id == userId);
            await _userManager.DeleteAsync(user);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int userId)
        {
            var user = await _userManager.Users.FirstAsync(x => x.Id == userId);
            var model = new UserMangmntVM
            {
                Id = user.Id,
                Name = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserMangmntVM model)
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


        #region Roles

        [HttpGet]
        public async Task<IActionResult> EditRoles(string roleId)
        {
            ViewBag.roleId = roleId;
            var role = await _roleManager.FindByIdAsync(roleId);
            if(role == null)
            {
                ViewBag.ErrorMessage = $"Role with id = {roleId} cannot be found";
                    return View("پیدا نشد");
            }
            var model = new List<UserRoleViewModel>();
            foreach (var user in _userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                };
                if(await _userManager.IsInRoleAsync(user , role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }
                model.Add(userRoleViewModel);
            }
            return View(model);
            //var user = await _userManager.Users.FirstAsync(x => x.Id == roleId);
            //var model = new UserMangmntVM
            //{
            //    Id = user.Id,
            //    Name = user.UserName,
            //    Email = user.Email,
            //    PhoneNumber = user.PhoneNumber,
            //    Roles = await _userManager.GetRolesAsync(user)
            //};
            //ViewBag.Roles = await _roleManager.Roles.Select(x => x.Name).ToListAsync();

          //  return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRoles(List<UserRoleViewModel> model, string roleId)
        {
            return View();
        }


        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);

        }
        #endregion
    }
}
