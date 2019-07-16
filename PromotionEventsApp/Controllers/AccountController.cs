using System;
using System.Collections.Generic;
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
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using PromotionEventsApp.Services.Abstract;

namespace PromotionEventsApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly RoleManager<Role> _roleManager;


        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _roleManager = roleManager;
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
                  
                    HttpContext.Session.SetString("JWToken", _tokenService.GenerateToken(_tokenService.GetUserClaims(user)));
                    return RedirectToAction("Index", "Home");





                }



            }

            return View();

        }

        #endregion

        #region Logout
        [Authorize]
        public IActionResult Logout()
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
