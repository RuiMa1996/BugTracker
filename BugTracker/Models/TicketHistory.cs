using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class TicketHistory
    {
        public int id { get; set; }
        public string Property { get; set; }
        public int OldValue { get; set; }
        public int NewValue { get; set; }
        public bool Changed { get; set; }

        [Foreignkey("Ticket")]
        public int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }

        [Foreignkey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}