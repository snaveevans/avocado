using System;
using Avocado.Domain.Enumerations;

namespace Avocado.Domain.Entities
{
    public class Member
    {
        public Guid AccountId { get; private set; }
        public Guid EventId { get; private set; }
        public string AttendanceStatus { get; private set; }
        public string Role { get; private set; }
        
        [Obsolete("system constructor")]
        protected Member() { }
        internal Member(Account account, Event evnt, Roles role)
        {
            if (account == null) { throw new ArgumentNullException(nameof(account));}
            if (evnt == null) { throw new ArgumentNullException(nameof(evnt));}
            
            AccountId = account.Id;
            EventId = evnt.Id;
            AttendanceStatus = AttendanceStatuses.NotResponded.ToString();
            Role = role.ToString();
        }

        internal void UpdateAttendanceStatus(AttendanceStatuses status)
        {
            AttendanceStatus = status.ToString();
        }

        internal void UpdateRole(Roles role)
        {
            Role = role.ToString();
        }
    }
}