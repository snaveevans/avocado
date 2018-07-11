using System.Collections.Generic;
using System.Linq;
using Avocado.Domain.Entities;
using Avocado.Domain.Interfaces;

namespace Avocado.Domain.Specifications.Invitees
{
    public class InviteesForAccount : ISpecification<Invitee>
    {
        private readonly Account _account;

        public InviteesForAccount(Account account)
        {
            _account = account;
        }
        public IEnumerable<Invitee> Filter(IEnumerable<Invitee> items)
        {
            return items.Where(invitee => invitee.AccountId == _account.Id);
        }
    }
}