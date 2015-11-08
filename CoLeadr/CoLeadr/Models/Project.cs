using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoLeadr.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }
        [Display(Name = "End Date")]
        public string EndDate { get; set; } //change to datetime with picker at later date
        
        //also allow for notes at a later date 
        public IList<string> Notes { get; set; }

        //figure out reminder functionality at later date as well 
        public IList<string> Reminders { get; set; }

        [Display(Name = "Assigned Members From Groups")]
        public virtual ICollection<Person> AssignedGroupMembers { get; set; }
        [Display(Name = "Assigned Individual Members")]
        public virtual ICollection<Person> AdditionalAssignedIndividuals { get; set; }
        [Display(Name = "Assigned Groups")]
        public virtual ICollection<Group> AssignedGroups { get; set; }
        [Display(Name = "Project Tasks")]
        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }

    }
}