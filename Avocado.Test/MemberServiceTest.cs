using System;
using System.Linq;
using Avocado.Domain.Entities;
using Avocado.Domain.Enumerations;
using Avocado.Domain.Interfaces;
using Avocado.Domain.Services;
using Avocado.Infrastructure.Authentication;
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
            var account = new Account("tyler", "foobar2");
            _accountAccessor = new TestAccountAccessor(account);
            var authService = new AuthorizationService(_memberRepo, _accountAccessor);
            _eventService = new EventService(_eventRepo, _memberRepo, authService, _accountAccessor);
            _memberService = new MemberService(_eventService, _memberRepo, authService, _accountAccessor);
        }

        [Fact]
        public async void AddMember()
        {
            var account = new Account("blah", "foobar");
            Event evnt = await _eventService.Create("Foo", "Bar");
            Member failMember = null, member = null;

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _memberService.AddMember(evnt.Id, null));
            Assert.Null(failMember);
            failMember = await _memberService.AddMember(Guid.NewGuid(), account);
            Assert.Null(failMember);

            member = await _memberService.AddMember(evnt.Id, account);
            Assert.NotNull(member);

            Assert.Equal(account.Id, member.AccountId);
            Assert.Equal(evnt.Id, member.EventId);
            Assert.Equal(AttendanceStatuses.NotResponded, member.AttendanceStatus);
            Assert.Equal(Roles.Guest, member.Role);
        }

        [Fact]
        public async void UpdateStatus()
        {
            Event evnt = await _eventService.Create("Foo", "Bar");
            Member member = null;

            member = await _memberService.UpdateStatus(Guid.NewGuid(), AttendanceStatuses.Going);
            Assert.Null(member);
            member = await _memberService.UpdateStatus(evnt.Id, AttendanceStatuses.Going);
            Assert.NotNull(member);
            Assert.Equal(AttendanceStatuses.Going, member.AttendanceStatus);
        }
    }
}