using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class TicketType
    {
        public int id { get; set; }
        public Type Name { get; set; }
        
    }

    public enum Type
    {
        CodeBug,
        DataBaseBug,
    }
}