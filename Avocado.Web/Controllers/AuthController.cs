using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Avocado.Domain.Entities;
using Avocado.Infrastructure.Authentication;
using Avocado.Infrastructure.Providers;
using Avocado.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Avocado.Web.Controllers
{
    [Route("api/auth"), ApiController]
    public class AuthController : Controller
    {
        private readonly IJwtFactory _jwtFactory;
        private readonly UserManager<Account> _userManager;
        private readonly IEnumerable<IProvider> _providers;

        public AuthController(IJwtFactory jwtFactory,
            UserManager<Account> userManager,
            IEnumerable<IProvider> providers)
        {
            _jwtFactory = jwtFactory;
            _userManager = userManager;
            _providers = providers;
        }

        [HttpPost]
        public async Task<ActionResult> Token([FromBody, Required] LoginModel model)
        {
            // get provider
            IProvider provider = _providers.FirstOrDefault(p =>
                p.Provider.Equals(model.Provider, StringComparison.InvariantCultureIgnoreCase));
            if (provider == null)
            {
                return BadRequest();
            }

            // get provider key
            string providerKey = await provider.GetProviderKey(model.AccessToken);
            if (string.IsNullOrWhiteSpace(providerKey))
            {
                return BadRequest();
            }

            // get account
            Account account = await _userManager.FindByLoginAsync(provider.Provider, providerKey);
            if (account == null)
            {
                return BadRequest();
            }

            // generate token
            string token = _jwtFactory.GenerateToken(account);
            return Ok(new TokenModel(token));
        }
    }
}