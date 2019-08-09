using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PromotionEventsApp.Extensions;
using PromotionEventsApp.Models;
using PromotionEventsApp.Services.Abstract;
using PromotionEventsApp.ViewModels;

namespace PromotionEventsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MobileController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly IMobileService _mobileService;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public MobileController(ITokenService tokenService, IMobileService mobileService, UserManager<User> userManager, IHttpContextAccessor contextAccessor)
        {
            _tokenService = tokenService;
            _mobileService = mobileService;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        // GET: api/Mobile
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [Authorize]
        [HttpGet("/checkCode/{code}")]
        public async Task<IActionResult> CheckCode(string code)
        {
            var user = await _userManager.FindByIdAsync(_contextAccessor.GetUserId().ToString());
            await _mobileService.CheckCode(code, user);
            return Ok();
        }

        [HttpPost("auth")]
        public async Task<string> Auth([FromBody] LoginViewModel model) => await _tokenService.Auth(model.Email, model.Password);

        // PUT: api/Mobile/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
