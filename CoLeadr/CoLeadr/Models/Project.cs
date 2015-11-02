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
        public IList<string> Requirements { get; set; }
        public IList<string> Notes { get; set; }
        //figure out reminder functionality at later date as well 
        public virtual ICollection<Person> AssignedIndividuals { get; set; }
        public virtual ICollection<Group> AssignedGroups { get; set; }
        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }

    }
}