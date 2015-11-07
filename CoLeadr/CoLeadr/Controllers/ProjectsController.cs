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
            return View(project);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            List<Person> allindividuals = new List<Person>(); 
            foreach(Person p in db.People)
            {
                allindividuals.Add(p); 
            }

            List<Group> allthegroups = new List<Group>(); 
            foreach(Group g in db.Groups)
            {
                allthegroups.Add(g); 
            }

            ProjectCreateViewModel viewmodel = new ProjectCreateViewModel();
            viewmodel.allgroups = allthegroups;
            viewmodel.allpeople = allindividuals;

            return View(viewmodel);
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjectCreateViewModel viewmodel)
        //public ActionResult Create([Bind(Include = "ProjectId,ProjectName,EndDate,SelectGroupIds")] Project project)
        {
            if (ModelState.IsValid)
            {
                Project project = new Project();
                project.ProjectId = viewmodel.ProjectId;
                project.ProjectName = viewmodel.ProjectName;
                project.EndDate = viewmodel.EndDate;

                List<Group> assignedGroups = new List<Group>();
                List<Person> assignedIndividuals = new List<Person>();

                if (viewmodel.SelectGroupIds != null)
                {
                    //add assigned groups to project 
                    foreach (int selectedGroupId in viewmodel.SelectGroupIds)
                    {
                        Group thisGroup = db.Groups.Find(selectedGroupId);
                        assignedGroups.Add(thisGroup);
                    }

                    //add individuals from groups to project 
                    List<Person> groupMembersToAdd = new List<Person>();
                    foreach (Group group in assignedGroups)
                    {
                        foreach (Person member in group.Members)
                        {
                            groupMembersToAdd.Add(member);
                        }
                    }

                    foreach (Person member in groupMembersToAdd)
                    {
                        assignedIndividuals.Add(member);
                    }
                }

                if (viewmodel.SelectPeopleIds != null)
                {
                    //add separately assigned individuals to project 
                    foreach (int selectedPersonId in viewmodel.SelectPeopleIds)
                    {
                        Person thisPerson = db.People.Find(selectedPersonId);
                        assignedIndividuals.Add(thisPerson);
                    }
                }

                //assign all the groups and individuals and add changes
                project.AssignedGroups = assignedGroups;
                project.AssignedIndividuals = assignedIndividuals;

                db.Projects.Add(project);
                db.SaveChanges();

                //redirect to "add project task" 
                return RedirectToAction("CreateFromProject","ProjectTasks", new { projectId = project.ProjectId });
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
        public ActionResult Edit([Bind(Include = "ProjectId,ProjectName,EndDate")] Project project)
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
