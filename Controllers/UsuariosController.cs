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
using TestePontual.Context;

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


            var User = new EditarUsuarioViewModel//cria um novo obj do tipo RegisterViewModel e enviar para model
            {
                UserName = user.UserName,
                Email = user.Email,

            };

            return View(User);


        }




        [HttpPost]
        public async Task<IActionResult> EditarUsuario(EditarUsuarioViewModel Usuario, string id)
        {

            if (ModelState.IsValid)
            {
                var UsuarioDB = _userManager.FindByIdAsync(id).Result;//pega user logado
                if (UsuarioDB == null)
                {
                    return NotFound();
                }

                UsuarioDB.UserName = Usuario.UserName;
                UsuarioDB.Email = Usuario.Email;

                if (Usuario.currentPassword != null)
                {
                    if (_userManager.CheckPasswordAsync(UsuarioDB, Usuario.currentPassword).Result)
                    {

                        await _userManager.ChangePasswordAsync(UsuarioDB, Usuario.currentPassword, Usuario.Password);//atualiza a senha

                    }
                    else
                    {
                        ModelState.AddModelError("", "Senha atual incorreta");
                        return View(Usuario);
                    }
                }

                var result = _userManager.UpdateAsync(UsuarioDB).Result;//primeiro atualiza o nome e email

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Usuarios");
                }
                else
                {
                    ModelState.AddModelError("", "Usuario não atualizado");
                }
            }

            return View(Usuario);



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