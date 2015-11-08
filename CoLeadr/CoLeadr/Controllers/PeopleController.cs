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
    public class PeopleController : Controller
    {
        private CoLeadrDBContext db = new CoLeadrDBContext();

        // GET: People
        public ActionResult Index()
        {
            return View(db.People.ToList());
        }

        // GET: People/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // GET: People/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PersonId,FirstName,LastName,PrimaryPhone,StreetAddress,City,State")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.People.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(person);
        }

        // GET: People/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PersonId,FirstName,LastName,PrimaryPhone,StreetAddress,City,State")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(person);
        }

        // GET: People/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.People.Find(id);
            db.People.Remove(person);
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

        //GET People/AddToGroup
        public ActionResult AddMemberToGroup(int? PersonId)
        {
           
                 if (PersonId == null)
                 {
                     return new HttpStatusCodeResult(HttpStatusCode.BadRequest); 
                 }

                 Person person = db.People.Find(PersonId);

                 if (person == null)
                 {
                     return HttpNotFound();
                 }
           
            SelectList allthegroups = new SelectList(db.Groups, "GroupId", "Name");

            PersonGroupingViewModel viewmodel = new PersonGroupingViewModel();
            viewmodel.PersonId = person.PersonId;
            viewmodel.FirstName = person.FirstName;
            viewmodel.LastName = person.LastName;
            viewmodel.AllGroups = allthegroups;

            //viewmodel.Memberships cannot be empty AND THEY ARE ALWAYS EMPTY
            if (person.Memberships == null)
            {
                    IList<Group> members = new List<Group>();
                    viewmodel.Memberships = members; 
            }
            else
            {
                viewmodel.Memberships = person.Memberships;
            }

            
            return View(viewmodel);
            }

        //POST: People/AddToGroup 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMemberToGroup(PersonGroupingViewModel viewmodel)
        {
            if (ModelState.IsValid) 
            {
                //add the person to the group
                int PersonId = viewmodel.PersonId;
                int GroupId = viewmodel.SelectedGroupId;
                Person person = db.People.Find(PersonId);
                Group group = db.Groups.Find(GroupId);
               
                group.Members.Add(person);
                db.SaveChanges();

                //if the person is not already in the member lists for group's projects, add them 
                foreach(Project project in group.Projects)
                {
                    if (project.AssignedGroupMembers.Contains(person) == false)
                    {
                        project.AssignedGroupMembers.Add(person);
                        db.SaveChanges();
                    }
                }

                //create a new viewmodel to pass back to the view with updated memberships (v.important)
                PersonGroupingViewModel vm = new PersonGroupingViewModel();
                SelectList allthegroups = new SelectList(db.Groups, "GroupId", "Name");
                vm.PersonId = person.PersonId;
                vm.FirstName = person.FirstName;
                vm.LastName = person.LastName;
                vm.Memberships = person.Memberships;
                vm.AllGroups = allthegroups; 

                return View(vm);
               
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Something is wrong with the returned viewmodel.");

        }

        //GET People/RemoveMemberFromGroup
        public ActionResult RemoveMemberFromGroup(int? PersonId)
        {

            if (PersonId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Person person = db.People.Find(PersonId);

            if (person == null)
            {
                return HttpNotFound();
            }

            //create a list of groups the member is already in for the selectlist
            List<Group> membergroups = new List<Group>(); 
            foreach(Group group in person.Memberships)
            {
                membergroups.Add(group); 
            }

            SelectList membershipgroups = new SelectList(membergroups, "GroupId", "Name");

            PersonGroupingViewModel viewmodel = new PersonGroupingViewModel();
            viewmodel.PersonId = person.PersonId;
            viewmodel.FirstName = person.FirstName;
            viewmodel.LastName = person.LastName;
            viewmodel.AllGroups = membershipgroups;

            //viewmodel.Memberships cannot be empty 
            if (person.Memberships == null)
            {
                IList<Group> members = new List<Group>();
                viewmodel.Memberships = members;
            }
            else
            {
                viewmodel.Memberships = person.Memberships;
            }


            return View(viewmodel);
        }

        //POST: People/RemoveMemberFromGroup 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveMemberFromGroup(PersonGroupingViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                int PersonId = viewmodel.PersonId;
                int GroupId = viewmodel.SelectedGroupId;
                Person person = db.People.Find(PersonId);
                Group group = db.Groups.Find(GroupId);

                group.Members.Remove(person);
                db.SaveChanges();

                //if the person is in the member lists for group's projects, remove them 
                foreach (Project project in group.Projects)
                {
                    if (project.AssignedGroupMembers.Contains(person))
                    {
                        project.AssignedGroupMembers.Remove(person);
                        db.SaveChanges();
                    }
                }

                //create a new viewmodel to pass back to the view with updated memberships (v.important)
                PersonGroupingViewModel vm = new PersonGroupingViewModel();

                List<Group> membergroups = new List<Group>();
                foreach (Group g in person.Memberships)
                {
                    membergroups.Add(g);
                }

                SelectList membershipgroups = new SelectList(membergroups, "GroupId", "Name");

                vm.PersonId = person.PersonId;
                vm.FirstName = person.FirstName;
                vm.LastName = person.LastName;
                vm.Memberships = person.Memberships;
                vm.AllGroups = membershipgroups;

                return View(vm);

            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Something is wrong with the returned viewmodel.");

        }


    }
}
