﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoLeadr.Models
{
    public class ProjectCreateViewModel
    {
        [Key]
        public int ProjectId { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "End Date")]
        public string EndDate { get; set; }

        [Display(Name = "All Available Groups")]
        public List<Group> AllGroups { get; set; }
        [Display(Name = "All Available Members")]
        public List<Person> AllPeople { get; set; }
        public int[] SelectedGroupIds { get; set; }
        public int[] SelectedPersonIds { get; set; }




    }
}