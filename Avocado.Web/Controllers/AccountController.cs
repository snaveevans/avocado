using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Avocado.Domain.Entities;
using Avocado.Infrastructure.Authentication;
using Avocado.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Avocado.Web.Controllers
{
    [Route("api/account"), ApiController]
    public class AccountController : Controller
    {
        private readonly UserManager<Account> _userManager;

        public AccountController(UserManager<Account> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterModel model)
        {
            Account account = new Account(model.Name, model.Picture);
            IdentityResult result = await _userManager.CreateAsync(account);
            if (result != IdentityResult.Success)
            {
                return BadRequest();
            }

            bool isSuccessful = await AddLogin(account, model);
            if (!isSuccessful)
            {
                // TODO: delete account (they have no way to login)
                return BadRequest();
            }

            return Created("api/account/", account);
        }

        [Authorize, HttpPost("login")]
        public async Task<ActionResult> AddLogin([FromBody, Required] LoginModel model)
        {
            Account account = await _userManager.GetUserAsync(User);
            bool isSuccessful = await AddLogin(account, model);
            if (!isSuccessful)
            {
                return BadRequest();
            }

            return Ok();
        }

        private async Task<bool> AddLogin(Account account, LoginModel model)
        {
            var login = new UserLoginInfo("Google", "foobar", "");
            var result = await _userManager.AddLoginAsync(account, login);
            return result == IdentityResult.Success;
        }
    }
}