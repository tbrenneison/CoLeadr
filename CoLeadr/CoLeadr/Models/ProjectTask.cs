using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoLeadr.Models
{
    public class ProjectTask
    {
        public int ProjectTaskId { get; set; }
        [Display(Name = "Project")]
        public virtual Project Project { get; set; } //one project has many tasks
        public int ProjectId { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Completed?")]
        public bool IsComplete { get; set; }
        public IList<Person> PeopleCompleting { get; set; }
        public IList<Person> PeopleCompleted { get; set; }


    }
}