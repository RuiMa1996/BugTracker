using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class TicketHistory
    {
        public int id { get; set; }
        public int TicketId { get; set; }
        public string Property { get; set; }
        public int OldValue { get; set; }
        public int NewValue { get; set; }
        public bool Changed { get; set; }
        public string UserId { get; set; }
    }
}