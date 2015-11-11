using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoLeadr.Models
{
    public class PersonProjectRecord
    {
        public int PersonProjectRecordId { get; set; }
        public int PersonId { get; set; }
        public int ProjectId { get; set; }
        public bool RemoveWithGroup { get; set; }
    }
}