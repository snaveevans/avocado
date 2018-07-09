using System;
using Avocado.Domain.Enumerations;

namespace Avocado.Domain.Entities
{
    public class Invitee
    {
        public Guid AccountId { get; private set; }
        public Guid EventId { get; private set; }
        public string AttendanceStatus { get; private set; }
        
        [Obsolete("system constructor")]
        protected Invitee() { }
        public Invitee(Account account, Event evnt)
        {
            if (account == null) { throw new ArgumentNullException(nameof(account));}
            if (evnt == null) { throw new ArgumentNullException(nameof(evnt));}
            
            AccountId = account.Id;
            EventId = evnt.Id;
            AttendanceStatus = AttendanceStatuses.NotResponded.ToString();
        }

        public void UpdateAttendanceStatus(AttendanceStatuses status)
        {
            AttendanceStatus = status.ToString();
        }
    }
}