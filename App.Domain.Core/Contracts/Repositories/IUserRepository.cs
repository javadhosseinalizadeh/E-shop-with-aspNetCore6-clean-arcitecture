using App.Domain.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contracts.Repositories
{
    public interface IUserRepository
    {
        Task<int> Add(AppUserDto user, string password);
        Task Update(AppUserDto user, string oldPassword, string newPassword);
        Task UpdateExpertSkills(int userId, List<int> categories, CancellationToken cancellationToken);
        Task Delete(int id);
        Task SignInUserById(int id, bool isPersistent);
        Task<int> LoginUser(string userName, string password, bool remember);
        Task SignoutUser();
        Task UpdateProfilePicture(AppUserDto user, CancellationToken cancellationToken);

        //

        Task<List<AppUserDto>> GetAll(int id, string? search, CancellationToken cancellationToken);
        Task<AppUserDto> Get(int id);
        Task<AppUserDto> Get(string username);
        Task<AppUserDto> GetUserByUserName(string username);
        Task<AppUserDto>? GetUserByEmail(string email);
        Task<List<RoleDto>> GetRoles();
        Task<List<ServiceCommentDto>> GetExpertRatingAndComments(int expertId, CancellationToken cancellationToken);
        Task<List<CategoryDto>?> GetExpertSkills(string username, CancellationToken cancellationToken);
        Task<List<string>?> GetUserRoles(string username, CancellationToken cancellationToken);
        Task<List<OrderDto>?> GetUserOrders(string username, CancellationToken cancellationToken);
        Task<List<BidDto>?> GetOrderSuggests(int orderId, CancellationToken cancellationToken);
        Task<List<ServiceCommentDto>?> GetOrderComments(int orderId, CancellationToken cancellationToken);

    }
}
