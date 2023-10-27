using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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


        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel()
            {
                ReturnUrl = returnUrl
            });
        }

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
                var result = await _signInManager.PasswordSignInAsync(user, login.Password, false, false);

                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(login.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return Redirect(login.ReturnUrl);
                }
            }


            ModelState.AddModelError("", "Falha ao Logar");
            return View(login);

        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//bloqueia duplicidade de formulario
        public async Task<IActionResult> Register(LoginViewModel registro)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = registro.UserName };
                var result = await _userManager.CreateAsync(user, registro.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    this.ModelState.AddModelError("Registro", "Falha ao registrar usuario!");
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
    }
}
