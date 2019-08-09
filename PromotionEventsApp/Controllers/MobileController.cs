using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PromotionEventsApp.Services.Abstract;
using PromotionEventsApp.ViewModels;

namespace PromotionEventsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MobileController : Controller
    {
        private readonly ITokenService _tokenService;

        public MobileController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        // GET: api/Mobile
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [Authorize]
        [HttpGet("{code}")]
        public async Task<IActionResult> CheckCode(string code)
        {
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
