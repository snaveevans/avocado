using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Avocado.Domain.Entities;
using Avocado.Domain.Interfaces;

namespace Avocado.Domain.Specifications.Members
{
    public class FindMember : ISpecification<Member>
    {
        private readonly Account _account;
        private readonly Event _evnt;

        public FindMember(Account account, Event evnt)
        {
            _account = account;
            _evnt = evnt;
        }

        public Expression<Func<Member, bool>> BuildExpression() => member => member.AccountId == _account.Id && member.EventId == _evnt.Id;
    }
}
