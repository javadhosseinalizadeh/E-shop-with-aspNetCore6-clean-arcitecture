using App.Domain.Core.Contracts.AppServices;
using App.Domain.Core.Contracts.Services;
using App.Domain.Core.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserAppService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IFileUploadService _fileService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserAppService(IUserService userService, ILogger<UserAppService> logger, IConfiguration configuration, IFileUploadService fileService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _logger = logger;
            _configuration = configuration;
            _fileService = fileService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<int> RegisterUser(AppUserDto user, string password, CancellationToken cancellationToken)
        {
            var ensure = await _userService.EnsureUserIsNotExist(user, cancellationToken);
            if (ensure)
            {
                var userId = await _userService.RegisterUser(user, password);
                return userId;
            }
            else
            {
                return -1;
            }

        }

        public async Task Delete(int id)
        {
            await _userService.Delete(id);
        }

        public async Task<AppUserDto> Get(int id)
        {
            var user = await _userService.Get(id);
            return user;
        }

        public async Task<List<AppUserDto>> GetAll(int id, string? search, CancellationToken cancellationToken)
        {
            var users = await _userService.GetAll(id, search, cancellationToken);
            return users;
        }

        public async Task<AppUserDto> GetUserByUserName(string username)
        {
            var user = await _userService.GetUserByUserName(username);
            return user;
        }

        public async Task<int> LoginUser(string userName, string password, bool remember)
        {
            var result = await _userService.LoginUser(userName, password, remember);
            return result;
        }

        public async Task SignInUserById(int id)
        {
            await _userService.SignInUserById(id);
        }

        public async Task SignoutUser()
        {
            await _userService.SignoutUser();
        }

        public async Task Update(AppUserDto user, string oldPassword, string newPassword, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Call update {appServiceName} for user", "update");
            await _userService.Update(user, oldPassword, newPassword);
        }

        public async Task<List<RoleDto>> GetRoles()
        {
            var roles = await _userService.GetRoles();
            return roles;
        }

        public async Task UpdateExpertSkills(int userId, List<int> categories, CancellationToken cancellationToken)
        {
            await _userService.UpdateExpertSkills(userId, categories, cancellationToken);
        }

        public async Task<AppUserDto> GetCurrentUserFullInfo(CancellationToken cancellationToken)
        {
            var currentUserUsername = _httpContextAccessor.HttpContext.User.Identity.Name;
            var yser = await _userService.GetCurrentUserBriefInfoByUsername(currentUserUsername, cancellationToken);
            var Userorders = await _userService.GetUserOrders(currentUserUsername, cancellationToken);
            yser.UserOrders = Userorders;
            var userRoles = await _userService.GetUserRoles(currentUserUsername, cancellationToken);
            yser.Roles = userRoles;
            if (userRoles.Any(x => x.Contains("Expert") || x.Contains("EXPERT")))
            {
                var skills = await _userService.GetExpertSkills(currentUserUsername, cancellationToken);
                yser.expertCategories = skills;
            }
            if (Userorders != null)
            {
                foreach (var order in Userorders)
                {
                    var suggests = await _userService.GetOrderSuggests(order.Id, cancellationToken);
                    order.Suggests = suggests;
                    var comments = await _userService.GetOrderComments(order.Id, cancellationToken);
                    order.Comments = comments;
                }
            }
            var filrRootPath = _configuration.GetSection("DownloadPath").Value;
            foreach (var order in yser.UserOrders)
            {
                foreach (var file in order.Photos)
                {
                    file.Path = filrRootPath + "/" + file.Path;
                }
            }
            yser.ProfilePicture = filrRootPath + "/" + yser.ProfilePicture;
            return yser;
        }

        public async Task ChangeProfilePicture(IFormFile file, CancellationToken cancellationToken)
        {
            var currentUser = await _userService.GetCurrentUserFullInfo();
            List<IFormFile> files = new();
            files.Add(file);
            await _fileService.DeletePhysicalFile(currentUser.ProfilePicture, cancellationToken);
            var ids = await _fileService.UploadFileAsync(files, cancellationToken);
            var filee = await _fileService.Get(ids[0], cancellationToken);
            currentUser.ProfilePicture = filee.Path;
            await _userService.UpdateProfilePicture(currentUser, cancellationToken);
        }

        public async Task<bool> EnsureUserNameIsNotExist(string username, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByUserName(username);
            if (user != null)
                return false;
            return true;
        }

        public async Task<bool> EnsureEmailIsNotExist(string email, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByEmail(email);
            if (user != null)
                return false;
            return true;
        }

        public async Task<bool> EnsureUserIsNotExist(AppUserDto user, CancellationToken cancellationToken)
        {
            var userByUsername = await _userService.GetUserByUserName(user.UserName);
            var userByEmail = await _userService.GetUserByEmail(user.Email);
            if (userByUsername != null || userByEmail != null)
                return false;
            return true;
        }

        public async Task<List<ServiceCommentDto>> GetExpertRatingAndComments(int expertId, CancellationToken cancellationToken)
        {
            var comments = await _userService.GetExpertRatingAndComments(expertId, cancellationToken);
            return comments;
        }

        public async Task<AppUserDto> GetCurrentUserBriefInfo(CancellationToken cancellationToken)
        {
            var currentUserUsername = _httpContextAccessor.HttpContext.User.Identity.Name;
            var user = await _userService.GetCurrentUserBriefInfoByUsername(currentUserUsername, cancellationToken);
            return user;
        }
    }
}
