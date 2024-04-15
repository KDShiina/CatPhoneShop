using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatPhoneShop.Areas.Admin.Models.DataModel
{
    [Table("OrderDetail")]
    public class OrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int orderDetailID { get; set; }

        [ForeignKey("Orders")]
        public int orderID { get; set; }


        [Display(Name ="Sản Phẩm")]
        [ForeignKey("Products")]
        public int ProductID { get; set; }

        [Display(Name = "Giá")]
        public decimal Price { get; set; }

        [Display(Name = "Số Lượng")]
        public int Quanlity { get; set; }

        [Display(Name ="Thành Tiền")]
        public float TotalProduct { get; set; }


        [Display(Name = "Trạng Thái")]
        public bool Status { get; set; }


        public virtual Order Orders { get; set; }
        public virtual Product Products { get; set; }
    }
}