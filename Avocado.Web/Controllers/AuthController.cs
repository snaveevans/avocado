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
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace Avocado.Web.Controllers
{
    [Route("api/auth"), ApiController]
    [SwaggerTag("Generates JWTs and auth configuration.")]
    public class AuthController : Controller
    {
        private readonly IJwtFactory _jwtFactory;
        private readonly UserManager<Account> _userManager;
        private readonly IEnumerable<IProvider> _providers;
        private readonly FirebaseConfig _firebaseConfig;

        public AuthController(IJwtFactory jwtFactory,
            UserManager<Account> userManager,
            IEnumerable<IProvider> providers,
            IOptions<FirebaseConfig> firebaseConfig)
        {
            _jwtFactory = jwtFactory;
            _userManager = userManager;
            _providers = providers;
            _firebaseConfig = firebaseConfig.Value;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Generates a new JWT.", OperationId = "GenerateToken")]
        [SwaggerResponse(201, "The token was created.", typeof(TokenModel))]
        [SwaggerResponse(400, "Unable to create token.")]
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

        [HttpGet("firebase-config")]
        [SwaggerOperation(Summary = "Gets firebase service account configuration.", OperationId = "FirebaseConfig")]
        [SwaggerResponse(200, "Firebase service account configuration.", typeof(FirebaseConfig))]
        public ActionResult FirebaseConfig()
        {
            return Ok(_firebaseConfig);
        }
    }
}