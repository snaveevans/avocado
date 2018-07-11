using Avocado.Domain.Entities;
using Avocado.Domain.Interfaces;
using Avocado.Domain.Specifications.Invitees;

namespace Avocado.Domain.Services
{
    public class AuthorizationService
    {
        private readonly IRepository<Invitee> _inviteeRepo;
        private readonly IAccountAccessor _accountAccessor;

        public AuthorizationService(IRepository<Invitee> inviteeRepo,
            IAccountAccessor accountAccessor)
        {
            _inviteeRepo = inviteeRepo;
            _accountAccessor = accountAccessor;
        }

        public bool CanReadEvent(Event evnt)
        {
            var account = _accountAccessor.Account; ;
            if (account == null || evnt == null)
                return false;

            var invitee = _inviteeRepo.Find(new FindInvitee(account, evnt));

            return invitee != null;
        }
    }
}