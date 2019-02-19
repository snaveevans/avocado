using System;
using Avocado.Domain.Entities;
using Avocado.Domain.Interfaces;
using Avocado.Domain.Services;
using Avocado.Infrastructure.Authentication;
using Xunit;

namespace Avocado.Test
{
    public class EventServiceTest
    {
        private readonly EventService _eventService;
        private readonly TestRepo<Event> _eventRepo;
        private readonly TestRepo<Member> _memberRepo;
        private readonly TestAccountAccessor _accountAccessor;

        public EventServiceTest()
        {
            _eventRepo = new TestRepo<Event>();
            _memberRepo = new TestRepo<Member>();
            _accountAccessor = new TestAccountAccessor(null);
            _eventService = new EventService(_eventRepo, _memberRepo, null, _accountAccessor);
        }

        [Fact]
        public void Create()
        {
            Event evnt = null, failEvent = null;
            Assert.False(_eventService.TryCreate("Foo", "Bar", out failEvent));

            var account = new Account("tyler", "foobar");
            _accountAccessor.SetAccount(account);
            Assert.Throws<ArgumentNullException>(() => _eventService.TryCreate("", "Bar", out failEvent));
            Assert.Throws<ArgumentNullException>(() => _eventService.TryCreate("Foo", "", out failEvent));

            Assert.True(_eventService.TryCreate("Foo", "Bar", out evnt));
            Assert.NotEqual(Guid.Empty, evnt.Id);
            Assert.Equal("Foo", evnt.Title);
            Assert.Equal("Bar", evnt.Description);
            Assert.Single(_eventRepo.List);
            Assert.Single(_memberRepo.List);
        }
    }
}
