using App.Domain.Core.Contracts.Repositories;
using App.Domain.Core.Contracts.Services;
using App.Domain.Core.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services
{
    public class UserSerivce : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserFileRepository _userFileRepository;
        private readonly ILogger<UserSerivce> _logger;
        private readonly IHttpContextAccessor _httpContext;
        public UserSerivce(IUserRepository userRepository, IUserFileRepository userFileRepository, ILogger<UserSerivce> logger, IHttpContextAccessor httpContext)
        {
            _userRepository = userRepository;
            _userFileRepository = userFileRepository;
            _logger = logger;
            _httpContext = httpContext;
        }


        public async Task<int> RegisterUser(AppUserDto user, string password)
        {

            var result = await _userRepository.Add(user, password);
            return result;
        }

        public async Task Delete(int id)
        {
            await _userRepository.Delete(id);
        }

        public async Task<AppUserDto> Get(int id)
        {
            var user = await _userRepository.Get(id);
            return user;
        }

        public async Task<List<AppUserDto>> GetAll(int id, string? search, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAll(id, search, cancellationToken);
            return users;
        }


        public async Task<AppUserDto> GetUserByUserName(string username)
        {
            var user = await _userRepository.GetUserByUserName(username);
            return user;
        }

        public async Task<int> LoginUser(string userName, string password, bool remember)
        {
            var result = await _userRepository.LoginUser(userName, password, remember);
            return result;
        }

        public async Task SignInUserById(int id)
        {
            var isPersistent = true;
            await _userRepository.SignInUserById(id, isPersistent);
        }

        public async Task SignoutUser()
        {
            await _userRepository.SignoutUser();
        }

        public async Task Update(AppUserDto user, string oldPassword, string newPassword)
        {
            _logger.LogTrace("Call update {serviceName} for user", "update");
            await _userRepository.Update(user, oldPassword, newPassword);
        }

        public async Task<bool> AddUserFiles(int userId, List<int> files, CancellationToken cancellationToken)
        {
            foreach (var fileId in files)
            {
                UserFileDto userFile = new()
                {
                    FileId = fileId,
                    UserId = userId,
                    IsDeleted = false,
                };
                var id = await _userFileRepository.Add(userFile, cancellationToken);
            }
            return true;
        }

        public async Task<List<RoleDto>> GetRoles()
        {
            var roles = await _userRepository.GetRoles();
            return roles;
        }

        public async Task UpdateExpertSkills(int userId, List<int> categories, CancellationToken cancellationToken)
        {
            await _userRepository.UpdateExpertSkills(userId, categories, cancellationToken);
        }

        public async Task<AppUserDto> GetCurrentUserFullInfo()
        {
            var username = _httpContext.HttpContext.User.Identity.Name;
            var userDto = await _userRepository.GetUserByUserName(username);
            return userDto;
        }

        public async Task UpdateProfilePicture(AppUserDto user, CancellationToken cancellationToken)
        {
            await _userRepository.UpdateProfilePicture(user, cancellationToken);
        }

        public async Task<AppUserDto> GetUserByEmail(string email)
        {
            var userr = await _userRepository.GetUserByEmail(email);
            return userr;
        }

        public async Task<bool> EnsureUserIsNotExist(AppUserDto user, CancellationToken cancellationToken)
        {
            var userByUsername = await _userRepository.GetUserByUserName(user.UserName);
            var userByEmail = await _userRepository.GetUserByEmail(user.Email);
            if (userByUsername != null || userByEmail != null)
                return false;
            return true;
        }

        public async Task<List<ServiceCommentDto>> GetExpertRatingAndComments(int expertId, CancellationToken cancellationToken)
        {
            var comments = await _userRepository.GetExpertRatingAndComments(expertId, cancellationToken);
            return comments;
        }

        public async Task<List<CategoryDto>?> GetExpertSkills(string username, CancellationToken cancellationToken)
        {
            var skills = await _userRepository.GetExpertSkills(username, cancellationToken);
            return skills;
        }

        public async Task<List<string>?> GetUserRoles(string username, CancellationToken cancellationToken)
        {
            var userRoles = await _userRepository.GetUserRoles(username, cancellationToken);
            return userRoles;
        }

        public async Task<List<OrderDto>?> GetUserOrders(string username, CancellationToken cancellationToken)
        {
            var orders = await _userRepository.GetUserOrders(username, cancellationToken);
            return orders;
        }

        public async Task<List<BidDto>?> GetOrderSuggests(int orderId, CancellationToken cancellationToken)
        {
            var suggests = await _userRepository.GetOrderSuggests(orderId, cancellationToken);
            return suggests;
        }

        public async Task<List<ServiceCommentDto>?> GetOrderComments(int orderId, CancellationToken cancellationToken)
        {
            var comments = await _userRepository.GetOrderComments(orderId, cancellationToken);
            return comments;
        }

        public async Task<AppUserDto> GetCurrentUserBriefInfoByUsername(string username, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Get(username);
            return user;
        }
    }
}
