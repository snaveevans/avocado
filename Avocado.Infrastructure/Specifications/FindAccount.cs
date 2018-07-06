using System;
using System.Collections.Generic;
using System.Linq;
using Avocado.Domain;

namespace Avocado.Infrastructure.Specifications
{
    public class FindAccount : ISpecification<Account>
    {
        private readonly Guid _accountId;
        public FindAccount(Guid accountId)
        {
            _accountId = accountId;
        }
        public IEnumerable<Account> Filter(IEnumerable<Account> items)
        {
            return items.Where(a => a.Id == _accountId);
        }
    }
}