using System;
using Avocado.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Avocado.Infrastructure.Authentication
{
    public class Account : IdentityUser<Guid>, IAccount
    {
        /// <inheritdoc />
        public string Name { get; private set; }
        /// <inheritdoc />
        public bool IsEnabled { get; private set; }
        /// <inheritdoc />
        public string Picture { get; private set; }

        /// <summary>
        /// Create new account.
        /// </summary>
        /// <param name="name">User's given name.</param>
        public Account(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            Id = Guid.NewGuid();
            Name = name.Trim();
            IsEnabled = true;
            Picture = string.Empty;
        }

        /// <summary>
        /// Disable the account.
        /// </summary>
        public void Disable()
        {
            IsEnabled = false;
        }

        /// <summary>
        /// Enable the account.
        /// </summary>
        public void Enable()
        {
            IsEnabled = true;
        }

        /// <summary>
        /// Update the user's given name.
        /// </summary>
        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            Name = name.Trim();
        }

        /// <summary>
        /// Update the user's picture.
        /// </summary>
        public void UpdatePicture(string picture)
        {
            if (string.IsNullOrWhiteSpace(picture))
                throw new ArgumentNullException(nameof(picture));

            Picture = picture.Trim();
        }
    }
}