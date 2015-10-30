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
        public ActionResult AddMemberToGroup(PersonGroupingViewModel vm)
        {
            if (ModelState.IsValid) 
            {
                int PersonId = vm.PersonId;
                int GroupId = vm.SelectedGroupId;
                Person person = db.People.Find(PersonId);
                Group group = db.Groups.Find(GroupId);

               /* if (group.Members == null)
                {
                    IList<Person> groupmembers = new List<Person>();
                    groupmembers.Add(person);
                    group.Members = groupmembers;
                }
                else
                {*/
                    group.Members.Add(person);
               // }
                

                db.SaveChanges();

                vm.Memberships = person.Memberships; 
               return View(vm);
               
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Something is wrong with the returned viewmodel.");

        }

    }
}
