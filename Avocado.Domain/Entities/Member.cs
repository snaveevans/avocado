using System;
using Avocado.Domain.Enumerations;

namespace Avocado.Domain.Entities
{
    /// <summary>
    /// Avocado event member
    /// </summary>
    public class Member
    {
        /// <summary>
        /// Account id of the member.
        /// </summary>
        public Guid AccountId { get; private set; }
        /// <summary>
        /// Event id the member belongs to.
        /// </summary>
        public Guid EventId { get; private set; }
        /// <summary>
        /// Member's attendance status.
        /// </summary>
        public AttendanceStatuses AttendanceStatus { get; private set; }
        /// <summary>
        /// Member's role
        /// </summary>
        public Roles Role { get; private set; }

        [Obsolete("system constructor")]
        protected Member() { }
        internal Member(IAccount account, Event evnt, Roles role)
        {
            if (account == null) { throw new ArgumentNullException(nameof(account)); }
            if (evnt == null) { throw new ArgumentNullException(nameof(evnt)); }

            AccountId = account.Id;
            EventId = evnt.Id;
            AttendanceStatus = AttendanceStatuses.NotResponded;
            Role = role;
        }

        internal void UpdateAttendanceStatus(AttendanceStatuses status)
        {
            AttendanceStatus = status;
        }

        internal void UpdateRole(Roles role)
        {
            Role = role;
        }
    }
}