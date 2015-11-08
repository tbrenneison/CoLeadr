using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoLeadr.Models
{
    public class PersonGroupingViewModel
    {
        [Key]
        public int PersonId { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set;}

        [Display(Name = "Group Memberships")]
        public virtual IList<Group> Memberships { get; set; }

        [Display(Name = "All Available Groups")]
        public SelectList AllGroups { get; set; }
        public int SelectedGroupId { get; set; }


    }

    
}