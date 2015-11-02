using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoLeadr.Models
{
    public class ProjectTaskViewModel
    {
        public int ProjectTaskId { get; set; }
        public Project Project { get; set; } //one project has many tasks
        public string Description { get; set; }
        public bool IsComplete { get; set; }
        public IList<Person> PeopleCompleting { get; set; }
        public IList<Person> PeopleCompleted { get; set; }

        //for project selection dropdown menu
        public SelectList allprojects { get; set; }
        public int SelectedProjectId { get; set; }

    }
}