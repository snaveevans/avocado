using System;
using System.Linq.Expressions;
using Avocado.Domain.Entities;
using Avocado.Domain.Interfaces;
using Avocado.Infrastructure.Authentication;

namespace Avocado.Infrastructure.Specifications
{
    public class LoginsForAccount : ISpecification<Login>
    {
        private readonly Guid _accountId;
        public LoginsForAccount(Account account)
        {
            _accountId = account.Id;
        }

        public Expression<Func<Login, bool>> BuildExpression() => login => login.AccountId == _accountId;
    }
}