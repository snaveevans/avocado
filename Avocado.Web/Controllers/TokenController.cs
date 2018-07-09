using System;
using System.Net;
using Avocado.Infrastructure.Authorization;
using Avocado.Infrastructure.Enumerations;
using Avocado.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;

namespace Avocado.Web.Controllers
{
    [Route("api/token")]
    public class TokenController : Controller
    {
        private readonly LoginService _loginService;
        private readonly IConfiguration _configuration;

        public TokenController(LoginService loginService, IConfiguration configuration)
        {
            _loginService = loginService;
            _configuration = configuration;
        }

        [HttpPost("testtoken")]
        public IActionResult TestToken([FromBody] LoginModel model)
        {
            if (!_loginService.TryLogin(model, out string token)) { return BadRequest(); }

            return Ok(token);
        }

        [HttpGet("google/{mode}"), AllowAnonymous]
        public IActionResult Google(string mode, string code)
        {
            if (mode != "login" && mode != "register")
            {
                return View(new { error = "failed" });
            }

            // validate code + get access token
            var client = new RestClient("https://www.googleapis.com");
            var request = new RestRequest("oauth2/v4/token", Method.POST);
            request.AddParameter("client_id", _configuration["Authentication:Google:ClientId"]);
            request.AddParameter("client_secret", _configuration["Authentication:Google:ClientSecret"]);
            request.AddParameter("redirect_uri", _configuration["Authentication:Google:RedirectURI"] + $"/{mode}");
            request.AddParameter("grant_type", "authorization_code");
            request.AddParameter("code", code);

            var response = client.Execute<GoogleResponse>(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return View(new { error = "failed" });
            }
            var accessToken = response.Data.access_token;

            // get user information
            request = new RestRequest("oauth2/v3/userinfo", Method.GET);
            request.AddParameter("access_token", accessToken);
            var infoResponse = client.Execute<GoogleInformation>(request);
            if (infoResponse.StatusCode != HttpStatusCode.OK)
            {
                return View(new { error = "failed" });
            }
            var content = infoResponse.Data;

            var model = new RegisterModel
            {
                Name = infoResponse.Data.name,
                Picture = infoResponse.Data.picture,
                Provider = Providers.Google.ToString(),
                ProviderId = content.sub
            };

            if (mode == "login")
            {
                if (_loginService.TryLogin(model, out string token))
                {
                    return View(new { token, accessToken });
                }
            }
            else
            {
                if (_loginService.TryRegister(model, out string token))
                {
                    return View(new { token, accessToken });
                }
            }

            return View(new { error = "failed" });
        }
    }
}