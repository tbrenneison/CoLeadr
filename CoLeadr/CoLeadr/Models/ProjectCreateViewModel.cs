using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System.Web;

namespace CoLeadr.Models
{
    public class ProjectCreateViewModel
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string EndDate { get; set; } //change to datetime with picker at later date


        public virtual ICollection<Person> AssignedIndividuals { get; set; }
        public virtual ICollection<Group> AssignedGroups { get; set; }

        public List<Person> allpeople { get; set; }
        public List<Group> allgroups { get; set; }

        public int[] SelectPeopleIds { get; set; }
        public int[] SelectGroupIds { get; set; }


    }
}