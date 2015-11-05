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

            ViewBag.ProjectName = thisProject.ProjectName; 

         return View(viewmodel);
        }

// POST: ProjectTasks/Create
// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SelectedProjectId, ProjectTaskId,Description,IsComplete")] ProjectTask newTask)

        {
            if (ModelState.IsValid)
            { 
                db.ProjectTasks.Add(newTask);
                db.SaveChanges();
                //it's redirecting to the index after creation so this is working!!!! 11/3
                return RedirectToAction("Index");
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
                newTask.Project = viewmodel.Project;
                newTask.ProjectTaskId = viewmodel.ProjectTaskId;
                newTask.Description = viewmodel.Description;
                newTask.IsComplete = viewmodel.IsComplete; 

                db.ProjectTasks.Add(newTask);
                db.SaveChanges();
                //it's redirecting to the index after creation so this is working!!!! 11/3
                return RedirectToAction("Index");
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
