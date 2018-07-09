using System.Collections.Generic;
using System.Linq;
using Avocado.Domain.Entities;
using Avocado.Domain.Interfaces;
using Avocado.Infrastructure.Specifications.Invitees;

namespace Avocado.Infrastructure.Specifications.Events
{
    public class AllAuthorizedEvents : ISpecification<Event>
    {
        private readonly Account _account;
        private readonly IRepository<Invitee> _inviteeRepo;
        
        public AllAuthorizedEvents(Account account, IRepository<Invitee> inviteeRepo)
        {
            _account = account;
            _inviteeRepo = inviteeRepo;
        }
        public IEnumerable<Event> Filter(IEnumerable<Event> items)
        {
            var invitees = _inviteeRepo.Query(new InviteesForAccount(_account))
                .Select(invitee => invitee.EventId);
            return items.Where(evnt => invitees.Contains(evnt.Id));
        }
    }
}