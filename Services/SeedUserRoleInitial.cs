using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TestePontual.ViewModels;

namespace TestePontual.Services
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedUserRoleInitial(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void SeedRoles()//metodo para criar os perfis
        {
            if (!_roleManager.RoleExistsAsync("Tecnico").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Tecnico";
                role.NormalizedName = "Tecnico";
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;


            }

            if (!_roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                role.NormalizedName = "ADMIN";
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
            }


        }

        public void SeedUsers()//metodo para criar os usuarios
        {
            if (_userManager.FindByNameAsync("ADMIN").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "Admin";
                user.Email = "admin@admin.com";
                user.NormalizedUserName = "ADMIN";
                user.NormalizedEmail = "ADMIN@ADMIN.COM";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _userManager.CreateAsync(user, "123456").Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Admin").Wait();
                }

            }
        }

        
    }
}