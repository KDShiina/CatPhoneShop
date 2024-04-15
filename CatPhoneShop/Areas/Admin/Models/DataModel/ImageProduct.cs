using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace CatPhoneShop.Areas.Admin.Models.DataModel
{
    [Table("ImageProduct")]
    public class ImageProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ImageProductID { get; set; }

        public string FileImages { get; set; }

        [ForeignKey("Products")]
        public int ProductID { get; set; }


        public virtual Product Products { get; set; }
    }
}