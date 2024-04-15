using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CatPhoneShop.Areas.Admin.Models.BusinessModel;
using CatPhoneShop.Areas.Admin.Models.DataModel;
using System.IO;
using PagedList;

namespace CatPhoneShop.Areas.Admin.Controllers
{
    [AuthorizeBusiness]
    public class ContactsController : Controller
    {
        private ConnectionDBContext db = new ConnectionDBContext();

        // GET: Admin/Contacts
        public ActionResult Index(int? page,string q)
        {
            var count_contact = (from con in db.Contacts select con.ContactID).Count();

            ViewBag.count_contact = count_contact;




            var model = from p in db.Contacts orderby p.CreatedAt descending select p;
            int pagesize = 3;
            int pageNumber = (page ?? 1);


            if (!string.IsNullOrEmpty(q))
            {
                model = model.Where(x => x.FullName.Contains(q) || x.Email.Contains(q)).OrderByDescending(x => x.CreatedAt);
            }

            ViewBag.keyword_search = q;

            return View(model.ToPagedList(pageNumber, pagesize));
        }

        // GET: Admin/Contacts/Details/5
        public ActionResult Details(int? id)
        {

            Contact contact = db.Contacts.Find(id);
            contact.ViewStatus = true;
            db.Entry(contact).State = EntityState.Modified;
            db.SaveChanges();
            return View(contact);
        }






        public ActionResult Delete(int? id)
        {

            Contact contact = db.Contacts.Find(id);

            db.Contacts.Remove(contact);
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
