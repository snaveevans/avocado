using System;
using System.Linq;
using Avocado.Domain.Entities;
using Avocado.Domain.Interfaces;
using Avocado.Infrastructure.Specifications;
using Microsoft.AspNetCore.Http;

namespace Avocado.Web.Services
{
    public class AccountAccessor : IAccountAccessor
    {
        private readonly IHttpContextAccessor _httpAccessor;
        private readonly IRepository<Account> _accountRepo;
        private Account _account;

        public AccountAccessor(IHttpContextAccessor httpAccessor, IRepository<Account> accountRepo)
        {
            _httpAccessor = httpAccessor;
            _accountRepo = accountRepo;
        }

        public Account Account
        {
            get
            {
                if (_account == null)
                {
                    _account = GetAccount();
                }
                return _account;
            }
        }

        private Account GetAccount()
        {
            var user = _httpAccessor.HttpContext.User;
            if (user == null)
            {
                return null;
            };

            var claims = user.Claims;
            var jtiClaim = claims.FirstOrDefault(c => c.Type == "jti");
            if (jtiClaim == null)
            {
                return null;
            };

            if (!Guid.TryParse(jtiClaim.Value, out Guid accountId))
            {
                return null;
            };

            return _accountRepo.Find(new AccountById(accountId));
        }
    }
}