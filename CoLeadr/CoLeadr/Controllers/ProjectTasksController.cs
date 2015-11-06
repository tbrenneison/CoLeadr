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
        public ActionResult Index(int? projectId)
        {
            Project thisProject = db.Projects.Find(projectId);
            List<ProjectTask> theseTasks = thisProject.ProjectTasks.ToList(); 
            return View(theseTasks);
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

            Project thisProject = projectTask.Project;
            ViewBag.ProjectName = thisProject.ProjectName;

            return View(projectTask);
        }

        // GET: ProjectTasks/Create NEW PROJECT TASK 
        public ActionResult Create()
        { 

            return View();
        }

         // GET: ProjectTasks/Create FROM PROJECT CREATION 
         public ActionResult CreateFromProject(int? projectId)
        {
            Project thisProject = db.Projects.Find(projectId);

            ProjectTaskViewModel viewmodel = new ProjectTaskViewModel();

            viewmodel.Project = thisProject;
            viewmodel.ProjectId = thisProject.ProjectId; 
            ViewBag.ProjectName = thisProject.ProjectName; 

         return View(viewmodel);
        }

// POST: ProjectTasks/Create
// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
[HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "SelectedProjectId, ProjectTaskId,Description,IsComplete")] ProjectTask newTask)
        public ActionResult Create(ProjectTaskViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                ProjectTask newTask = new ProjectTask();
                newTask.Description = viewmodel.Description;
                newTask.IsComplete = viewmodel.IsComplete;
                newTask.ProjectTaskId = viewmodel.ProjectTaskId;

                newTask.Project = db.Projects.Find(viewmodel.ProjectId);
                newTask.ProjectId = viewmodel.ProjectId; 

                db.ProjectTasks.Add(newTask);
                db.SaveChanges();
                return RedirectToAction("Index", new { projectId = newTask.ProjectId });
            }

            return View();
        }

        //POST: New Project Task FROM PROJECT CREATION 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFromProject(ProjectTaskViewModel viewmodel)

        {
            if (ModelState.IsValid)
            {
                ProjectTask newTask = new ProjectTask();
                newTask.Project = db.Projects.Find(viewmodel.ProjectId);
                newTask.ProjectId = viewmodel.ProjectId; 
                newTask.ProjectTaskId = viewmodel.ProjectTaskId;
                newTask.Description = viewmodel.Description;
                newTask.IsComplete = viewmodel.IsComplete;

                //the project is null.  WHY IS THE PROJECT NULL
                //do i have to pass in a projectid instead of the project object, probably

                db.ProjectTasks.Add(newTask);
                db.SaveChanges();

                return RedirectToAction("Index", new { projectId = newTask.ProjectId});
            }

            return View();
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
            return View(projectTask);
        }

        // POST: ProjectTasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectTaskId,Description,IsComplete")] ProjectTask projectTask)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projectTask).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(projectTask);
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
