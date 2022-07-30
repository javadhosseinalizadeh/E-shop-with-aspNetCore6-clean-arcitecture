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
        public static void Seed(this ModelBuilder builder)
        {

            int ADMIN_ID = 16455435; //"02174cf0–9412–4cfe-afbf-59f706d72cf6";
            int ROLE_ID = 42242345; //"341743f0-asd2–42de-afbf-59kmkkmk72cf6";
            //seed adminRole
            builder.Entity<IdentityRole<int>>()
                .HasData(new IdentityRole<int>
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    Id = ROLE_ID,
                });
            //create user
            var appUser = new AppUser
            {
                Id = ADMIN_ID,
                Email = "thisistest@gmail.com",
                EmailConfirmed = true,
                FirstName = "جواد",
                LastName = "علیزاده",
                UserName = "javadalizadeh",
                NormalizedUserName = "javadalizadeh",
                IsActive = true,
                PictureFileId = 1,
                HomeAddress = "تهران",

            };
            //set user password
            PasswordHasher<AppUser> ph = new PasswordHasher<AppUser>();
            appUser.PasswordHash = ph.HashPassword(appUser, "12345678");
            //seed user
            builder.Entity<AppUser>()
                .HasData(appUser);
            //set user role to the admin
            builder.Entity<IdentityUserRole<int>>()
                .HasData(new IdentityUserRole<int>
                {
                    RoleId = ROLE_ID,
                    UserId = ADMIN_ID,
                });
        }
    }
}
