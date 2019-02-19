using System.Threading.Tasks;
using Avocado.Domain.Entities;
using Avocado.Domain.Interfaces;
using Avocado.Domain.Specifications.Members;

namespace Avocado.Domain.Services
{
    public class AuthorizationService
    {
        private readonly IRepository<Member> _memberRepo;
        private readonly IAccountAccessor _accountAccessor;

        public AuthorizationService(IRepository<Member> memberRepo,
            IAccountAccessor accountAccessor)
        {
            _memberRepo = memberRepo;
            _accountAccessor = accountAccessor;
        }

        public async Task<bool> CanReadEvent(Event evnt)
        {
            Account account = await _accountAccessor.GetAccount();
            if (account == null || evnt == null)
            {
                return false;
            }

            Member member = await _memberRepo.Find(new FindMember(account, evnt));
            return member != null;
        }
    }
}