using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoLeadr.Models
{
    public class ProjectTask
    {
        [Key]
        public int ProjectTaskId { get; set; }
        public virtual Project Project { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Complete?")]
        public bool IsComplete { get; set; }

        [Display(Name = "Task Participants")]
        public virtual ICollection<TaskParticipant> TaskParticipants { get; set; }
    }

}