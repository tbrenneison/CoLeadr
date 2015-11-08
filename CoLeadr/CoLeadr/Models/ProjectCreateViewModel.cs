using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CoLeadr.Models
{
    public class ProjectCreateViewModel
    {
        public int ProjectId { get; set; }

        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }
        [Display(Name = "End Date")]
        public string EndDate { get; set; } //change to datetime with picker at later date

        [Display(Name = "Assigned Members From Groups")]
        public virtual ICollection<Person> AssignedGroupMembers { get; set; }
        [Display(Name = "Assigned Individual Members")]
        public virtual ICollection<Person> AdditionalAssignedIndividuals { get; set; }
        [Display(Name = "Assigned Groups")]
        public virtual ICollection<Group> AssignedGroups { get; set; }

        [Display(Name = "All Available People")]
        public List<Person> allpeople { get; set; }
        [Display(Name = "All Available Groups")]
        public List<Group> allgroups { get; set; }

        public int[] SelectPeopleIds { get; set; }
        public int[] SelectGroupIds { get; set; }


    }
}