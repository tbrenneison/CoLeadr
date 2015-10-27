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
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set;}

        public List<Group> Memberships { get; set; }

        public SelectList AllGroups { get; set; }
        public int SelectedGroupId { get; set; }

    }

    
}