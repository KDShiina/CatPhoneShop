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
    public class HomesController : Controller
    {
        private ConnectionDBContext db = new ConnectionDBContext();

    

        public ActionResult Edit(int? id)
        {
            var listcate = db.Categorys.Where(x=>x.Status == true);
            ViewBag.categorys = listcate.ToList();


            var listblog = db.Blogs.Where(x => x.Status == true);
            ViewBag.blogs = listblog.ToList();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Home home = db.Homes.Find(id);
            if (home == null)
            {
                return HttpNotFound();
            }
            return View(home);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Home home)
        {
            if (ModelState.IsValid)
            {
                db.Entry(home).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = home.HomeID });
            }
            return View(home);
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
