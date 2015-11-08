using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoLeadr.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string EndDate { get; set; } //change to datetime with picker at later date
        
        //also allow for notes at a later date 
        public IList<string> Notes { get; set; }

        //figure out reminder functionality at later date as well 
        public IList<string> Reminders { get; set; }

        public virtual ICollection<Person> AssignedGroupMembers { get; set; }
        public virtual ICollection<Person> AdditionalAssignedIndividuals { get; set; }
        public virtual ICollection<Group> AssignedGroups { get; set; }
        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }

    }
}