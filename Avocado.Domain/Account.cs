using System;

namespace Avocado.Domain
{
    public class Account
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public bool IsEnabled { get; private set; }
        public string Avatar { get; private set; }

        [Obsolete("system constructor")]
        protected Account() { }

        public Account(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            Id = Guid.NewGuid();
            Name = name.Trim();
            IsEnabled = true;
            Avatar = string.Empty;
        }

        public void Disable()
        {
            IsEnabled = false;
        }

        public void Enable()
        {
            IsEnabled = true;
        }

        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            Name = name.Trim();
        }

        public void UpdateAvatar(string avatar)
        {
            if (string.IsNullOrWhiteSpace(avatar))
                throw new ArgumentNullException(nameof(avatar));

            Avatar = avatar.Trim();
        }
    }
}