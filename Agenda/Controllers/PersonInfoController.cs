using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Agenda.DataAccess;
using Agenda.Models;

namespace Agenda.Controllers
{
    public class PersonInfoController : Controller
    {
        private AgendaContext db = new AgendaContext();

        public ActionResult Index(string searchString)
        {
            var contact = from c in db.ContactInfo
                          select c;

            if (!String.IsNullOrEmpty(searchString))
            {
                contact = contact.Where(c =>
               c.Person.LastName.ToUpper().Contains(searchString.ToUpper())
                ||
               c.Person.FirstName.ToUpper().Contains(searchString.ToUpper()));
            }

            return View(contact.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonInfo personInfo = db.ContactInfo.Find(id);
            if (personInfo == null)
            {
                return HttpNotFound();
            }
            return View(personInfo);
        }

        public ActionResult Create()
        {
            ViewBag.PersonID = new SelectList(db.Persons, "PersonID", "FirstName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PersonInfoID,PersonID,Phone,Mail,Website")] PersonInfo personInfo)
        {
            if (ModelState.IsValid)
            {
                db.ContactInfo.Add(personInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PersonID = new SelectList(db.Persons, "PersonID", "FirstName", personInfo.PersonID);
            return View(personInfo);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonInfo personInfo = db.ContactInfo.Find(id);
            if (personInfo == null)
            {
                return HttpNotFound();
            }
            ViewBag.PersonID = new SelectList(db.Persons, "PersonID", "FirstName", personInfo.PersonID);
            return View(personInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PersonInfoID,PersonID,Phone,Mail,Website")] PersonInfo personInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PersonID = new SelectList(db.Persons, "PersonID", "FirstName", personInfo.PersonID);
            return View(personInfo);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonInfo personInfo = db.ContactInfo.Find(id);
            if (personInfo == null)
            {
                return HttpNotFound();
            }
            return View(personInfo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PersonInfo personInfo = db.ContactInfo.Find(id);
            db.ContactInfo.Remove(personInfo);
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
