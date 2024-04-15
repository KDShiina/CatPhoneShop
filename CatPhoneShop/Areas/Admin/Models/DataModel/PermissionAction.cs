using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatPhoneShop.Areas.Admin.Models.DataModel
{
    public class PermissionAction
    {
        public int PermissionID { get; set; }
        public string PermissionName{ get; set; }
        public string Description { get; set; }
        public bool IsGranted { get; set; }
    }
}