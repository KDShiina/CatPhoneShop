using CatPhoneShop.Areas.Admin.Models.BusinessModel;
using CatPhoneShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatPhoneShop.Controllers
{
    public class articlesController : Controller
    {
        private ConnectionDBContext db = new ConnectionDBContext();
        // GET: articles
        public ActionResult GetArticleID(int ArticleID, string Alias, int BlogID)
        {
            var model = from ar in db.Articles
                        where ar.ArticleID == ArticleID
                        select new BlogModel
                        {
                            Alias_Blog = ar.Alias,
                            Alias_Article = ar.Alias,
                            Content = ar.Content,
                            DescriptShort = ar.DescriptShort,
                            CreatedAt = ar.CreatedAt,
                            ArticleID = ar.ArticleID,
                            Author = ar.Author,
                            Images = ar.Images,
                            ArticleName=ar.ArticleName,
                            MetaDescription_ar=ar.MetaDescription,
                            MetaKeyword_ar=ar.MetaKeyword,
                            MetaTitle_ar=ar.MetaTitle
                            
                        };

            var getNameBlog = from b in db.Blogs where b.BlogID == BlogID
                              select new BlogModel {
                                  BlogName = b.BlogName,
                                  BlogID = b.BlogID,
                                  Alias_Blog=b.Alias
                              };
            ViewBag.getNameBlog = getNameBlog;



            var getBlogNew = (from ar in db.Articles
                             where ar.Status == true
                             orderby ar.CreatedAt descending
                             select new BlogModel
                             {
                                 Alias_Article = ar.Alias,
                                 Content = ar.Content,
                                 DescriptShort = ar.DescriptShort,
                                 CreatedAt = ar.CreatedAt,
                                 ArticleID = ar.ArticleID,
                                 Author = ar.Author,
                                 Images = ar.Images,
                                 ArticleName = ar.ArticleName,
                                 BlogID=ar.BlogID,
                                 MetaDescription_ar = ar.MetaDescription,
                                 MetaKeyword_ar = ar.MetaKeyword,
                                 MetaTitle_ar = ar.MetaTitle


                             }).Take(5);
            ViewBag.getBlogNew = getBlogNew.ToList();

            return View(model.ToList());
        }
    }
}