using System;
using System.Linq;
using Avocado.Domain.Entities;
using Avocado.Domain.Enumerations;
using Avocado.Domain.Interfaces;
using Avocado.Domain.Services;
using Xunit;

namespace Avocado.Test
{
    public class MemberServiceTest
    {
        private readonly EventService _eventService;
        private readonly MemberService _memberService;
        private readonly TestRepo<Event> _eventRepo;
        private readonly TestRepo<Member> _memberRepo;
        private readonly IAccountAccessor _accountAccessor;

        public MemberServiceTest()
        {
            _eventRepo = new TestRepo<Event>();
            _memberRepo = new TestRepo<Member>();
            var account = new Account("tyler");
            _accountAccessor = new TestAccountAccessor(account);
            var authService = new AuthorizationService(_memberRepo, _accountAccessor);
            _eventService = new EventService(_eventRepo, _memberRepo, authService, _accountAccessor);
            _memberService = new MemberService(_eventService, _memberRepo, authService, _accountAccessor);
        }

        [Fact]
        public void AddMember()
        {
            var account = new Account("blah");
            _eventService.TryCreate("Foo", "Bar", out Event evnt);
            Member failMember = null, member = null;

            Assert.Throws<ArgumentNullException>(() => _memberService.TryAddMember(evnt.Id, null, out failMember));
            Assert.Null(failMember);
            Assert.False(_memberService.TryAddMember(Guid.NewGuid(), account, out member));

            Assert.True(_memberService.TryAddMember(evnt.Id, account, out member));
            Assert.NotNull(member);

            Assert.Equal(account.Id, member.AccountId);
            Assert.Equal(evnt.Id, member.EventId);
            Assert.Equal(AttendanceStatuses.NotResponded, member.AttendanceStatus);
            Assert.Equal(Roles.Guest, member.Role);
        }

        [Fact]
        public void UpdateStatus()
        {
            _eventService.TryCreate("Foo", "Bar", out Event evnt);
            Member member = null;

            Assert.False(_memberService.TryUpdateStatus(Guid.NewGuid(), AttendanceStatuses.Going, out member));
            Assert.True(_memberService.TryUpdateStatus(evnt.Id, AttendanceStatuses.Going, out member));
            Assert.Equal(AttendanceStatuses.Going, member.AttendanceStatus);
        }
    }
}