using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class ProjectUser
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string UserId { get; set; }
        public virtual List<Project> Projects { get; set; }
        public virtual List<ApplicationUser> Users { get; set; }
    }
}