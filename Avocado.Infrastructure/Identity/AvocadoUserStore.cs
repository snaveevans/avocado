using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avocado.Domain.Entities;
using Avocado.Domain.Interfaces;
using Avocado.Domain.Specifications.Accounts;
using Avocado.Infrastructure.Authentication;
using Avocado.Infrastructure.Specifications;
using Microsoft.AspNetCore.Identity;

namespace Avocado.Infrastructure.Identity
{
    public class AvocadoUserStore : IUserStore<Account>, IUserLoginStore<Account>
    {
        private readonly IRepository<Account> _accountRepo;
        private readonly IRepository<Login> _loginRepo;
        public AvocadoUserStore(IRepository<Account> accountRepo, IRepository<Login> loginRepo)
        {
            _accountRepo = accountRepo;
            _loginRepo = loginRepo;
        }

        public async Task AddLoginAsync(Account user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            var avocadoLogin = new Login(user, login.LoginProvider, login.ProviderKey);
            _loginRepo.Add(avocadoLogin);
        }

        public async Task<IdentityResult> CreateAsync(Account user, CancellationToken cancellationToken)
        {
            _accountRepo.Add(user);

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(Account user, CancellationToken cancellationToken)
        {
            var isDeleted = _accountRepo.Remove(user);
            if (isDeleted)
            {
                return IdentityResult.Success;
            }

            return IdentityResult.Failed(new IdentityError { Description = $"Could not delete user {user.Id}" });
        }

        public void Dispose()
        {
        }

        public async Task<Account> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            Guid id;
            if (!Guid.TryParse(userId, out id))
            {
                return null;
            }

            Account account = _accountRepo.Find(new AccountById(id));
            return account;
        }

        public async Task<Account> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            Login login = _loginRepo.Find(new LoginByProvider(loginProvider, providerKey));
            if (login == null)
            {
                return null;
            }
            Account account = _accountRepo.Find(new AccountById(login.AccountId));
            return account;
        }

        public async Task<Account> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return _accountRepo.Find(new AccountByUserName(normalizedUserName));
        }

        public async Task<IList<UserLoginInfo>> GetLoginsAsync(Account user, CancellationToken cancellationToken)
        {
            var logins = _loginRepo.Query(new LoginsForAccount(user));
            var userLoginInfos = logins.Select(l => new UserLoginInfo(l.Provider, l.ProviderKey, string.Empty));
            return userLoginInfos.ToList(); ;
        }

        public async Task<string> GetNormalizedUserNameAsync(Account user, CancellationToken cancellationToken)
        {
            return user.NormalizedUserName;
        }

        public async Task<string> GetUserIdAsync(Account user, CancellationToken cancellationToken)
        {
            return user.Id.ToString();
        }

        public async Task<string> GetUserNameAsync(Account user, CancellationToken cancellationToken)
        {
            return user.UserName;
        }

        public Task RemoveLoginAsync(Account user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task SetNormalizedUserNameAsync(Account user, string normalizedName, CancellationToken cancellationToken)
        {
            user.UpdateNormalizedUserName(normalizedName);
        }

        public async Task SetUserNameAsync(Account user, string userName, CancellationToken cancellationToken)
        {
            user.UpdateUserName(userName);
        }

        public async Task<IdentityResult> UpdateAsync(Account user, CancellationToken cancellationToken)
        {
            _accountRepo.Update(user);

            return IdentityResult.Success;
        }
    }
}