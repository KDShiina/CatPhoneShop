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

namespace CatPhoneShop.Areas.Admin.Controllers
{
    [AuthorizeBusiness]
    public class UserPermissionsController : Controller
    {
        private ConnectionDBContext db = new ConnectionDBContext();

        // GET: Admin/UserPermissions
        public ActionResult Index(string id)
        {
            var userPermission = db.UserPermission.Where(p => p.BusinessID == id);
            var ente = from g in db.UserBusiness
                       join p in db.UserPermission on g.BusinessID equals p.BusinessID
                       where g.BusinessID == id
                       select  g.BusinessName;

            foreach (var item in ente)
            {
                ViewBag.ente = item;
            }


            return View(userPermission.ToList());
        }

        // GET: Admin/UserPermissions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserPermission userPermission = db.UserPermission.Find(id);
            if (userPermission == null)
            {
                return HttpNotFound();
            }
            return View(userPermission);
        }

        // GET: Admin/UserPermissions/Create
        public ActionResult Create()
        {
            ViewBag.BusinessID = new SelectList(db.UserBusiness, "BusinessID", "BusinessName");
            return View();
        }

        // POST: Admin/UserPermissions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PermissionID,PermissionName,PermissionDescription,BusinessID")] UserPermission userPermission)
        {
            if (ModelState.IsValid)
            {
                db.UserPermission.Add(userPermission);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BusinessID = new SelectList(db.UserBusiness, "BusinessID", "BusinessName", userPermission.BusinessID);
            return View(userPermission);
        }

        // GET: Admin/UserPermissions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserPermission userPermission = db.UserPermission.Find(id);
            if (userPermission == null)
            {
                return HttpNotFound();
            }
            ViewBag.BusinessID = new SelectList(db.UserBusiness, "BusinessID", "BusinessName", userPermission.BusinessID);
            return View(userPermission);
        }

        // POST: Admin/UserPermissions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PermissionID,PermissionName,PermissionDescription,BusinessID")] UserPermission userPermission)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userPermission).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = userPermission.BusinessID });
            }
            ViewBag.BusinessID = new SelectList(db.UserBusiness, "BusinessID", "BusinessName", userPermission.BusinessID);
            return View(userPermission);
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
