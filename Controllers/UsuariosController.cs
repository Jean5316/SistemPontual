using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using TestePontual.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace SistemPontual.Controllers
{

    [Authorize]
    public class UsuariosController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;

        public UsuariosController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        public async Task<IActionResult> EditarUsuario(string id)
        {
            var user = await _userManager.FindByIdAsync(id);//busca user no banco pelo id e retorno obj do tipo Identity
            if (user == null)
            {
                return NotFound();
            }

            var User = new RegisterViewModel//cria um novo obj do tipo RegisterViewModel e enviar para model
            {
                UserName = user.UserName,
                Email = user.Email,

            };

            return View(User);


        }

        [HttpPost]
        public async Task<IActionResult> EditarUsuario(RegisterViewModel user)
        {
            if (ModelState.IsValid)
            {
                var UserDb = await _userManager.FindByNameAsync(user.UserName);
                UserDb.UserName = user.UserName;
                UserDb.Email = user.Email;
                // UserDb = new IdentityUser
                // {
                //     UserName = user.UserName,
                //     Email = user.Email,
                // };

                // if (UserDb != null)
                // {
                //     ModelState.AddModelError("", "Usuario ja registrado!");
                // }

                var result = await _userManager.UpdateAsync(UserDb);//primeiro atualiza o nome e email
                result = await _userManager.ChangePasswordAsync(UserDb, user.currentPassword, user.Password);//atualiza a senha
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Falha ao Editar Usuario");
                }



            }

            var User = new RegisterViewModel//cria um novo obj do tipo RegisterViewModel e enviar para model
            {
                UserName = user.UserName,
                Email = user.Email,

            };

            return View(User);

        }

        public async Task<IActionResult> ExcluirUsuario(string id)
        {
            var user = await _userManager.FindByIdAsync(id);//busca user no banco pelo id e retorno obj do tipo Identity
            if (user == null)
            {
                return NotFound();
            }

            await _userManager.DeleteAsync(user);
            
            return RedirectToAction("Index", "Usuarios");
        }


    }
}