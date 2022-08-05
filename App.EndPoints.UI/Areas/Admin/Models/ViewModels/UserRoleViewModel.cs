namespace App.EndPoints.UI.Areas.Admin.Models.ViewModels
{
    public class UserRoleViewModel
    {
        public int UserId { get; set; }
        public string RoleId { get; set; }
        public string UserName { get; set; }
        public bool IsSelected { get; set; }
    }
}
