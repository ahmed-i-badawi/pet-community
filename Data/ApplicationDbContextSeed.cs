using Microsoft.AspNetCore.Identity;
using PetCommunity.Data.Enums;
using PetCommunity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetCommunity.Data
{
    public static class ApplicationDbContextSeed
    {

        public static async Task<User> SeedUser(UserManager<User> _userManager, User InputUser, string Password, string Role)
        {

            var user = new User
            {
                UserName = InputUser.Email,
                Email = InputUser.Email,
                EmailConfirmed = true,
                Type = UserType.Normal,
                FullName= InputUser.FullName,
            };

            await _userManager.CreateAsync(user, Password);
            await _userManager.AddToRoleAsync(user, Role);

            return user;
        }

        public static async Task SeedRolesAsync(ApplicationDbContext context)
        {

            if (!context.Roles.Any())
            {
                await context.Roles.AddAsync(new IdentityRole { Name = "ADMIN", NormalizedName = "ADMIN", Id = Guid.NewGuid().ToString(), ConcurrencyStamp = Guid.NewGuid().ToString() });
                await context.Roles.AddAsync(new IdentityRole { Name = "DOCTOR", NormalizedName = "DOCTOR", Id = Guid.NewGuid().ToString(), ConcurrencyStamp = Guid.NewGuid().ToString() });
            }
        }

        public static async Task SeedDefaultUsersAsync(UserManager<User> _userManager, ApplicationDbContext context)
        {

            if (!context.Users.Any())
            {

                #region seed Admin with role ADMIN

                var user = new User
                {
                    UserName = "Admin",
                    Email = "Admin@gmail.com",
                    EmailConfirmed = true,
                    Type = UserType.Normal,
                    FullName = "administrator",
                };
                var password = "123@Admin";
                var role = "ADMIN";
                await SeedUser(_userManager, user, password,role);

                #endregion



            }

        }

    }
}
