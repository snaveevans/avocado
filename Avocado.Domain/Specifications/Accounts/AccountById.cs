using System;
using System.Collections.Generic;
using System.Linq;
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
        public IEnumerable<Account> Filter(IEnumerable<Account> items)
        {
            return items.Where(a => a.Id == _accountId);
        }
    }
}