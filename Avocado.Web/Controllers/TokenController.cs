using System;
using Avocado.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Avocado.Web.Controllers
{
    [Route("api/token")]
    public class TokenController : Controller
    {
        private readonly LoginService _loginService;
        
        public TokenController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginModel model)
        {
            if (!_loginService.TryLogin(model, out string token))
            {
                return BadRequest("Could not create token");
            }

            return Ok(token);
        }
    }
}