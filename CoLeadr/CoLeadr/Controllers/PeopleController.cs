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

        //GET: EditGroupMemberships
        public ActionResult EditGroupMemberships(int? PersonId)
        {
            if(PersonId == null)
                 {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Person person = db.People.Find(PersonId);

            if (person == null)
            {
                return HttpNotFound();
            }

            List<Group> allgroups = new List<Group>(); 
            foreach (Group g in db.Groups)
            {
                allgroups.Add(g);
            }

            PersonGroupingViewModel viewmodel = new PersonGroupingViewModel();
            viewmodel.PersonId = person.PersonId;
            viewmodel.FirstName = person.FirstName;
            viewmodel.LastName = person.LastName;
            viewmodel.AllAvailableGroups = allgroups;

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

                //add person to group's projects & create ppr with removewithgroup = true
                List<Person> isingroupalready = new List<Person>();
                foreach(Project project in group.Projects) //for every project the group is assigned to
                {
                    foreach(PersonProjectRecord ppr in project.PersonProjectRecords) //for every ppr on the project
                    {
                        //if they are already on the project and their rwg flag is false add them to this list
                        if (ppr.PersonId == person.PersonId && ppr.RemoveWithGroup == false)
                        {
                            isingroupalready.Add(person);
                        }
                    }
                    //if the list is empty (person not already in group  w/ false rwg flag) 
                    //then make a new ppr and add them to project with true rwg flag 
                    if(isingroupalready.Count == 0)
                    {
                        PersonProjectRecord newrecord = new PersonProjectRecord();
                        newrecord.PersonId = person.PersonId;
                        newrecord.ProjectId = project.ProjectId;
                        newrecord.RemoveWithGroup = true;

                        db.PersonProjectRecords.Add(newrecord); 
                    }
                }
                db.SaveChanges(); 
 

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
                //remove the person from the group
                int PersonId = viewmodel.PersonId;
                int GroupId = viewmodel.SelectedGroupId;
                Person person = db.People.Find(PersonId);
                Group group = db.Groups.Find(GroupId);

                group.Members.Remove(person);
                db.SaveChanges();

                //remove from projects if removewithgroup flag is true
                List<PersonProjectRecord> pprstoremove = new List<PersonProjectRecord>(); 
                foreach(Project project in group.Projects) //for each project in the group
                {
                    foreach(PersonProjectRecord ppr in project.PersonProjectRecords) //search pprs in the project
                    {
                        if(ppr.PersonId == person.PersonId && ppr.RemoveWithGroup == true)
                        {
                            //add this person's pprs where rwg = true to a list 
                            pprstoremove.Add(ppr);
                        }
                    }
                }
                //you've got to remove them this way (via list) because otherwise messes with the enumerator 
                //hella exceptions
                foreach(PersonProjectRecord ppr in pprstoremove)
                {
                    db.PersonProjectRecords.Remove(ppr); 
                }

                db.SaveChanges(); 


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
