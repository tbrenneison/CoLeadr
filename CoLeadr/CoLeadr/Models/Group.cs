using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoLeadr.Models
{
    public class Group
    {
        public int GroupId { get; set; }

        public string Name { get; set; }

        public virtual IList<Person> Members { get; set; }
        public virtual ICollection<Project> Projects { get; set; }

    }
}