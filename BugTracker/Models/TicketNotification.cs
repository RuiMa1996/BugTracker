using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class TicketNotification
    {
        public int id { get; set; }
        public int TicketId { get; set; }
        public string UserId { get; set; }
    }
}