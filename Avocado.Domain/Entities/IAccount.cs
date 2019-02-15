using System;

namespace Avocado.Domain.Entities
{
    /// <summary>
    /// Avocado User Account.
    /// </summary>
    public interface IAccount
    {
        /// <summary>
        /// Id of the account.
        /// </summary>
        Guid Id { get; }
        /// <summary>
        /// User's given name.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// True if the account is enabled, false if disabled.
        /// </summary>
        bool IsEnabled { get; }
        /// <summary>
        /// Url to user's picture.
        /// </summary>
        string Picture { get; }
    }
}
