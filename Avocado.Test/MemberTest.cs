using System;
using System.Linq;
using Avocado.Domain.Entities;
using Avocado.Domain.Enumerations;
using Avocado.Domain.Interfaces;
using Avocado.Domain.Services;
using Xunit;

namespace Avocado.Test
{
    public class MemberTest
    {
        private readonly EventService _eventService;
        private readonly TestRepo<Event> _eventRepo;
        private readonly TestRepo<Member> _memberRepo;
        private readonly IAccountAccessor _accountAccessor;
                
        public MemberTest()
        {
            _eventRepo = new TestRepo<Event>();
            _memberRepo = new TestRepo<Member>();
            var account = new Account("tyler");
            _accountAccessor = new TestAccountAccessor(account);
            _eventService = new EventService(_eventRepo, _memberRepo, null, _accountAccessor);
        }

        [Fact]
        public void Constructor()
        {
            var account = new Account("blah");
            var evnt = _eventService.Create("Foo", "Bar");

            Assert.Throws<ArgumentNullException>(() => new Member(null, evnt, Roles.Guest));
            Assert.Throws<ArgumentNullException>(() => new Member(account, null, Roles.Guest));

            var member = new Member(account, evnt, Roles.Guest);

            Assert.Equal(account.Id, member.AccountId);
            Assert.Equal(evnt.Id, member.EventId);
            Assert.Equal(AttendanceStatuses.NotResponded.ToString(), member.AttendanceStatus);
        }

        [Fact]
        public void UpdateStatus()
        {
            var account = new Account("blah");
            var evnt = _eventService.Create("Foo", "Bar");

            var member = _memberRepo.List.First();
            member.UpdateAttendanceStatus(AttendanceStatuses.Going);

            Assert.Equal(AttendanceStatuses.Going.ToString(), member.AttendanceStatus);
        }
    }
}