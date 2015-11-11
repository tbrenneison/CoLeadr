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
    public class ProjectsController : Controller
    {
        private CoLeadrDBContext db = new CoLeadrDBContext();

        // GET: Projects
        public ActionResult Index()
        {
            return View(db.Projects.ToList());
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            //new viewmodel
            ProjectViewModel viewmodel = new ProjectViewModel();
            viewmodel.Name = project.Name;
            viewmodel.EndDate = project.EndDate;
            viewmodel.ProjectGroups = project.ProjectGroups;

            List<Person> projectmembers = new List<Person>();
            foreach(PersonProjectRecord record in project.PersonProjectRecords)
            {
                Person person = db.People.Find(record.PersonId);
                projectmembers.Add(person);
            }
            viewmodel.ProjectMembers = projectmembers;

            return View(viewmodel);

        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            ProjectCreateViewModel viewmodel = new ProjectCreateViewModel();
            List<Group> allgroups = new List<Group>();
            List<Person> allpeople = new List<Person>(); 

            foreach(Group g in db.Groups)
            {
                allgroups.Add(g);
            }
            foreach(Person p in db.People)
            {
                allpeople.Add(p); 
            }

            viewmodel.AllGroups = allgroups;
            viewmodel.AllPeople = allpeople;

            return View(viewmodel);
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ProjectId,Name,EndDate")] Project project)
        public ActionResult Create(ProjectCreateViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                Project project = new Project();
                //add stuff to project 
                project.EndDate = viewmodel.EndDate;
                project.Name = viewmodel.Name;
                project.ProjectId = viewmodel.ProjectId;

                //add project groups
                List<Group> projectgroups = new List<Group>(); 
                foreach(int groupId in viewmodel.SelectedGroupIds)
                {
                    Group group = db.Groups.Find(groupId);
                    projectgroups.Add(group); 
                }
                project.ProjectGroups = projectgroups;

                //create personprojectrecords for individual project memberships

                List<PersonProjectRecord> newrecords = new List<PersonProjectRecord>(); 
                foreach(Group group in projectgroups)
                {
                    foreach(Person member in group.Members)
                    {
                        PersonProjectRecord newrecord = new PersonProjectRecord();
                        newrecord.PersonId = member.PersonId;
                        newrecord.ProjectId = project.ProjectId;
                        newrecord.RemoveWithGroup = true;
                        newrecords.Add(newrecord); 
                    }
                }
                

                //save to db
                foreach(PersonProjectRecord record in newrecords)
                {
                    db.PersonProjectRecords.Add(record);
                }

                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(viewmodel);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectId,Name,EndDate")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
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
