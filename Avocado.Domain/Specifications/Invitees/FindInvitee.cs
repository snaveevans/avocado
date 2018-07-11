using System.Collections.Generic;
using System.Linq;
using Avocado.Domain.Entities;
using Avocado.Domain.Interfaces;

namespace Avocado.Domain.Specifications.Invitees
{
    public class FindInvitee : ISpecification<Invitee>
    {
        private readonly Account _account;
        private readonly Event _evnt;
        
        public FindInvitee(Account account, Event evnt)
        {
            _account = account;
            _evnt = evnt;
        }

        public IEnumerable<Invitee> Filter(IEnumerable<Invitee> items)
        {
            return items.Where(i => i.AccountId == _account.Id && i.EventId == _evnt.Id);
        }
    }
}