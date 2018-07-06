using System;
using Avocado.Domain;
using Xunit;

namespace Avocado.Test
{
    public class AccountTest
    {
        [Fact]
        public void Constructor()
        {
            var account = new Account("tyler evans");

            Assert.Throws<ArgumentNullException>(() => new Account(""));

            Assert.NotEqual(Guid.Empty, account.Id);
            Assert.Equal("tyler evans", account.Name);
            Assert.True(account.IsEnabled);
            Assert.Equal(string.Empty, account.Picture);
        }

        [Fact]
        public void Disable()
        {
            var account = new Account("tyler evans");
            
            account.Disable();

            Assert.False(account.IsEnabled);
        }

        [Fact]
        public void Enable()
        {
            var account = new Account("tyler evans");
            
            account.Disable();
            account.Enable();

            Assert.True(account.IsEnabled);
        }

        [Fact]
        public void UpdateAvatar()
        {
            var account = new Account("tyler evans");
            
            account.UpdatePicture("foobar");

            Assert.Equal("foobar", account.Picture);
        }

        [Fact]
        public void UpdateName()
        {
            var account = new Account("tyler evans");
            
            account.UpdateName("blah");

            Assert.Equal("blah", account.Name);
        }
    }
}