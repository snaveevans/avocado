using Avocado.Infrastructure.Authorization;
using Avocado.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Avocado.Web.Controllers
{
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly LoginService _loginService;
        
        public AccountController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                return BadRequest("name");
            }
            
            if(!_loginService.TryRegister(model.Name, model, out string token))
            {
                return BadRequest();
            }

            return Ok(token);
        }
    }
}