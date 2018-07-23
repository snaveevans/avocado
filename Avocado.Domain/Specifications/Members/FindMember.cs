using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Member> Filter(IEnumerable<Member> items)
        {
            return items.Where(i => i.AccountId == _account.Id && i.EventId == _evnt.Id);
        }
    }
}