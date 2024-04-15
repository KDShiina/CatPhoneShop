using CatPhoneShop.Areas.Admin.Models.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatPhoneShop.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        ConnectionDBContext db = new ConnectionDBContext();

        public ActionResult LoginAccount()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginAccount(string email,string password)
        {
            Md5Encode md5 = new Md5Encode();
            var passmd5=md5.EncodeMd5Encrypt(password);
            var login = db.User.SingleOrDefault(x => x.Email == email && x.Password == passmd5 && x.Status == true);



           
            if (login != null)
            {
               

                Session["UserID"] = login.UserID;
                Session["Username"] = login.Username;
                Session["Email"] = login.Email;
                Session["Password"] = login.Password;
                Session["Image"] = login.Image;
                return Redirect("~/Admin/Home/Index");


            }
            else
            {
                ViewBag.error = "Tên Tài Khoản Hoặc Mật Khẩu Không Đúng";
            }
            return View();
        }


       
    }
}