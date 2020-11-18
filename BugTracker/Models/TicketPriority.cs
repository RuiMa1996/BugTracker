using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class TicketPriority
    {
        public int id { get; set; }
        public Priority Name { get; set; }

    }

    public enum Priority
    {
        High,
        Normal,
        Low,
    }
}