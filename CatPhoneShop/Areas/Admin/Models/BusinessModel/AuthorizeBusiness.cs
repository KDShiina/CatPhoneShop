using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatPhoneShop.Areas.Admin.Models.BusinessModel
{
    public class AuthorizeBusiness:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["UserID"] == null)
            {
                filterContext.Result = new RedirectResult("/Admin/Login/LoginAccount");
                return;
            }
            else
            {

                int userid = int.Parse(HttpContext.Current.Session["UserID"].ToString());
                string actionName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "Controller-" + filterContext.ActionDescriptor.ActionName;
                ConnectionDBContext db = new ConnectionDBContext();
                var admin = db.User.Where(a => a.UserID == userid && a.Status == true).FirstOrDefault();

                if (admin != null)
                {


                    var listPermission = from p in db.UserPermission
                                         join g in db.UserGrantPermission on p.PermissionID equals g.PermissionID
                                         where g.UserID == userid
                                         select p.PermissionName;

                    if (!listPermission.Contains(actionName))
                    {
                        filterContext.Result = new RedirectResult("/Admin/Home/HomeAuthorPermission");
                        return;
                    }
                }

            }

            
        }
    }
}