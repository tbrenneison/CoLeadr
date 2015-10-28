using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoLeadr.Models
{
    public class Person
    {
        public int PersonId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string PrimaryPhone { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }

       public List<Group> Memberships { get; set; }
        
    }
}