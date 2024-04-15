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

    [Table("Slide")]
    public class Slide
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SliderID { get; set; }


        [Display(Name = "Thứ tự sắp xếp")]
        [Required(ErrorMessage ="Thứ tự sắp xếp không được bỏ trống")]
        public int SortID { get; set; }

        [Display(Name = "Tên Slide")]
        [MaxLength(250, ErrorMessage = "Tên Sản Phẩm Không Được Vượt Quá 250 Kí Tự")]
        public string SliderName { get; set; }

        [Display(Name = "Mô tả Slide")]
        public string Description { get; set; }


        [Display(Name = "Hình Ảnh Slide")]
        public string Images { get; set; }


        [Display(Name = "URL Liên Kết")]
        [MaxLength(250, ErrorMessage = "URL liên kết Không Được Vượt Quá 250 Kí Tự")]
        public string URL { get; set; }


        [Display(Name = "Trạng Thái")]
        public bool Status { get; set; }

    }
}