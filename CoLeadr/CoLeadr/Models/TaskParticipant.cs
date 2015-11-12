using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoLeadr.Models
{
    public class TaskParticipant
    {
        [Key]
        public int TaskParticipantId { get; set; }
        public int ProjectTaskId { get; set; }
        public int PersonId { get; set; }
        public bool HasCompleted { get; set; }

    }
}