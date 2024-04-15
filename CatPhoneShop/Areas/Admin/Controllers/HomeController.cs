using CatPhoneShop.Areas.Admin.Models.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatPhoneShop.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private ConnectionDBContext db = new ConnectionDBContext();
        // GET: Admin/Home
        public ActionResult Index()
        {
            var count_user = (from pro in db.User select pro.UserID).Count();

            ViewBag.count_user = count_user;

            var count_product = (from pro in db.Products select pro.ProductID).Count();

            ViewBag.count_product = count_product;

            var count_baiviet = (from bv in db.Articles select bv.ArticleID).Count();

            ViewBag.count_baiviet = count_baiviet;

            var count_order = (from or in db.Orders where or.ViewStatus == false select or.orderID).Count();

            if (count_order > 0)
            {
                Session["countnewcart"] = count_order;
            }

            var model = db.Articles.Where(x => x.Status == true).OrderByDescending(x => x.CreatedAt).Take(10);
            ViewBag.newblog = model.ToList();



            var newproduct = db.Products.Where(x => x.Status == true).OrderByDescending(x => x.CreatedAt).Take(10);
            ViewBag.newproduct = newproduct.ToList();


            return View();
        }

        public ActionResult LogoutUser()
        {
            Session["UserID"] = null;
            Session["Username"] = null;
            Session["Password"] = null;
            Session["Image"] = null;
            return Redirect("~/Admin/Login/LoginAccount");
        }

        public ActionResult HomeAuthorPermission()
        {
            return View();
        }








    }
}