using System;
using System.Linq;
using Avocado.Domain.Entities;
using Avocado.Domain.Enumerations;
using Avocado.Domain.Services;
using Xunit;

namespace Avocado.Test
{
    public class InviteeTest
    {
        private readonly EventService _eventService;
        private readonly TestRepo<Event> _eventRepo;
        private readonly TestRepo<Invitee> _inviteeRepo;
        
        public InviteeTest()
        {
            _eventRepo = new TestRepo<Event>();
            _inviteeRepo = new TestRepo<Invitee>();
            _eventService = new EventService(_eventRepo, _inviteeRepo);
        }

        [Fact]
        public void Constructor()
        {
            var account = new Account("tyler");
            var evnt = _eventService.Create(new Account("blah"), "Foo", "Bar");

            Assert.Throws<ArgumentNullException>(() => new Invitee(null, evnt));
            Assert.Throws<ArgumentNullException>(() => new Invitee(account, null));

            var invitee = new Invitee(account, evnt);

            Assert.Equal(account.Id, invitee.AccountId);
            Assert.Equal(evnt.Id, invitee.EventId);
            Assert.Equal(AttendanceStatuses.NotResponded.ToString(), invitee.AttendanceStatus);
        }

        [Fact]
        public void UpdateStatus()
        {
            var account = new Account("tyler");
            var evnt = _eventService.Create(account, "Foo", "Bar");

            var invitee = _inviteeRepo.List.First();
            invitee.UpdateAttendanceStatus(AttendanceStatuses.Going);

            Assert.Equal(AttendanceStatuses.Going.ToString(), invitee.AttendanceStatus);
        }
    }
}