using iVertion.Domain.Account;
using iVertion.Infra.Data.Context;
using Microsoft.AspNetCore.Identity;
using System;

namespace iVertion.Infra.Data.Identity
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedUserRoleInitial(UserManager<ApplicationUser> userManager,
                                   RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public void SeedUsers()
        {
            if (_userManager.FindByEmailAsync("admin@localhost").Result == null)
            {
                ApplicationUser user = new()
                {
                    UserName = "admin@localhost",
                    Email = "admin@localhost",
                    NormalizedUserName = "ADMIN@LOCALHOST",
                    NormalizedEmail = "ADMIN@LOCALHOST",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    IsEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserProfileId = 1,
                    PersonId = 1
                };

                IdentityResult result = _userManager.CreateAsync(user, "MdRPgW/*-2023").Result;


            }

        }
        public void SeedRoles()
        {
            if (!_roleManager.RoleExistsAsync("GetUsers").Result)
            {
                IdentityRole role = new()
                {
                    Name = "GetUsers",
                    NormalizedName = "GETUSERS"
                };
                _ = _roleManager.CreateAsync(role).Result;
            }
            if (!_roleManager.RoleExistsAsync("AddUser").Result)
            {
                IdentityRole role = new()
                {
                    Name = "AddUser",
                    NormalizedName = "ADDUSER"
                };
                _ = _roleManager.CreateAsync(role).Result;
            }
            if (!_roleManager.RoleExistsAsync("EditUser").Result)
            {
                IdentityRole role = new()
                {
                    Name = "EditUser",
                    NormalizedName = "EDITUSER"
                };
                _ = _roleManager.CreateAsync(role).Result;
            }
            if (!_roleManager.RoleExistsAsync("RemoveUser").Result)
            {
                IdentityRole role = new()
                {
                    Name = "RemoveUser",
                    NormalizedName = "REMOVEUSER"
                };
                _ = _roleManager.CreateAsync(role).Result;
            }
            if (!_roleManager.RoleExistsAsync("AddToRole").Result)
            {
                IdentityRole role = new()
                {
                    Name = "AddToRole",
                    NormalizedName = "ADDTOROLE"
                };
                _ = _roleManager.CreateAsync(role).Result;
            }
            if (!_roleManager.RoleExistsAsync("RemoveFromRole").Result)
            {
                IdentityRole role = new()
                {
                    Name = "RemoveFromRole",
                    NormalizedName = "REMOVEFROMROLE"
                };
                _ = _roleManager.CreateAsync(role).Result;
            }
            if (!_roleManager.RoleExistsAsync("Manager").Result)
            {
                IdentityRole role = new()
                {
                    Name = "Manager",
                    NormalizedName = "MANAGER"
                };
                _ = _roleManager.CreateAsync(role).Result;
            }
            if (!_roleManager.RoleExistsAsync("Dashboard").Result)
            {
                IdentityRole role = new()
                {
                    Name = "Dashboard",
                    NormalizedName = "DASHBOARD"
                };
                _ = _roleManager.CreateAsync(role).Result;
            }
            if (!_roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new()
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                };
                _ = _roleManager.CreateAsync(role).Result;
            }

        }


    }
}