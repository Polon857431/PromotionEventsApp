using System.Runtime.InteropServices.ComTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PromotionEventsApp.Models;
using PromotionEventsApp.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using System.Text;
using PromotionEventsApp.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;

namespace PromotionEventsApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly AppSettings _appSettings;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IOptions<AppSettings> appSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
        }





        #region Login
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {

            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError(nameof(model.Email), "Nieprawidłowa nazwa użytkownika lub hasło.");

                    return View(model);
                }
                if (!await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    ModelState.AddModelError(nameof(model.Email), "Nieprawidłowa nazwa użytkownika lub hasło.");

                    return View(model);
                }
                else
                { 
                    var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                    var JWToken = new JwtSecurityToken(
                        );
              

                }



            }

            return View();

        }

        #endregion

        #region Logout
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        #endregion

        #region Register
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            if (!ModelState.IsValid)
                return View(model);

            var currentUser = await _userManager.FindByEmailAsync(model.Email);
            if (currentUser != null)
            {
                ModelState.AddModelError(nameof(model.Email), "Ten adres e-mail jest już w bazie. Proszę użyć innego.");
                return View(model);
            }

            User user = new User
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email,
                EmailConfirmed = true
            };

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {

                await _userManager.AddToRoleAsync(user, "User");

                return View("Login");
            }
            else
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
        }
        #endregion
    }

}
