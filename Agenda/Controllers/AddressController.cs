using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Agenda.DataAccess;
using Agenda.Models;
using System;

namespace Agenda.Controllers
{
    public class AddressController : Controller
    {
        private AgendaContext db = new AgendaContext();

        public ActionResult Index(string searchString)
        {
            var addresses = from a in db.Addresses
                          select a;

            if (!String.IsNullOrEmpty(searchString))
            {
                addresses = addresses.Where(a =>
               a.Person.LastName.ToUpper().Contains(searchString.ToUpper())
                ||
               a.Person.FirstName.ToUpper().Contains(searchString.ToUpper()));
            }

            return View(addresses.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        public ActionResult Create()
        {
            ViewBag.PersonID = new SelectList(db.Persons, "PersonID", "FirstName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AddressID,PersonID,Direction,ZIP,Country,State")] Address address)
        {
            if (ModelState.IsValid)
            {
                db.Addresses.Add(address);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PersonID = new SelectList(db.Persons, "PersonID", "FirstName", address.PersonID);
            return View(address);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            ViewBag.PersonID = new SelectList(db.Persons, "PersonID", "FirstName", address.PersonID);
            return View(address);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AddressID,PersonID,Direction,ZIP,Country,State")] Address address)
        {
            if (ModelState.IsValid)
            {
                db.Entry(address).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PersonID = new SelectList(db.Persons, "PersonID", "FirstName", address.PersonID);
            return View(address);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Address address = db.Addresses.Find(id);
            db.Addresses.Remove(address);
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
