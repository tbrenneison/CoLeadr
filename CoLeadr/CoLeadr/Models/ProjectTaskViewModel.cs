using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoLeadr.Models
{
    public class ProjectTaskViewModel
    {
        [Key]
        public int ProjectTaskId { get; set; }
        public Project Project { get; set; } //one project has many tasks
        public string Description { get; set; }
        public bool IsComplete { get; set; }

        public IList<Person> PeopleCompleting { get; set; }
        public IList<Person> PeopleCompleted { get; set; }


    }
}