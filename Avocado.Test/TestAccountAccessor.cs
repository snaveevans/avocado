using Avocado.Domain.Entities;
using Avocado.Domain.Interfaces;

namespace Avocado.Test
{
    public class TestAccountAccessor : IAccountAccessor
    {
        public IAccount Account => _account;
        private IAccount _account;
        public TestAccountAccessor(IAccount account)
        {
            _account = account;
        }

        public void SetAccount(IAccount account)
        {
            _account = account;
        }
    }
}