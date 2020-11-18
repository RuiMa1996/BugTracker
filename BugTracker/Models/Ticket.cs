using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class Ticket
    {
        public int id { get; set; }
        public string Titile { get; set; }
        public string Description { get; set; }
        public bool Created { get; set; }
        public bool Updated { get; set; }
        public int TicketTypeId { get; set; }
        public int TicketStatusId { get; set; }
        public string OwnerUserId { get; set; }
        public string AssignedToUserId { get; set; }
    }
}