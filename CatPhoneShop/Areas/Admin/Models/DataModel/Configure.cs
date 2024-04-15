using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatPhoneShop.Areas.Admin.Models.DataModel
{
    [Table("Configure")]
    public class Configure
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ConfigID { get; set; }


        [Display(Name = "Logo Website")]
        [MaxLength(256, ErrorMessage = "Tên File Logo Quá Dài Vui Lòng Đổi Lại tên")]
        public string Logo { get; set; }


        [Display(Name = "Icon Favicon")]
        [MaxLength(256, ErrorMessage = "Tên File Favicon Quá Dài Vui Lòng Đổi Lại tên")]
        public string Favicon { get; set; }


        [Display(Name = "Tên Công Ty,Doanh Nghiệp")]
        [MaxLength(256,ErrorMessage = "Tên Công Ty,Doanh Nghiệp Không Được Quá 256 Kí Tự")]
        public string NameCompany { get; set; }


        [Display(Name = "Mô tả Công Ty,Doanh Nghiệp")]
        public string Description { get; set; }


        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }


        [Display(Name = "Email")]
        [MaxLength(50, ErrorMessage = "Email Không Được Quá 50 Kí Tự")]
        public string Email { get; set; }

        [Display(Name = "Email Nhận Tin Khách Hàng")]
        [MaxLength(50, ErrorMessage = "Email Nhận Tin Không Được Quá 50 Kí Tự")]
        public string EmailReceive { get; set; }


        [Display(Name = "Số Điện Thoại")]
        [MaxLength(11, ErrorMessage = "Số Điện Thoại Không Hợp Lệ")]
        public string Phone { get; set; }


        [Display(Name = "Hotline")]
        [MaxLength(150, ErrorMessage = "Hotline Tối Đa Là 150 Kí Tự")]
        public string Hotline { get; set; }

        [AllowHtml]
        [Display(Name = "Mã Nhúng Google Map")]
        public string Map { get; set; }

        [AllowHtml]
        [Display(Name = "Mã Nhúng FANPAGE FACEBOOK")]
        public string FanpageFacebook { get; set; }


        [Display(Name = "Link icon Facebook")]
        public string IconFacebook { get; set; }

        [Display(Name = "Link icon Twitter")]
        public string IconTwiiter{ get; set; }


        [Display(Name = "Link icon Instagram")]
        public string IconInstagram { get; set; }

        [Display(Name = "Link icon Youtube")]
        public string IconYoutube { get; set; }


        [Display(Name = "Link icon Google")]
        public string IconGoogle { get; set; }

        [Display(Name = "Tiêu đề trang")]
        [MaxLength(250, ErrorMessage = "MetaTitle Không Được Quá 250 Kí Tự")]
        public string MetaTitle { get; set; }

        [Display(Name = "Từ Khóa SEO")]
        [MaxLength(250, ErrorMessage = "MetaKeyWord Không Được Quá 250 Kí Tự")]
        public string MetaKeyword { get; set; }

        [Display(Name = "Mô tả SEO")]
        public string MetaDescription { get; set; }

    }
}