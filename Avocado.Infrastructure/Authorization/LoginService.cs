using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Avocado.Domain.Entities;
using Avocado.Domain.Interfaces;
using Avocado.Infrastructure.Enumerations;
using Avocado.Infrastructure.Specifications;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Avocado.Infrastructure.Authorization
{
    public class LoginService
    {
        private readonly LoginOptions _options;
        private readonly IRepository<Login> _loginRepo;
        private readonly IRepository<Account> _accountRepo;

        public LoginService(IOptions<LoginOptions> options,
            IRepository<Login> loginRepo,
            IRepository<Account> accountRepo)
        {
            _options = options.Value;
            _loginRepo = loginRepo;
            _accountRepo = accountRepo;
        }

        private Login FindLogin(Providers provider, string providerId) => _loginRepo.Find(new FindLogin(provider, providerId));

        // register
        public bool TryRegister(RegisterModel model, out string token)
        {
            if (string.IsNullOrWhiteSpace(model.Name) ||
                string.IsNullOrWhiteSpace(model.Provider) ||
                string.IsNullOrWhiteSpace(model.ProviderId))
            {
                token = string.Empty;
                return false;
            }

            if (!Enum.TryParse<Providers>(model.Provider, true, out Providers provider))
            {
                token = string.Empty;
                return false;
            }

            // look for existing login
            var login = FindLogin(provider, model.ProviderId);
            if (login != null)
            {
                token = string.Empty;
                return false;
            }
            // create account
            var account = new Account(model.Name);
            if (!string.IsNullOrWhiteSpace(model.Picture))
            {
                account.UpdatePicture(model.Picture);
            }
            // create login
            login = new Login(account, provider, model.ProviderId, model.ProviderKey);
            // save account and login
            _accountRepo.Add(account);
            _loginRepo.Add(login);
            // return token
            token = GenerateToken(account);
            return true;
        }

        // add additional login

        // login
        public bool TryLogin(LoginModel model, out string token)
        {
            if (string.IsNullOrWhiteSpace(model.Provider) ||
                string.IsNullOrWhiteSpace(model.ProviderId))
            {
                token = string.Empty;
                return false;
            }
            
            var login = FindLogin(Enum.Parse<Providers>(model.Provider), model.ProviderId);

            if (login == null || login.ProviderKey != model.ProviderKey)
            {
                token = string.Empty;
                return false;
            }

            var account = _accountRepo.Find(new FindAccount(login.AccountId));
            token = GenerateToken(account);

            return true;
        }

        private string GenerateToken(Account account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, account.Name),
                new Claim(JwtRegisteredClaimNames.Jti, account.Id.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(_options.Issuer,
            _options.Issuer,
            claims,
            expires: DateTime.Now.AddMilliseconds(_options.MillisecondsUntilExpiration),
            signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}