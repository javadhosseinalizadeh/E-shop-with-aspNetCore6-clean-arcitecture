using App.Domain.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.InfraStructures.Database.SqlServer.Data
{
    public static class DatabaseSeed
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<IdentityRole>()
                .HasData(new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Admin",
                    NormalizedName = "admin",

                },
            new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "ExpertUser",
                NormalizedName = "admin",

            },
            new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Customer",
                NormalizedName = "customer",

            });
            modelBuilder.Entity<ApplicationUser>()
                .HasData(new ApplicationUser
                {
                    Id = 1,
                    FirstName = "Javad",
                    LastName = "Alizadeh",
                    UserName = "javad.alizadeh",
                    PasswordHash = "ffsfsf",
                    IsActive = true,
                    PhotoPath = "",
                });
        }
    }
}
