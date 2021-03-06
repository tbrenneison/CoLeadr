﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CoLeadr.Models
{
    public class CoLeadrDBContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public CoLeadrDBContext() : base("name=CoLeadrDBContext")
        { }

            protected override void OnModelCreating(DbModelBuilder modelBuilder)
             {
                //ihavenoideawhatimdoing.jpg
                modelBuilder.Entity<Project>()
                .HasMany(t => t.ProjectTasks)
                .WithRequired(p => p.Project)
                .Map(k => k.MapKey("ProjectId"))
                .WillCascadeOnDelete(false);
              }
        

        public System.Data.Entity.DbSet<CoLeadr.Models.Person> People { get; set; }

        public System.Data.Entity.DbSet<CoLeadr.Models.Group> Groups { get; set; }

        public System.Data.Entity.DbSet<CoLeadr.Models.PersonGroupingViewModel> PersonGroupingViewModels { get; set; }

        public System.Data.Entity.DbSet<CoLeadr.Models.Project> Projects { get; set; }

        public System.Data.Entity.DbSet<CoLeadr.Models.PersonProjectRecord> PersonProjectRecords { get; set; }

        public System.Data.Entity.DbSet<CoLeadr.Models.ProjectTask> ProjectTasks { get; set; }

        public System.Data.Entity.DbSet<CoLeadr.Models.ProjectTaskViewModel> ProjectTaskViewModels { get; set; }
    }
}
