using System;
using System.Net;
using Microsoft.Extensions.Primitives;
using Avocado.Infrastructure.Authentication;
using Avocado.Infrastructure.Enumerations;
using Avocado.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System.Linq;

namespace Avocado.Web.Controllers
{
    [Route("api/token")]
    public class TokenController : Controller
    {
        private readonly LoginService _loginService;
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _environment;

        public TokenController(LoginService loginService,
            IConfiguration configuration,
            IHostingEnvironment environment)
        {
            _loginService = loginService;
            _configuration = configuration;
            _environment = environment;
        }

        [HttpPost("test-token"), AllowAnonymous]
        public IActionResult TestToken([FromBody] LoginModel model)
        {
            if (!_environment.IsDevelopment())
            {
                return NotFound();
            }

            if (!_loginService.TryLogin(model, out string token))
            {
                return BadRequest();
            }

            return Ok(new { token });
        }

        [HttpGet("google/{mode}"), AllowAnonymous]
        public IActionResult Google(string mode)
        {
            if (!Request.Headers.TryGetValue("Provider-Token", out StringValues providerTokens) || providerTokens.Count > 1)
            {
                return BadRequest();
            }
            var providerToken = providerTokens.First();

            // validate access_token
            var client = new RestClient("https://www.googleapis.com");
            var request = new RestRequest("oauth2/v3/tokeninfo", Method.GET);
            request.AddParameter("access_token", providerToken);
            var response = client.Execute<GoogleResponse>(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return BadRequest();
            }

            var info = response.Data;

            if (info.ClientId != _configuration["GoogleClientId"])
            {
                return BadRequest();
            }

            var model = new RegisterModel
            {
                Provider = Providers.Google.ToString(),
                ProviderId = info.GoogleId
            };

            string token;
            if (mode == "login")
            {
                if (!_loginService.TryLogin(model, out token))
                {
                    return BadRequest();
                }
            }
            else if (mode == "register")
            {
                // get user information
                request = new RestRequest("oauth2/v3/userinfo", Method.GET);
                request.AddParameter("access_token", providerToken);
                var infoResponse = client.Execute<GoogleInformation>(request);
                if (infoResponse.StatusCode != HttpStatusCode.OK)
                {
                    return View(new { error = "failed" });
                }
                var content = infoResponse.Data;

                model.Name = infoResponse.Data.name;
                model.Picture = infoResponse.Data.picture;

                if (!_loginService.TryRegister(model, out token))
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }

            return Ok(new { token });
        }
    }
}