using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatPhoneShop.Areas.Admin.Models.DataModel
{
    [Table("Home")]
    public class Home
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HomeID { get; set; }

        [Display(Name = "Banner Cạnh Slider 1")]
        [MaxLength(250)]
        public string Banner_1 { get; set; }

        [Display(Name = "Link liên kết Banner Cạnh Slider 1")]
        [MaxLength(250)]
        public string Link_banner_1 { get; set; }


        [Display(Name = "Banner Cạnh Slider 2")]
        [MaxLength(250)]
        public string Banner_2 { get; set; }

        [Display(Name = "Link liên kết Banner Cạnh Slider 2")]
        [MaxLength(250)]
        public string Link_banner_2 { get; set; }



        [Display(Name = "Banner Dưới Slider 1")]
        [MaxLength(250)]
        public string Banner_3 { get; set; }

        [Display(Name = "Link liên kết Banner Dưới Slider 1")]
        [MaxLength(250)]
        public string Link_banner_3 { get; set; }

        [Display(Name = "Banner Dưới Slider 2")]
        [MaxLength(250)]
        public string Banner_4 { get; set; }
        [Display(Name = "Link liên kết Banner Dưới Slider 2")]
        [MaxLength(250)]
        public string Link_banner_4 { get; set; }




        //nhóm 1
        [Display(Name = "Hiển thị nhóm sản phẩm 1")]
        public bool Collection1_Status { get; set; }

        [Display(Name = "Tiêu đề nhóm sản phẩm 1")]
        [MaxLength(250)]
        public string title_category1 { get; set; }


        [Display(Name = "Chọn nhóm sản phẩm 1")]
        public int? Collection_home1 { get; set; }

        [Display(Name = "Link nút xem thêm nhóm sản phẩm 1")]
        [MaxLength(250)]
        public string button_more1 { get; set; }



        //nhóm 2
        [Display(Name = "Hiển thị nhóm sản phẩm 2")]
        public bool Collection2_Status { get; set; }

        [Display(Name = "Tiêu đề nhóm sản phẩm 2")]
        [MaxLength(250)]
        public string title_category2 { get; set; }

        [Display(Name = "Banner Nhóm Sản Phẩm 2")]
        [MaxLength(250)]
        public string Banner_Collection2 { get; set; }

        [Display(Name = "Link liên kết Banner Nhóm Sản Phẩm 2")]
        [MaxLength(250)]
        public string Banner_Collection2_Link { get; set; }


        [Display(Name = "Chọn nhóm sản phẩm 2 ")]
        public int? Collection_home2 { get; set; }

        [Display(Name = "Link nút xem thêm nhóm sản phẩm 2")]
        [MaxLength(250)]
        public string button_more2 { get; set; }


        [Display(Name = "Tiêu đề bên trái")]
        [MaxLength(250)]
        public string title_left2 { get; set; }

        [Display(Name = "Chọn nhóm sản phẩm phía bên trái ")]
        public int? Collection_left2 { get; set; }

        [Display(Name = "Link nút xem thêm sản phẩm phía bên trái")]
        [MaxLength(250)]
        public string button_more_left2 { get; set; }





        //nhóm 3
        [Display(Name = "Hiển thị nhóm sản phẩm 3")]
        public bool Collection3_Status { get; set; }

        [Display(Name = "Tiêu đề nhóm sản phẩm 3")]
        [MaxLength(250)]
        public string title_category3 { get; set; }

        [Display(Name = "Banner Nhóm Sản Phẩm 3")]
        [MaxLength(250)]
        public string Banner_Collection3 { get; set; }

        [Display(Name = "Link liên kết Banner Nhóm Sản Phẩm 3")]
        [MaxLength(250)]
        public string Banner_Collection3_Link { get; set; }


        [Display(Name = "Chọn nhóm sản phẩm 3 ")]
        public int? Collection_home3 { get; set; }

        [Display(Name = "Link nút xem thêm nhóm sản phẩm 3")]
        [MaxLength(250)]
        public string button_more3 { get; set; }


        [Display(Name = "Tiêu đề bên phải")]
        [MaxLength(250)]
        public string title_right3 { get; set; }

        [Display(Name = "Chọn nhóm sản phẩm phía bên phải ")]
        public int? Collection_right3 { get; set; }

        [Display(Name = "Link nút xem thêm sản phẩm phía bên phải")]
        [MaxLength(250)]
        public string button_more_right3 { get; set; }





        [Display(Name = "Hiển thị nhóm sản phẩm 4")]
        public bool Collection4_Status { get; set; }

        [Display(Name = "Tiêu đề nhóm sản phẩm 4")]
        [MaxLength(250)]
        public string title_category4 { get; set; }

        [Display(Name = "Banner Nhóm Sản Phẩm 4")]
        [MaxLength(250)]
        public string Banner_Collection4 { get; set; }

        [Display(Name = "Link liên kết Banner Nhóm Sản Phẩm 4")]
        [MaxLength(250)]
        public string Banner_Collection4_Link { get; set; }


        [Display(Name = "Chọn nhóm sản phẩm 4 ")]
        public int? Collection_home4 { get; set; }

        [Display(Name = "Link nút xem thêm nhóm sản phẩm 4")]
        [MaxLength(250)]
        public string button_more4 { get; set; }


        [Display(Name = "Tiêu đề bên trái")]
        [MaxLength(250)]
        public string title_left4 { get; set; }

        [Display(Name = "Chọn nhóm sản phẩm phía bên trái ")]
        public int? Collection_left4 { get; set; }

        [Display(Name = "Link nút xem thêm sản phẩm phía bên trái")]
        [MaxLength(250)]
        public string button_more_left4 { get; set; }






        //blog

        [Display(Name = "Hiển thị nhóm tin tức")]
        public bool Blog_Status { get; set; }

        [Display(Name = "Tiêu đề nhóm tin tức")]
        [MaxLength(250)]
        public string title_blog { get; set; }

        [Display(Name = "Chọn nhóm Blog tin tức ")]
        public int? Blog_home { get; set; }


    }
}