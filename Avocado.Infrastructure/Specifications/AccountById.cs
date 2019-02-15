
using System;
using System.Linq.Expressions;
using Avocado.Domain.Interfaces;
using Avocado.Infrastructure.Authentication;

namespace Avocado.Infrastructure.Specifications
{
    public class AccountById : ISpecification<Account>
    {
        private readonly Guid _accountId;
        public AccountById(Guid accountId)
        {
            _accountId = accountId;
        }

        public Expression<Func<Account, bool>> BuildExpression() => account => account.Id == _accountId;
    }
}
