using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Avocado.Domain.Entities;
using Avocado.Domain.Interfaces;

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
