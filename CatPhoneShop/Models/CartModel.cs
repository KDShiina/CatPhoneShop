using CatPhoneShop.Areas.Admin.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatPhoneShop.Models
{
    [Serializable]
    public class CartModel
    {

        public Product Product { get; set; }

        public int Quanlity { get; set; }

    }
}