using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoLeadr.Models
{
    public class Group
    {
        [Key]
        public int GroupId { get; set; }

        [Display(Name = "Group Name")]
        public string Name { get; set; }

        [Display(Name = "Group Members")]
        public virtual IList<Person> Members { get; set; }
        [Display(Name = "Group Projects")]
        public virtual ICollection<Project> Projects { get; set; }

    }
}