using System;
using System.Linq;
using Avocado.Domain.Entities;
using Avocado.Domain.Interfaces;
using Avocado.Infrastructure.Specifications;
using Microsoft.AspNetCore.Http;

namespace Avocado.Web.Services
{
    public class AccountService
    {
        private readonly IHttpContextAccessor _httpAccessor;
        private readonly IRepository<Account> _accountRepo;


        public AccountService(IHttpContextAccessor httpAccessor, IRepository<Account> accountRepo)
        {
            _httpAccessor = httpAccessor;
            _accountRepo = accountRepo;
        }

        public bool TryGetCurrentAccount(out Account account)
        {
            var user = _httpAccessor.HttpContext.User;
            if (user == null)
            {
                account = null;
                return false;
            };
            var claims = user.Claims;
            var jtiClaim = claims.FirstOrDefault(c => c.Type == "jti");
            if (jtiClaim == null)
            {
                account = null;
                return false;
            };
            if (!Guid.TryParse(jtiClaim.Value, out Guid accountId))
            {
                account = null;
                return false;
            };

            account = _accountRepo.Find(new FindAccount(accountId));
            return true;
        }
    }
}