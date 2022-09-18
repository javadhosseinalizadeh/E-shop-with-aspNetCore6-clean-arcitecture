
using App.Domain.Core.Contracts.AppServices;
using App.Domain.Core.Dtos;
using App.EndPoint.MVC.UI.MappingProfile;
using App.EndPoints.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App.EndPoints.UI.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IUserAppService _userAppService;
        private readonly ICategoryAppService _categoryAppService;

        public AccountController(IUserAppService userAppService, ICategoryAppService categoryAppService)
        {
            _userAppService = userAppService;
            _categoryAppService = categoryAppService;
        }

        public async Task<IActionResult> Profile(CancellationToken cancellationToken)
        {
            var user = await _userAppService.GetCurrentUserFullInfo( cancellationToken);
            return View(user);
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AppUserDto user, bool remember)
        {
            var result = await _userAppService.LoginUser(user.UserName, user.Password, remember);


            if (result == 0)
            {
                return RedirectToAction(nameof(Login));
            }
            else
            {
                var loginUser = await _userAppService.Get(result);
                if (loginUser.Roles.Count == 1)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View("PanelSelect", loginUser.Roles);
                }
            }

        }


        public async Task<IActionResult> Logout()
        {
            await _userAppService.SignoutUser();
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel userModel, CancellationToken cancellationToken)

        {
            if (!ModelState.IsValid)
                return View(userModel);

            var userDTO = UserMapping.MapUserViewModelToUserDto(userModel);
            var result = await _userAppService.EnsureUserIsNotExist(userDTO, cancellationToken);
            if (!result)
            {
                ModelState.AddModelError("exist", "نام کاربری یا ایمیل تکراری است");
                return View(userModel);
            }
            var id = await _userAppService.RegisterUser(userDTO, userDTO.Password, cancellationToken);
            await _userAppService.SignInUserById(id);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public async Task<bool> EnsureEmailIsNotExist(string Email, CancellationToken cancellationToken)
        {
            return await _userAppService.EnsureEmailIsNotExist(Email, cancellationToken);
        }
        public async Task<bool> EnsureUserNameIsNotExist(string Username, CancellationToken cancellationToken)
        {
            return await _userAppService.EnsureUserNameIsNotExist(Username, cancellationToken);
        }

        public async Task<IActionResult> EditProfile(CancellationToken cancellationToken)
        {
            var user = await _userAppService.GetCurrentUserFullInfo(cancellationToken);
            var categories = await _categoryAppService.GetAll(cancellationToken);
            var filteredCategories = categories.Where(t => !user.expertCategories
                .Any(r => r.Title == t.Title))
                .Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Title })
                .ToList();
            ViewBag.Categories = filteredCategories;
            return View(user);

        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(AppUserDto userDTO, List<int>? categories, string? oldPassword, string? newPassword, CancellationToken cancellationToken)
        {
            ModelState.Remove("Password");
            ModelState.Remove("ConfirmPassword");
            if (!ModelState.IsValid)
            {
                return View(userDTO);
            }
            await _userAppService.Update(userDTO, oldPassword, newPassword, cancellationToken);
            await _userAppService.UpdateExpertSkills(userDTO.Id, categories, cancellationToken);
            return RedirectToAction(nameof(Profile));
        }

        [HttpPost]
        public async Task<IActionResult> ChangeProfilePicture(IFormFile? file, CancellationToken cancellationToken)
        {
            await _userAppService.ChangeProfilePicture(file, cancellationToken);
            return RedirectToAction("Profile", "Account");
        }

        public async Task<IActionResult> ExpertRating(int id, CancellationToken cancellationToken)
        {
            var comments = await _userAppService.GetExpertRatingAndComments(id, cancellationToken);
            return View(comments);
        }


    }
}
