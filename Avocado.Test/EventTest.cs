using System;
using Avocado.Domain;
using Xunit;

namespace Avocado.Test
{
    public class EventTest
    {
        [Fact]
        public void Constructor()
        {
            var evnt = new Event("Foo", "Bar");

            Assert.Throws<ArgumentNullException>(() => new Event("", "Bar"));
            Assert.Throws<ArgumentNullException>(() => new Event("Foo", ""));

            Assert.NotEqual(Guid.Empty, evnt.Id);
            Assert.Equal("Foo", evnt.Title);
            Assert.Equal("Bar", evnt.Description);
        }
    }
}
