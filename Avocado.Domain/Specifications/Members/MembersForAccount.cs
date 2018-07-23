using System.Collections.Generic;
using System.Linq;
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
        public IEnumerable<Member> Filter(IEnumerable<Member> items)
        {
            return items.Where(member => member.AccountId == _account.Id);
        }
    }
}