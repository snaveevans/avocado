using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Avocado.Infrastructure.Authorization
{
    public class LoginService
    {
        private readonly LoginOptions _options;

        public LoginService(LoginOptions options)
        {
            _options = options;
        }
        // register

        // login
        public bool TryLogin(LoginModel model, out string token)
        {
            token = string.Empty;

            if (model.Provider != "password" ||
                model.ProviderId != "tylerjevans" ||
                model.ProviderKey != "american@100")
            {
                return false;
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "tylerjevans"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(_options.Issuer,
            _options.Issuer,
            claims,
            expires: DateTime.Now.AddMilliseconds(_options.MillisecondsUntilExpiration),
            signingCredentials: creds);

            token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return true;
        }
    }
}