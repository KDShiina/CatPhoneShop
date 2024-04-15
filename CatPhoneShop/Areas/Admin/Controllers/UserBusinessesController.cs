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
    public class UserBusinessesController : Controller
    {

        private ConnectionDBContext db = new ConnectionDBContext();

        // GET: Admin/UserBusinesses


        public ActionResult UpdateBusiness()
        {
            ReflectionController recontroller = new ReflectionController();
            List<Type> listControllerType = recontroller.GetControllers("CatPhoneShop.Areas.Admin.Controllers");
            List<string> listcontrollerOld = db.UserBusiness.Select(k => k.BusinessID).ToList();
            List<string> listPermissionOld = db.UserPermission.Select(k => k.PermissionName).ToList();

            foreach (var item in listControllerType)
            {
                if (!listcontrollerOld.Contains(item.Name))
                {
                    UserBusiness ub = new UserBusiness() { BusinessID = item.Name, BusinessName = "No Description" };
                    db.UserBusiness.Add(ub);

                }
                List<string> listPermission = recontroller.GetActions(item);

                foreach (var per in listPermission)
                {
                    if(!listPermissionOld.Contains(item.Name + "-" + per))
                    {
                        UserPermission up = new UserPermission() { PermissionName = item.Name + "-" + per, PermissionDescription="No Description",BusinessID=item.Name };
                        db.UserPermission.Add(up);
                    }
                }

            }
            db.SaveChanges();
            TempData["alert"]= "<div class='alert alert-success'>Đã Cập Nhật Thành Công</ div > ";
            return RedirectToAction("Index");
        }

        public ActionResult Index()
        {
            return View(db.UserBusiness.ToList());
        }

        // GET: Admin/UserBusinesses/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserBusiness userBusiness = db.UserBusiness.Find(id);
            if (userBusiness == null)
            {
                return HttpNotFound();
            }
            return View(userBusiness);
        }

        // GET: Admin/UserBusinesses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/UserBusinesses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BusinessID,BusinessName")] UserBusiness userBusiness)
        {
            if (ModelState.IsValid)
            {
                db.UserBusiness.Add(userBusiness);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userBusiness);
        }

        // GET: Admin/UserBusinesses/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserBusiness userBusiness = db.UserBusiness.Find(id);
            if (userBusiness == null)
            {
                return HttpNotFound();
            }
            return View(userBusiness);
        }

        // POST: Admin/UserBusinesses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BusinessID,BusinessName")] UserBusiness userBusiness)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userBusiness).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userBusiness);
        }

        // GET: Admin/UserBusinesses/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserBusiness userBusiness = db.UserBusiness.Find(id);
            if (userBusiness == null)
            {
                return HttpNotFound();
            }
            return View(userBusiness);
        }

        // POST: Admin/UserBusinesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            UserBusiness userBusiness = db.UserBusiness.Find(id);
            db.UserBusiness.Remove(userBusiness);
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
