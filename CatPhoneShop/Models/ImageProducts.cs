﻿using CatPhoneShop.Areas.Admin.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatPhoneShop.Models
{
    public class ImageProducts
    {
        public ICollection<ProductModel> Product { get; set; }


        public ICollection<ImageMores> Image { get; set; }


    }
}