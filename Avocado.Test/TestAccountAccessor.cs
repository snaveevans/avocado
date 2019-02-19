using System.Threading.Tasks;
using Avocado.Domain.Entities;
using Avocado.Domain.Interfaces;

namespace Avocado.Test
{
    public class TestAccountAccessor : IAccountAccessor
    {
        private Account _account;
        public TestAccountAccessor(Account account)
        {
            _account = account;
        }

        public void SetAccount(Account account)
        {
            _account = account;
        }

        public async Task<Account> GetAccount()
        {
            return await Task.Run(() => _account);
        }
    }
}