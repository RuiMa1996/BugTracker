using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTracker.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Titile { get; set; }
        public string Description { get; set; }
        public bool Created { get; set; }
        public bool Updated { get; set; }

        [Foreignkey("Project")] 
        public string ProjectId { get; set; }
        public virtual Project Project { get; set; }

        [Foreignkey("TicketType")]
        public int TicketTypeId { get; set; }
        public virtual TicketType TicketType { get; set; }

        [Foreignkey("TicketStatus")]
        public int TicketStatusId { get; set; }
        public virtual TicketStatus TicketStatus { get; set; }

        [Foreignkey("OwnerUser")]
        public string OwnerUserId { get; set; }
        public virtual ApplicationUser OwnerUser { get; set; }

        [Foreignkey("AssignedToUserId")]
        public string AssignedToUserId { get; set; }
        public virtual ApplicationUser AssignedToUser{ get; set; }
    }
}