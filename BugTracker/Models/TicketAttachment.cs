using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class TicketAttachment
    {
        public int id { get; set; }
        public int TicletId { get; set; }
        public string FilePath { get; set; }
        public string Description { get; set; }
        public bool Created { get; set; }
        public string UserId { get; set; }
        public string FileUrl { get; set; }
    }
}