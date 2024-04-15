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
using System.IO;

namespace CatPhoneShop.Areas.Admin.Controllers
{
    [AuthorizeBusiness]
    public class ArticlesController : Controller
    {
        private ConnectionDBContext db = new ConnectionDBContext();

        // GET: Admin/Articles
        public ActionResult Index(int? page, string q)
        {
            var count_article = (from pro in db.Articles select pro.ArticleID).Count();

            ViewBag.count_article = count_article;

            var model = from p in db.Articles.Include(p => p.Blogs) orderby p.CreatedAt descending select p;
            int pagesize = 15;
            int pageNumber = (page ?? 1);

            if (!string.IsNullOrEmpty(q))
            {
                model = model.Where(x => x.ArticleName.Contains(q) || x.Blogs.BlogName.Contains(q)).OrderByDescending(x => x.CreatedAt);
            }

            ViewBag.keyword_search = q;

            return View(model.ToPagedList(pageNumber, pagesize));
        }


        public ActionResult Create()
        {
            ViewBag.BlogID = new SelectList(db.Blogs, "BlogID", "BlogName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Article article, HttpPostedFileBase image_avatar, bool status_mi)
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

                    article.Author = Session["Username"].ToString();
                    article.CreatedAt = DateTime.Now;
                    article.Status = tus;

                    var check_article = db.Articles.Count(x => x.BlogID == article.ArticleID);
                    var check_alias = db.Categorys.Count(x => x.Alias == article.Alias);
                    if (check_article > 0)
                    {
                        ViewBag.check_cate = "Tên Bài Viết  Đã Tồn Tại";
                    }
                    else if (check_alias > 0)
                    {
                        ViewBag.check_alias = "Đường Dẫn URL Đã Tồn Tại";
                    }
                    else
                    {
                        if (image_avatar != null)
                        {
                            var filename = Path.GetFileName(image_avatar.FileName);
                            var path = Path.Combine(Server.MapPath("~/Upload/Images"), filename);


                            image_avatar.SaveAs(path);
                            article.Images = "/Upload/Images/" + image_avatar.FileName;
                        }
                        else
                        {
                            article.Images = "";
                        }

                        db.Articles.Add(article);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }

            }

            ViewBag.BlogID = new SelectList(db.Blogs, "BlogID", "BlogName", article.BlogID);
            return View(article);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.BlogID = new SelectList(db.Blogs, "BlogID", "BlogName", article.BlogID);
            return View(article);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Article article, HttpPostedFileBase image_avatar)
        {
            if (ModelState.IsValid)
            {

                if (image_avatar != null)
                {
                    var filename = Path.GetFileName(image_avatar.FileName);
                    var path = Path.Combine(Server.MapPath("~/Upload/Images"), filename);
                    image_avatar.SaveAs(path);
                    article.Images = "/Upload/Images/" + image_avatar.FileName;
                }



                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BlogID = new SelectList(db.Blogs, "BlogID", "BlogName", article.BlogID);
            return View(article);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Article articleToDelete = db.Articles.FirstOrDefault(a => a.ArticleID == id);

            if (articleToDelete == null)
            {
                return HttpNotFound();
            }

            string imagePath = articleToDelete.Images;

            if (!string.IsNullOrEmpty(imagePath))
            {
                string fullPath = Server.MapPath("~/Upload/Images/" + Path.GetFileName(imagePath));
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }

            db.Articles.Remove(articleToDelete);
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
