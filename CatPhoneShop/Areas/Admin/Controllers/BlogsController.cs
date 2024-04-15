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
using PagedList;

namespace CatPhoneShop.Areas.Admin.Controllers
{
    [AuthorizeBusiness]
    public class BlogsController : Controller
    {


        private ConnectionDBContext db = new ConnectionDBContext();

        // GET: Admin/Blogs
        public ActionResult Index(int? page, string q)
        {
            var count_blog = (from cate in db.Blogs select cate.BlogID).Count();

            ViewBag.count_blog = count_blog;




            var model = from p in db.Blogs orderby p.CreatedAt descending select p;
            int pagesize = 15;
            int pageNumber = (page ?? 1);


            if (!string.IsNullOrEmpty(q))
            {
                model = model.Where(x => x.BlogName.Contains(q)).OrderByDescending(x => x.CreatedAt);
            }

            ViewBag.keyword_search = q;

            return View(model.ToPagedList(pageNumber, pagesize));

        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Blog blog, bool status_mi)
        {
            if (ModelState.IsValid)
            {


                bool tus;
                if (status_mi == true)
                {
                    tus = true;
                }
                else
                {
                    tus = false;
                }

                blog.Author = Session["Username"].ToString();
                blog.CreatedAt = DateTime.Now;
                blog.Status = tus;



                var check_blog = db.Blogs.Count(x => x.BlogName == blog.BlogName);
                var check_alias = db.Blogs.Count(x => x.Alias == blog.Alias);
                if (check_blog > 0)
                {
                    ViewBag.check_blog = "Tên Danh Mục Blog Đã Tồn Tại";
                }
                else if (check_alias > 0)
                {
                    ViewBag.check_alias = "Đường Dẫn URL Đã Tồn Tại";
                }
                else
                {

                    db.Blogs.Add(blog);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(blog);
        }

        // GET: Admin/Blogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Admin/Blogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Blog blog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blog);
        }

        // GET: Admin/Blogs/Delete/5
        public ActionResult Delete(int? id)
        {
            Blog blog = db.Blogs.Find(id);


            db.Blogs.Remove(blog);
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
