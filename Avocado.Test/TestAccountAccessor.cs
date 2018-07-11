using Avocado.Domain.Entities;
using Avocado.Domain.Interfaces;

namespace Avocado.Test
{
    public class TestAccountAccessor : IAccountAccessor
    {
        public Account Account => _account;
        private Account _account;
        public TestAccountAccessor(Account account)
        {
            _account = account;
        }

        public void SetAccount(Account account)
        {
            _account = account;
        }
    }
}