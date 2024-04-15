using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatPhoneShop.Areas.Admin.Models.DataModel
{
    [Table("Article")]
    public class Article
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ArticleID { get; set; }

        [Display(Name = "Tên Bài Viết")]
        [Required(ErrorMessage = "Tên Bài Viết Không Được Bỏ Trống")]
        [MaxLength(250, ErrorMessage = "Tên Bài Viết Không Được Vượt Quá 250 Kí Tự")]
        public string ArticleName { get; set; }

        [Display(Name = "Đường Dẫn URL")]
        [Required(ErrorMessage = "URL Không Được Bỏ Trống")]
        [MaxLength(250, ErrorMessage = "URL Không Được Vượt Quá 250 Kí Tự")]
        [Index(IsUnique = true)]
        [Column(TypeName = "varchar")]
        public string Alias { get; set; }


        [Display(Name = "Mô tả ngắn")]
        [AllowHtml]
        public string DescriptShort { get; set; }


        [Required]
        [ForeignKey("Blogs")]
        public int BlogID { get; set; }

        [Display(Name = "Ảnh đại diện")]
        [MaxLength(500)]
        public string Images { get; set; }

        [Display(Name = "Tác Giả")]
        public string Author { get; set; }


        [AllowHtml]
        [Column(TypeName = "ntext")]
        [Display(Name = "Nội Dung")]
        public string Content { get; set; }


        [Display(Name = "Tiêu đề trang")]
        [MaxLength(250, ErrorMessage = "MetaTitle Không Được Quá 250 Kí Tự")]
        public string MetaTitle { get; set; }

        [Display(Name = "Từ Khóa SEO")]
        [MaxLength(250, ErrorMessage = "MetaKeyWord Không Được Quá 250 Kí Tự")]
        public string MetaKeyword { get; set; }

        [Display(Name = "Mô tả SEO")]
        public string MetaDescription { get; set; }


        [Display(Name = "Ngày đăng")]
        public DateTime? CreatedAt { get; set; }

        [Display(Name = "Trạng Thái")]
        public bool Status { get; set; }


        public virtual Blog Blogs { get; set; }
    }
}