using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Avocado.Domain.Entities;
using Avocado.Infrastructure.Authentication;
using Avocado.Infrastructure.Providers;
using Avocado.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Avocado.Web.Controllers
{
    [Route("api/account"), ApiController]
    [SwaggerTag("Register, read, update, and delete Accounts.")]
    public class AccountController : Controller
    {
        private readonly UserManager<Account> _userManager;
        private readonly IEnumerable<IProvider> _providers;

        public AccountController(UserManager<Account> userManager,
            IEnumerable<IProvider> providers)
        {
            _userManager = userManager;
            _providers = providers;
        }

        [HttpPost("register")]
        [SwaggerOperation(Summary = "Registers a new account.", OperationId = "RegisterAccount")]
        [SwaggerResponse(201, "The account was created.", typeof(Account))]
        [SwaggerResponse(400, "Unable to create account.")]
        public async Task<ActionResult> Register(
            [FromBody, Required]
            [SwaggerParameter("Account and login values.")]
                RegisterModel model)
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
                await _userManager.DeleteAsync(account);
                return BadRequest();
            }

            return Created("api/account/", account);
        }

        [Authorize, HttpPost("login")]
        [SwaggerOperation(Summary = "Adds a new login to the account.", OperationId = "AddLogin")]
        [SwaggerResponse(204, "The login was created.")]
        [SwaggerResponse(400, "Unable to create login.")]
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
            // get provider
            IProvider provider = _providers.FirstOrDefault(p =>
                p.Provider.Equals(model.Provider, StringComparison.InvariantCultureIgnoreCase));
            if (provider == null)
            {
                return false;
            }

            // get provider key
            string providerKey = await provider.GetProviderKey(model.AccessToken);
            if (string.IsNullOrWhiteSpace(providerKey))
            {
                return false;
            }

            // add login
            var login = new UserLoginInfo(provider.Provider, providerKey, string.Empty);
            var result = await _userManager.AddLoginAsync(account, login);
            return result == IdentityResult.Success;
        }
    }
}