using App.Domain.Core.Dtos;
using App.EndPoints.UI.Models;

namespace App.EndPoint.MVC.UI.MappingProfile
{
    public static class UserMapping
    {
        public static AppUserDto MapUserViewModelToUserDto(RegisterViewModel userViewModel)
        {
            var userDto = new AppUserDto()
            {
                UserName = userViewModel.UserName,
                Email = userViewModel.Email,
                FirstName = userViewModel.FirstName,
                LastName = userViewModel.LastName,
                Password = userViewModel.Password,
                ConfirmPassword = userViewModel.ConfirmPassword,
            };
            return userDto;
        }
    }
}
