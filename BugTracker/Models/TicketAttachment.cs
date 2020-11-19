using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class TicketAttachment
    {
        public int id { get; set; }

        
        public string FilePath { get; set; }
        public string Description { get; set; }
        public bool Created { get; set; }

        [Foreignkey("Ticket")]
        public int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }

        [Foreignkey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get;set; }

        public string FileUrl { get; set; }
    }
}