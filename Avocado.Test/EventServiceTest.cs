using System;
using Avocado.Domain.Entities;
using Avocado.Domain.Services;
using Xunit;

namespace Avocado.Test
{
    public class EventServiceTest
    {
        private readonly EventService _eventService;
        private readonly TestRepo<Event> _eventRepo;
        private readonly TestRepo<Invitee> _inviteeRepo;
        
        public EventServiceTest()
        {
            _eventRepo = new TestRepo<Event>();
            _inviteeRepo = new TestRepo<Invitee>();
            _eventService = new EventService(_eventRepo, _inviteeRepo);
        }

        [Fact]
        public void Create()
        {
            var account = new Account("tyler");
            var evnt = _eventService.Create(account, "Foo", "Bar");

            Assert.Throws<ArgumentNullException>(() => _eventService.Create(null, "Foo", "Bar"));
            Assert.Throws<ArgumentNullException>(() => _eventService.Create(null, "", "Bar"));
            Assert.Throws<ArgumentNullException>(() => _eventService.Create(account, "Foo", ""));

            Assert.NotEqual(Guid.Empty, evnt.Id);
            Assert.Equal("Foo", evnt.Title);
            Assert.Equal("Bar", evnt.Description);
            Assert.Single(_eventRepo.List);
            Assert.Single(_inviteeRepo.List);
        }
    }
}
