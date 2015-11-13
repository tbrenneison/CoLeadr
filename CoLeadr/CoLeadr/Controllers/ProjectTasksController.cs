using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CoLeadr.Models;

namespace CoLeadr.Controllers
{
    public class ProjectTasksController : Controller
    {
        private CoLeadrDBContext db = new CoLeadrDBContext();

        // GET: ProjectTasks
        public ActionResult Index()
        {
            return View(db.ProjectTasks.ToList());
        }

        // GET: TaskIndexForProject
        public ActionResult TaskIndexForProject(int? projectId)
        {
            Project thisproject = db.Projects.Find(projectId);
            ViewBag.ProjectName = thisproject.Name;
            List<ProjectTask> alltasksforproject = new List<ProjectTask>();
            if (thisproject.ProjectTasks.Count != 0)
            {
                foreach (ProjectTask ptask in thisproject.ProjectTasks)
                {
                    alltasksforproject.Add(ptask);
                }
                return View(alltasksforproject);
            }
            else
            {
                return RedirectToAction("Index", "Projects");
            }
        }


        // GET: ProjectTasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectTask projectTask = db.ProjectTasks.Find(id);
            if (projectTask == null)
            {
                return HttpNotFound();
            }
            ProjectTaskViewModel viewmodel = new ProjectTaskViewModel();
            Project thisproject = projectTask.Project;
            viewmodel.ProjectName = thisproject.Name;
            viewmodel.ProjectId = thisproject.ProjectId;
            viewmodel.Description = projectTask.Description;
            viewmodel.IsComplete = projectTask.IsComplete;
            viewmodel.ProjectTaskId = projectTask.ProjectTaskId;
            return View(viewmodel);
        }

        // GET: ProjectTasks/Create
        public ActionResult Create(int? projectId)
        {
            ProjectTaskViewModel viewmodel = new ProjectTaskViewModel();
            Project thisproject = db.Projects.Find(projectId);
            viewmodel.ProjectId = thisproject.ProjectId; 
            viewmodel.ProjectName = thisproject.Name;

            return View(viewmodel);
        }

        // POST: ProjectTasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ProjectTaskId,Description,IsComplete")] ProjectTask projectTask)
        public ActionResult Create(ProjectTaskViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                ProjectTask projectTask = new ProjectTask();
                Project thisproject = db.Projects.Find(viewmodel.ProjectId);
                projectTask.Project = thisproject;
                projectTask.Description = viewmodel.Description;
                projectTask.IsComplete = viewmodel.IsComplete; 

                db.ProjectTasks.Add(projectTask);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(viewmodel);
        }

        // GET: ProjectTasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectTask projectTask = db.ProjectTasks.Find(id);
            if (projectTask == null)
            {
                return HttpNotFound();
            }
            ProjectTaskViewModel viewmodel = new ProjectTaskViewModel();
            viewmodel.Description = projectTask.Description;
            viewmodel.IsComplete = projectTask.IsComplete;
            viewmodel.ProjectTaskId = projectTask.ProjectTaskId;
            Project thisproject = projectTask.Project;
            viewmodel.ProjectId = thisproject.ProjectId; 
            return View(viewmodel);
        }

        // POST: ProjectTasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ProjectTaskId,Description,IsComplete")] ProjectTask projectTask)
        public ActionResult Edit(ProjectTaskViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                ProjectTask thisprojecttask = db.ProjectTasks.Find(viewmodel.ProjectTaskId);
                thisprojecttask.Description = viewmodel.Description;
                thisprojecttask.IsComplete = viewmodel.IsComplete;

                db.Entry(thisprojecttask).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("TaskIndexForProject", new { projectId = viewmodel.ProjectId});
            }
            return View(viewmodel);
        }

        // GET: ProjectTasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectTask projectTask = db.ProjectTasks.Find(id);
            if (projectTask == null)
            {
                return HttpNotFound();
            }
            return View(projectTask);
        }

        // POST: ProjectTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjectTask projectTask = db.ProjectTasks.Find(id);
            db.ProjectTasks.Remove(projectTask);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
