using CatPhoneShop.Areas.Admin.Models.BusinessModel;
using CatPhoneShop.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatPhoneShop.Controllers
{
    public class blogsController : Controller
    {
        private ConnectionDBContext db = new ConnectionDBContext();
        // GET: blogs
        public ActionResult GetBlogID(int BlogID, string Alias, int? page)
        {
            var model = from bl in db.Blogs join ar in db.Articles 
                        on bl.BlogID equals ar.BlogID
                        where bl.BlogID == BlogID && bl.Status == true && ar.Status ==true
                        orderby ar.CreatedAt descending
                        select new BlogModel {
                            BlogName = bl.BlogName,
                            Alias_Blog = bl.Alias,
                            Alias_Article=ar.Alias,
                            Content = ar.Content,
                            DescriptShort =ar.DescriptShort,
                            CreatedAt = ar.CreatedAt,
                            BlogID = bl.BlogID,
                            ArticleID = ar.ArticleID,
                            Author =ar.Author,
                            Images = ar.Images,
                            MetaDescription=bl.MetaDescription,
                            MetaKeyword=bl.MetaKeyword,
                            MetaTitle=bl.MetaTitle
                            
                        };
            int pagesize = 5;
            int pageNumber = (page ?? 1);

            ViewBag.count_article = model.Count();



            var getNameBlog = from a in db.Blogs where a.BlogID == BlogID
                              select new BlogModel { BlogName=a.BlogName,MetaDescription=a.MetaDescription,MetaTitle=a.MetaKeyword,MetaKeyword=a.MetaKeyword};
            ViewBag.getNameBlog = getNameBlog.ToList();


            var getBlogNew = (from ar in db.Articles where ar.Status == true orderby ar.CreatedAt descending
                             select new BlogModel
                             {
                                 Alias_Article = ar.Alias,
                                 Content = ar.Content,
                                 DescriptShort = ar.DescriptShort,
                                 CreatedAt = ar.CreatedAt,
                                 ArticleID = ar.ArticleID,
                                 Author = ar.Author,
                                 Images = ar.Images,
                                 ArticleName=ar.ArticleName

                             }).Take(5);
            ViewBag.getBlogNew = getBlogNew.ToList();

            return View(model.ToPagedList(pageNumber, pagesize));
        }


    }
}