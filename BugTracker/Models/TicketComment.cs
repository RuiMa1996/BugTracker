using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class TicketComment
    {
        public int id { get; set; }
        public string Comment { get; set; }
        public bool Created { get; set; }
        public int TicketId { get; set; }
        public string UserId { get; set; }
    }
}