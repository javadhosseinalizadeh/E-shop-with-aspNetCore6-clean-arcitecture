using App.Domain.Core.Contracts.Repositories;
using App.Domain.Core.Contracts.Services;
using App.Domain.Core.Dtos;
using App.Domain.Core.Entities;
using App.InfraStructures.Database.SqlServer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.InfraStructures.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly ILogger<UserRepository> _logger;
        private readonly AppDbContext _appDbContext;

        public UserRepository(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ILogger<UserRepository> logger, AppDbContext appDbContext, RoleManager<IdentityRole<int>> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _appDbContext = appDbContext;
            _roleManager = roleManager;
        }

        public async Task<int> Add(AppUserDto user, string password)
        {
            try
            {
                var newUser = new AppUser();
                newUser.UserName = user.UserName;
                newUser.Email = user.Email;
                newUser.FirstName = user.FirstName;
                newUser.LastName = user.LastName;
                newUser.HomeAddress = user.HomeAddress;
                var result = await _userManager.CreateAsync(newUser, password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, "Custommer");
                    _logger.LogInformation("new user {username} is {action} successfully", newUser.UserName, "create");
                }
                return newUser.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "فرایند {عملیات} کاربر با خطا روبه رو شد", "ثبت نام");
                throw new Exception("فرایند ثبت نام کاربر با خطا روبه رو شد");
            }
        }



        public async Task Delete(int id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id.ToString());
                await _userManager.DeleteAsync(user);
                _logger.LogInformation("user with id {id} is {action} successfully", id, "delete");
            }
            catch (Exception ex)
            {
                throw new Exception("امکان حذف به دلیل استفاده شناسه وجود ندارد", ex.InnerException);
            }

        }

        public async Task<int> LoginUser(string userName, string password, bool remember)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var result = await _userManager.CheckPasswordAsync(user, password);
            if (result)
            {
                _logger.LogInformation("{username} {action} successfully", userName, "login");
                await _signInManager.SignInAsync(user, remember);
                return user.Id;
            }
            else
            {
                _logger.LogWarning("{username} {action} process failed", userName, "login");
                return 0;
            }

        }

        public async Task SignInUserById(int id, bool isPersistent)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            _logger.LogInformation("کاربری با شناسه کاربری {آی دی} {عملیات} سایت شد", id, "وارد");
            await _signInManager.SignInAsync(user, isPersistent);
        }

        public async Task SignoutUser()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task Update(AppUserDto user, string oldPassword, string newPassword)
        {
            var user1 = await _userManager.FindByIdAsync(user.Id.ToString());
            if (!string.IsNullOrWhiteSpace(newPassword))
            {
                var result = await _userManager.CheckPasswordAsync(user1, oldPassword);
                if (result)
                {
                    await _userManager.ChangePasswordAsync(user1, oldPassword, newPassword);
                    _logger.LogInformation("رمز عبور کاربر {نام کاربری} با موفقیت {عملیات} شد", user.UserName, "به روز رسانی");
                }
                else
                {
                    _logger.LogWarning("خطایی در {عملیات} رمز عبور کاربر {نام کاربری} رخ داد", "به روز رسانی", user.UserName);
                }

            }
            user1.Email = user.Email;
            user1.FirstName = user.FirstName;
            user1.LastName = user.LastName;
            user1.HomeAddress = user.HomeAddress;
            var roles = await _userManager.GetRolesAsync(user1);
            await _userManager.RemoveFromRolesAsync(user1, roles);
            await _userManager.AddToRolesAsync(user1, user.Roles);
            await _userManager.UpdateAsync(user1);
            _logger.LogInformation("{عملیات} مشخصات کاربر با نام کاربری {نام کاربری} با موفقیت انجام شد", "به روز رسانی", user.UserName);

        }

        public async Task UpdateExpertSkills(int userId, List<int> categories, CancellationToken cancellationToken)
        {
            var result = await _appDbContext.ExpertFavoriteCategories.Where(c => c.ExpertUserId == userId).ToListAsync();

            foreach (var item in categories)
            {
                if (!result.Select(x => x.CategoryId).Contains(item))
                {
                    ExpertFavoriteCategory skill = new()
                    {
                        CategoryId = item,
                        ExpertUserId = userId,
                    };
                    await _appDbContext.ExpertFavoriteCategories.AddAsync(skill, cancellationToken);
                }
            }
            await _appDbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("{عملیات} مشخصات کاربر با شناسه کاربری {شناسه کاربری} با موفقیت انجام شد", "به روز رسانی مهارت", userId);
        }

        public async Task UpdateProfilePicture(AppUserDto user, CancellationToken cancellationToken)
        {
            var yser = await _userManager.FindByIdAsync(user.Id.ToString());
            yser.ProfilePicture = user.ProfilePicture;
            await _userManager.UpdateAsync(yser);
            _logger.LogInformation("{عملیات} مشخصات کاربر با نام کاربری {نام کاربری} با موفقیت انجام شد", "به روز رسانی تصویر پروفایل", user.UserName);
        }

        public async Task<AppUserDto> Get(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            AppUserDto userDto = new()
            {
                Id = user.Id,
                HomeAddress = user.HomeAddress,
                FirstName = user.FirstName,
                Email = user.Email,
                LastName = user.LastName,

                ProfilePicture = user.ProfilePicture,
                UserName = user.UserName
            };
            return userDto;
        }

        public async Task<List<AppUserDto>> GetAll(int id, string? search, CancellationToken cancellationToken)
        {
            var fff = _appDbContext.Orders;
            var users = await _userManager.Users.Select(x => new AppUserDto()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                HomeAddress = x.HomeAddress,
                Email = x.Email,
                LastName = x.LastName,
                ProfilePicture = x.ProfilePicture,
                UserName = x.UserName,
            }).ToListAsync(cancellationToken);

            foreach (var item in users)
            {
                var user = await _userManager.FindByIdAsync(item.Id.ToString());
                var roles = await _userManager.GetRolesAsync(user);
                item.Roles = roles.ToList();

                item.expertCategories =
                    await _appDbContext.ExpertFavoriteCategories
                    .Where(x => x.ExpertUserId == item.Id)
                    .Select(x => x.Category)
                    .Select(x => new CategoryDto()
                    {
                        Id = x.Id,
                        Title = x.Title
                    }).ToListAsync(cancellationToken);
            }
            if (string.IsNullOrWhiteSpace(search))
            {
                //s.ForEach(async x=>await _userManager.GetRolesAsync(x))
                //var userrole = await _userManager.GetRolesAsync(await _userManager.Users.FirstAsync(x => x.Id == item.Id));
                if (id == 1)
                {
                    users = users.Where(x => x.Roles.Count == 1).ToList();
                    return users;
                }
                if (id == 2)
                {
                    users = users.Where(x => x.Roles.Count == 2).ToList();
                    return users;
                }
                return users;
            }
            else
            {
                users = users
                    .Where(x => x.UserName.Contains(search) ||
                    x.FirstName.Contains(search) ||
                    x.LastName.Contains(search) ||
                    x.Email.Contains(search))
                    .ToList();
                return users;
            }

        }

        public async Task<List<RoleDto>> GetRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var roleDtos = roles.Select(x => new RoleDto() { Id = x.Id, Name = x.Name, }).ToList();
            return roleDtos;
        }

        public async Task<AppUserDto>? GetUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return null;
            else
            {
                AppUserDto? userDto = new AppUserDto()
                {
                    HomeAddress = user.HomeAddress,
                    FirstName = user.FirstName,
                    Email = user.Email,
                    Id = user.Id,
                    LastName = user.LastName,
                    UserName = user.UserName,
                };
                return userDto;
            }
        }

        public async Task<AppUserDto> Get(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            AppUserDto userDto = new()
            {
                Id = user.Id,
                HomeAddress = user.HomeAddress,
                FirstName = user.FirstName,
                Email = user.Email,
                LastName = user.LastName,
                ProfilePicture = user.ProfilePicture,
                UserName = user.UserName,

            };
            var roles = await _userManager.GetRolesAsync(user);
            userDto.Roles = roles.ToList();
            userDto.expertCategories = await _appDbContext.ExpertFavoriteCategories
                    .Where(x => x.ExpertUserId == userDto.Id)
                    .Select(x => x.Category)
                    .Select(x => new CategoryDto()
                    {
                        Id = x.Id,
                        Title = x.Title
                    }).ToListAsync();
            return userDto;
        }
        public async Task<List<CategoryDto>?> GetExpertSkills(string username, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return null;
            else
            {
                var skills = await _appDbContext.ExpertFavoriteCategories
                        .Where(x => x.ExpertUserId == user.Id)
                        .Select(x => x.Category)
                        .Select(x => new CategoryDto()
                        {

                            Id = x.Id,
                            Title = x.Title
                        }).ToListAsync();
                return skills;
            }
        }
        public async Task<List<string>?> GetUserRoles(string username, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return null;
            else
            {
                var roles = await _userManager.GetRolesAsync(user);
                return roles.ToList();
            }
        }
        public async Task<List<OrderDto>?> GetUserOrders(string username, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return null;
            else
            {
                var orders = await _appDbContext.Orders.Where(x => x.CustomerId == user.Id).Select(x => new OrderDto()
                {
                    Id = x.Id,
                    Description = x.Description,
                    FinalPrice = x.FinalPrice,
                    ConfirmedExpertId = x.ConfirmedExpertId,
                    CreationDate = x.CreationDate,
                    ShamsiCreationDate = x.CreationDate.ToShamsi(),
                    CustomerId = x.CustomerId,
                    IsConfirmedByCustomer = x.IsConfirmedByCustomer,
                    StatusId = x.StatusId,
                    ServiceId = x.ServiceId,
                    IsDeleted = x.IsDeleted,
                    CategoryId = x.Service.CategoryId,
                    StatusName = x.Status.Name,
                    StatusValue = x.Status.StatusValue,
                    CustomerName = x.Customer.UserName,
                    ExpertName = x.Expert.UserName,
                    ServiceName = x.Service.Title,
                    HomeAddress = x.Customer.HomeAddress,
                }).ToListAsync();
                return orders;
            }
        }
        public async Task<List<BidDto>?> GetOrderSuggests(int orderId, CancellationToken cancellationToken)
        {
            var suggests = await _appDbContext.Bids
                .Where(s => s.OrderId == orderId)
                .Select(h => new BidDto()
                {
                    Id = h.Id,
                    CreationDate = h.CreationDate,
                    ExpertId = h.ExpertId,
                    IsConfirmedByCustomer = h.IsConfirmedByCustomer,
                    OrderId = h.OrderId,
                    SuggestedPrice = h.SuggestedPrice,
                    ExpertName = h.Expert.UserName,
                }).ToListAsync(cancellationToken);
            if (suggests == null)
                return null;
            else
                return suggests;

        }
        public async Task<List<ServiceCommentDto>?> GetOrderComments(int orderId, CancellationToken cancellationToken)
        {
            var comments = await _appDbContext.ServiceComments
                .Where(c => c.OrderId == orderId)
                .Select(h => new ServiceCommentDto()
                {
                    CreationDate = h.CreationDate,
                    ShamsiCreationDate = h.CreationDate.ToShamsi(),
                    Description = h.Description,
                    Id = h.Id,
                    IsApproved = h.IsApproved,
                    OrderId = h.OrderId,
                    //ServiceId = x.ServiceId,
                    Title = h.Title,
                    IsWriteByCustomer = h.IsWriteByCustomer
                }).ToListAsync(cancellationToken);
            if (comments == null)
                return null;
            else
                return comments;
        }


        public async Task<AppUserDto> GetUserByUserName(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return null;
            else
            {
                AppUserDto userDto = new()
                {
                    Id = user.Id,
                    HomeAddress = user.HomeAddress,
                    FirstName = user.FirstName,
                    Email = user.Email,
                    LastName = user.LastName,
                    ProfilePicture = user.ProfilePicture,
                    UserName = user.UserName,

                };
                var roles = await _userManager.GetRolesAsync(user);
                userDto.Roles = roles.ToList();
                if (roles.Count == 1 && roles.Contains("Customer"))
                    _logger.LogInformation("کاربر مورد نظر {نقش} می باشد", "مشتری");
                if (roles.Count == 2 && roles.Contains("Customer") && roles.Contains("Expert"))
                    _logger.LogInformation("کاربر مورد نظر {نقش} می باشد", "متخصص");
                if (roles.Contains("Admin"))
                    _logger.LogInformation("کاربر مورد نظر {نقش} می باشد", "ادمین");

                userDto.expertCategories = await _appDbContext.ExpertFavoriteCategories
                        .Where(x => x.ExpertUserId == userDto.Id)
                        .Select(x => x.Category)
                        .Select(x => new CategoryDto()
                        {

                            Id = x.Id,
                            Title = x.Title
                        }).ToListAsync();

                userDto.UserOrders = await _appDbContext.Orders.Where(x => x.CustomerId == userDto.Id).Select(x => new OrderDto()
                {
                    Id = x.Id,
                    Description = x.Description,
                    FinalPrice = x.FinalPrice,
                    ConfirmedExpertId = x.ConfirmedExpertId,
                    CreationDate = x.CreationDate,
                    ShamsiCreationDate = x.CreationDate.ToShamsi(),
                    CustomerId = x.CustomerId,
                    IsConfirmedByCustomer = x.IsConfirmedByCustomer,
                    StatusId = x.StatusId,
                    ServiceId = x.ServiceId,
                    IsDeleted = x.IsDeleted,
                    CategoryId = x.Service.CategoryId,
                    StatusName = x.Status.Name,
                    StatusValue = x.Status.StatusValue,
                    CustomerName = x.Customer.UserName,
                    ExpertName = x.Expert.UserName,
                    ServiceName = x.Service.Title,
                    HomeAddress = x.Customer.HomeAddress,
                    Suggests = x.ExpertSuggests.Select(h => new BidDto()
                    {
                        Id = h.Id,
                        CreationDate = h.CreationDate,
                        Description = h.Description,
                        ExpertId = h.ExpertId,
                        IsConfirmedByCustomer = h.IsConfirmedByCustomer,
                        OrderId = h.OrderId,
                        SuggestedPrice = h.SuggestedPrice,
                        ExpertName = h.Expert.UserName,
                    }).ToList(),
                    Comments = x.Comments.Select(h => new ServiceCommentDto()
                    {
                        CreationDate = h.CreationDate,
                        ShamsiCreationDate = h.CreationDate.ToShamsi(),
                        Description = h.Description,
                        Id = h.Id,
                        IsApproved = h.IsApproved,
                        OrderId = h.OrderId,
                        //ServiceId = x.ServiceId,
                        Title = h.Title,
                        IsWriteByCustomer = h.IsWriteByCustomer,
                        ServiceId = x.ServiceId,
                    }).ToList(),
                    Photos = x.OrderFiles.Select(f => f.File).Select(z => new AppFileDto()
                    {
                        CreationDate = z.CreationDate,
                        Id = z.Id,
                        Path = z.Path,
                    }).ToList(),

                }).ToListAsync();
                return userDto;
            }


        }

        public async Task<List<ServiceCommentDto>> GetExpertRatingAndComments(int expertId, CancellationToken cancellationToken)
        {
            var comments = await _appDbContext.ServiceComments
                .Where(c => c.Order.ConfirmedExpertId == expertId)
                .Select(v => new ServiceCommentDto()
                {
                    Description = v.Description,
                    ShamsiCreationDate = v.CreationDate.ToShamsi(),
                    Id = v.Id,
                    IsApproved = v.IsApproved,
                    IsWriteByCustomer = v.IsWriteByCustomer,
                    OrderId = v.OrderId,
                    Title = v.Title,
                })
                .ToListAsync();
            return comments;
        }
    }
}
