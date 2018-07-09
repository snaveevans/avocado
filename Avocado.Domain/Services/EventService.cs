using Avocado.Domain.Entities;
using Avocado.Domain.Interfaces;

namespace Avocado.Domain.Services
{
    public class EventService
    {
        private readonly IRepository<Event> _eventRepo;
        private readonly IRepository<Invitee> _inviteeRepo;

        public EventService(IRepository<Event> eventRepo,
            IRepository<Invitee> inviteeRepo)
        {
            _eventRepo = eventRepo;
            _inviteeRepo = inviteeRepo;
        }

        public Event Create(Account account, string title, string description)
        {
            var evnt = new Event(title, description);
            var invitee = new Invitee(account, evnt);

            _eventRepo.Add(evnt);
            _inviteeRepo.Add(invitee);

            return evnt;
        }
    }
}