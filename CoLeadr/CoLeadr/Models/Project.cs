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

        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "End Date")]
        public string EndDate { get; set; }

        [Display(Name = "Groups")]
        public virtual ICollection<Group> ProjectGroups { get; set; }

        [Display(Name = "Project Tasks")]
        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }

        public virtual ICollection<PersonProjectRecord> PersonProjectRecords { get; set; }


        
    }
}