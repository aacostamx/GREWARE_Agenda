using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Agenda.DataAccess;
using Agenda.Models;
using PagedList;

namespace Agenda.Controllers
{
    public class PersonController : Controller
    {
        private AgendaContext db = new AgendaContext();

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var persons = from p in db.Persons
                          select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                persons = persons.Where(p =>
               p.LastName.ToUpper().Contains(searchString.ToUpper())
                ||
               p.FirstName.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    persons = persons.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    persons = persons.OrderBy(s => s.DOB);
                    break;
                case "date_desc":
                    persons = persons.OrderByDescending(s => s.DOB);
                    break;
                default:
                    persons = persons.OrderBy(s => s.LastName);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(persons.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Persons.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PersonID,FirstName,LastName,DOB")] Person person)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Persons.Add(person);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException dex )
            {
                ModelState.AddModelError("", "Unable to save changes. Tryagain, and if the problem persists see your system administrator. Error: " + dex.ToString());
            }

            return View(person);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Persons.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PersonID,FirstName,LastName,DOB")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(person);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Persons.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.Persons.Find(id);
            db.Persons.Remove(person);
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
