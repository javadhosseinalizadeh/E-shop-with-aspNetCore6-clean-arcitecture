using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool IsActive { get; set; }
        public string? ProfilePicture { get; set; }
        public string? HomeAddress { get; set; }
        public virtual List<AppFile>? AppFiles { get; set; }
        public virtual List<ExpertFavoriteCategory>? ExpertFavoriteCategories { get; set; }
        public virtual List<Bid>? Bids { get; set; }
        public virtual List<Order>? CustomerOrders { get; set; }
        public virtual List<Order>? ExpertOrders { get; set; }
        public virtual List<UserFile> UserFiles { get; set; }

    }
}
