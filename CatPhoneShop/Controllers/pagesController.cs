using CatPhoneShop.Areas.Admin.Models.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatPhoneShop.Controllers
{
    public class pagesController : Controller
    {
        private ConnectionDBContext db = new ConnectionDBContext();
        public ActionResult GetPageID(int MenuID,string MenuName)
        {
            var model = db.MenuNavigations.Where(x => x.MenuID == MenuID);
            return View(model.ToList());
        }
    }
}