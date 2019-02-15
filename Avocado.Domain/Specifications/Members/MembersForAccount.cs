using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Avocado.Domain.Entities;
using Avocado.Domain.Interfaces;

namespace Avocado.Domain.Specifications.Members
{
    public class MembersForAccount : ISpecification<Member>
    {
        private readonly Account _account;

        public MembersForAccount(Account account)
        {
            _account = account;
        }

        public Expression<Func<Member, bool>> BuildExpression() => member => member.AccountId == _account.Id;
    }
}
