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
    public class UsersController : Controller
    {
        private ConnectionDBContext db = new ConnectionDBContext();

        // GET: Admin/Users
        public ActionResult Index(int? page, string q)
        {
            var count_user = (from cus in db.User select cus.UserID).Count();

            ViewBag.count_user = count_user;

            var model = from p in db.User orderby p.CreatedAt descending select p;
            int pagesize = 15;
            int pageNumber = (page ?? 1);

            if (!string.IsNullOrEmpty(q))
            {
                model = model.Where(x => x.Username.Contains(q) || x.Email.Contains(q) || x.Fullname.Contains(q)).OrderByDescending(x => x.CreatedAt);
            }

            ViewBag.keyword_search = q;

            return View(model.ToPagedList(pageNumber, pagesize));
        }

        public ActionResult showGrantPermision(int id)
        {
            var listcontrol = db.UserBusiness.AsEnumerable();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in listcontrol)
            {
                items.Add(new SelectListItem() { Text = item.BusinessName, Value = item.BusinessID });
            }
            ViewBag.item = items;


                var listgranted = from g in db.UserGrantPermission
                                  join p in db.UserPermission on g.PermissionID equals p.PermissionID
                                  where g.UserID == id
                                  select new SelectListItem() { Value = p.PermissionID.ToString(), Text = p.PermissionDescription };

            ViewBag.listgranted = listgranted;
            Session["usergranted"] = id;
            var usergrant = db.User.Find(id);
            ViewBag.usergrant = usergrant.Username + "(" + usergrant.Fullname + ")";

            return View();
        }

        public JsonResult getPermissions(string id,int usertemp)
        {
            var listgranted = (from g in db.UserGrantPermission
                               join p in db.UserPermission on g.PermissionID equals p.PermissionID
                               where g.UserID == usertemp && p.BusinessID == id
                               select new PermissionAction { PermissionID = p.PermissionID, PermissionName = p.PermissionName, Description = p.PermissionDescription, IsGranted = true }).ToList();

            var listpermission = from p in db.UserPermission
                                 where p.BusinessID == id
                                 select new PermissionAction { PermissionID = p.PermissionID, PermissionName = p.PermissionName, Description = p.PermissionDescription, IsGranted = false };

            var listpermissionId = listgranted.Select(p => p.PermissionID);

            foreach (var item in listpermission)
            {
                if (!listpermissionId.Contains(item.PermissionID))
                {
                    listgranted.Add(item);
                }
            }
            return Json(listgranted.OrderBy(x => x.Description), JsonRequestBehavior.AllowGet);
        }
        

        // GET: Admin/Users/Details/5


        // GET: Admin/Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create(User user,HttpPostedFileBase image_avatar, bool status_mi)
        {
           
            if (ModelState.IsValid)
            {
                Md5Encode md5 = new Md5Encode();
                user.Password = md5.EncodeMd5Encrypt(user.Password);
                user.CreatedAt = DateTime.Now;

                bool tus;
                if(status_mi == true)
                {
                    tus = true;
                }
                else
                {
                    tus = false;
                }

                var checkemail = db.User.Count(x => x.Email == user.Email);
                if (checkemail > 0)
                {
                    ViewBag.erroremail = "Email Đăng Kí Đã Tồn Tại";
                }
                else
                {
                    if (image_avatar != null)
                    {
                        var filename = Path.GetFileName(image_avatar.FileName);
                        var path = Path.Combine(Server.MapPath("~/Upload/Images"), filename);


                        image_avatar.SaveAs(path);
                        user.Image = "/Upload/Images/" + image_avatar.FileName;
                    }
                    else
                    {
                        user.Image = "/Upload/Default/man-avatar.jpg";
                    }
                        user.Status = tus;
                        db.User.Add(user);
                        db.SaveChanges();
                        return RedirectToAction("Index");

                }
            }

            return View(user);
        }


        public string updatePermission(int id,int usertemps)
        {
            string message_permission = "";
            var grant = db.UserGrantPermission.Find(id, usertemps);
            if (grant == null)
            {
                UserGrantPermission ugp = new UserGrantPermission() { PermissionID=id,UserID=usertemps,Description="Được Cấp Quyền" };
                db.UserGrantPermission.Add(ugp);
                message_permission = "<div class='alert alert-success'>Đã Cập Nhật Thông Tin Cấp Quyền</div>";
            }
            else
            {
                db.UserGrantPermission.Remove(grant);
                message_permission = "<div class='alert alert-danger'>Đã Hủy Quyền</div>";
            }
            db.SaveChanges();
            return message_permission;
        }





        // GET: Admin/Users/Delete/5
        public ActionResult Delete(int? id)
        {

            User user = db.User.Find(id);

            if (user.Image != null)
            {
                var filename = Path.GetFileName(user.Image);
                System.IO.File.Delete(Request.PhysicalApplicationPath + "/Upload/Images/" + filename);
            }

            db.User.Remove(user);
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
