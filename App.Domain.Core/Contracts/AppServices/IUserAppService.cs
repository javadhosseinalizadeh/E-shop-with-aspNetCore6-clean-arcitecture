using App.Domain.Core.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contracts.AppServices
{
    public interface IUserAppService
    {
        Task<List<AppUserDto>> GetAll(int id, string? search, CancellationToken cancellationToken);
        Task<AppUserDto> Get(int id);
        Task<AppUserDto> GetUserByUserName(string username);
        Task<int> RegisterUser(AppUserDto user, string password, CancellationToken cancellationToken);
        Task Update(AppUserDto user, string oldPassword, string newPassword, CancellationToken cancellationToken);
        Task UpdateExpertSkills(int userId, List<int> categories, CancellationToken cancellationToken);
        Task<AppUserDto> GetCurrentUserFullInfo(CancellationToken cancellationToken);
        Task<AppUserDto> GetCurrentUserBriefInfo(CancellationToken cancellationToken);
        Task Delete(int id);
        Task SignInUserById(int id);
        Task SignoutUser();
        Task<int> LoginUser(string userName, string password, bool remember);
        Task<List<RoleDto>> GetRoles();
        Task ChangeProfilePicture(IFormFile file, CancellationToken cancellationToken);
        Task<bool> EnsureUserIsNotExist(AppUserDto user, CancellationToken cancellationToken);
        Task<bool> EnsureUserNameIsNotExist(string username, CancellationToken cancellationToken);
        Task<bool> EnsureEmailIsNotExist(string email, CancellationToken cancellationToken);
        Task<List<ServiceCommentDto>> GetExpertRatingAndComments(int expertId, CancellationToken cancellationToken);
    }
}
