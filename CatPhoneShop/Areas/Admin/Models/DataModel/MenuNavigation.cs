using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatPhoneShop.Areas.Admin.Models.DataModel
{
    [Table("MenuNavigation")]
    public class MenuNavigation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MenuID { get; set; }

        [Display(Name = "Thứ tự hiển thị")]
        public int? Sequence { get; set; }

        [Display(Name = "Tên Menu")]
        [Required(ErrorMessage ="Tên Menu Không Được Bỏ Trống")]
        [MaxLength(50,ErrorMessage ="Tên Menu Không Được Quá 50 Kí Tự")]
        public string MenuName { get; set; }


        [Display(Name = "Icon hình ảnh")]
        public string IconMenu { get; set; }



        [Display(Name = "Chọn Cấp Thư Mục")]
        public int MenuParents { get; set; }

        [Display(Name = "Kiểu Trang")]
        public int PageStyle { get; set; }

        [Display(Name = "Thể Loại Trang")]
        public int? FormatPage { get; set; }

        [Display(Name = "Nhóm Liên Kết")]
        public int? GroupLink { get; set; }

        [AllowHtml]
        [Column(TypeName = "ntext")]
        [Display(Name = "Trang Nội Dung")]
        public string PageContent { get; set; }

        [Display(Name = "Link Liên Kết")]
        [MaxLength(250, ErrorMessage = "Link Liên Kết Không Được Quá 250 Kí Tự")]
        public string PageLink { get; set; }


        [Display(Name = "Tiêu đề trang")]
        [MaxLength(250, ErrorMessage = "MetaTitle Không Được Quá 250 Kí Tự")]
        public string MetaTitle { get; set; }

        [Display(Name = "Từ Khóa SEO")]
        [MaxLength(250, ErrorMessage = "MetaKeyWord Không Được Quá 250 Kí Tự")]
        public string MetaKeyword { get; set; }


        [Display(Name = "Mô tả SEO")]
        public string MetaDescription { get; set; }


        [Display(Name = "Ngày Tạo")]
        public DateTime? CreatedAt { get; set; }

        [Display(Name = "Trạng Thái")]
        public bool Status { get; set; }


    }
}