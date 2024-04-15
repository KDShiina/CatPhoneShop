using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatPhoneShop.Areas.Admin.Models.ViewModel
{
    public class MenuModel
    {
        public int MenuID { get; set; }

        public int MenuParents { get; set; }

        public string MenuName { get; set; }

        public string PageContent { get; set; }
    }
}