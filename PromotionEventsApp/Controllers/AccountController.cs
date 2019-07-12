using System;
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
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace PromotionEventsApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly JWTConfiguration _jWtConfiguration;
        private readonly RoleManager<Role> _roleManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IOptions<JWTConfiguration> jWtConfiguration, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jWtConfiguration = jWtConfiguration.Value;
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
        public async Task<IActionResult> Login(LoginViewModel model)
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
                    var key = Encoding.ASCII.GetBytes(_jWtConfiguration.Secret);
                    var jwToken = new JwtSecurityToken(
                        issuer: "http://localhost:44369/",
                        audience: "http://localhost:44369/",
                        claims: new[] {new Claim(ClaimTypes.Name, user.Id.ToString())},
                        notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                        expires: new DateTimeOffset(DateTime.Now.AddDays(1)).DateTime,
                        //Using HS256 Algorithm to encrypt Token
                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    );
                    var token = new JwtSecurityTokenHandler().WriteToken(jwToken);
                    HttpContext.Session.SetString("JWToken", token);
                    return RedirectToAction("Index", "Home");





                }



            }

            return View();

        }

        #endregion

        #region Logout
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("~/Home/Index");
         
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
                if (!await _roleManager.RoleExistsAsync("User"))
                {
                    await _roleManager.CreateAsync(new Role {Name = "User"});
                }
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
