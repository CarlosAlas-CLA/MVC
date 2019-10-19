using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private ContactsModel db = new ContactsModel();
        // GET: Home
        public ActionResult Index()
        {
            var myEntitiesEmailAddress = db.MyEntitiesEmailAddress.Include(e => e.Contact);
            return View(myEntitiesEmailAddress.ToList());

        }//CreateContacts
        public ActionResult CreateContacts()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateContacts(EmailAddress e)
        {
            if (ModelState.IsValid)
            {
                db.MyEntitiesEmailAddress.Add(e);
                db.SaveChanges();
                return RedirectToAction("SentConfirmation");
            }
            return View();
        }
        //Show Contacts
        public ActionResult ShowContacts()
        {
            var myEntitiesEmailAddress = db.MyEntitiesEmailAddress.Include(e => e.Contact);
            return View(myEntitiesEmailAddress.ToList());
        }//Delete
        public ActionResult DeleteContacts(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailAddress emailAddress = db.MyEntitiesEmailAddress.Find(id);
            if (emailAddress == null)
            {
                return HttpNotFound();
            }
            return View(emailAddress);
        }
        //SentConfirmation
        public ActionResult SentConfirmation()
        {
            return View();
        }//Delete Confirmation
        [HttpPost, ActionName("DeleteContacts")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmation(int id)
        {
            Contact contact = db.MyEntitiesContact.Find(id);
            EmailAddress emailAddress = db.MyEntitiesEmailAddress.Find(id);
            db.MyEntitiesContact.Remove(contact);
            db.MyEntitiesEmailAddress.Remove(emailAddress);
            db.SaveChanges();
            return RedirectToAction("ContactDeleted");
        }
        //Contact successfully deleted
        public ActionResult ContactDeleted()
        {
            return View();
        }
        //Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailAddress emailAddress = db.MyEntitiesEmailAddress.Find(id);
            if (emailAddress == null)
            {
                return HttpNotFound();
            }
            return View(emailAddress);
        }
        //Edit

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmailAddress e)
        {
            EmailAddress emailAddress = db.MyEntitiesEmailAddress.Find(e.EmailID);

            emailAddress.Contact.FirstName = e.Contact.FirstName;
            emailAddress.Contact.LastName = e.Contact.LastName;
            emailAddress.Email = e.Email;
            emailAddress.EmailType = e.EmailType;

            db.Entry(emailAddress).State = EntityState.Modified;

            db.SaveChanges();

            return RedirectToAction("EditSave");



        }
        public ActionResult EditSave()
        {


            return View();
        }





        // Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailAddress emailAddress = db.MyEntitiesEmailAddress.Find(id);
            if (emailAddress == null)
            {
                return HttpNotFound();
            }
            return View(emailAddress);
        }
    }
}