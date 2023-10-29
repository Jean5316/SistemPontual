using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TestePontual.ViewModels;

namespace TestePontual.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;



        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel()
            {
                ReturnUrl = returnUrl

            });
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var user = await _userManager.FindByNameAsync(login.UserName);


            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, login.Password, false, false);//entrar no sistema

                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(login.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }

                    return Redirect(login.ReturnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Usuario ou senha Invalidos");
                    return View(login);

                }
            }


            ModelState.AddModelError("", "Usuario não registrado");
            return View();

        }
[Authorize("Admin")]
        //Carrega page Register
        public IActionResult Register()
        {
            return View();
        }

[Authorize("Admin")]
        //Carrega page Register POST
        [HttpPost]
        [ValidateAntiForgeryToken]//bloqueia duplicidade de formulario
        public async Task<IActionResult> Register(RegisterViewModel registro)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(registro.UserName);
                if (user != null)
                {
                    ModelState.AddModelError("", "Usuario ja registrado!");
                }

                user = new IdentityUser
                {
                    UserName = registro.UserName,
                    Email = registro.Email,
                };

                var result = await _userManager.CreateAsync(user, registro.Password);

                if (result.Succeeded)
                {
                    //await _signInManager.SignInAsync(user, false);
                    await _userManager.AddToRoleAsync(user, "Member");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Falha ao registrar usuario!");
                }
            }
            return View(registro);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.User = null;
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            ViewBag.Titulo = "Acess Denied";
            return View();
        }
    }
}
