using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatPhoneShop.Areas.Admin.Models.DataModel
{
    [Table("Contact")]
    public class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContactID { get; set; }

        [Display(Name = "Họ Và Tên")]
        [Required(ErrorMessage = "Họ Tên Không Được Bỏ Trống")]
        [MaxLength(250, ErrorMessage = "Họ Tên Không Được Vượt Quá 250 Kí Tự")]
        public string FullName { get; set; }



        [Display(Name = "Email")]
        [Column(TypeName = "varchar")]
        [Required(ErrorMessage = "Email Không Được Bỏ Trống")]
        [MaxLength(250, ErrorMessage = "Email Không Được Vượt Quá 250 Kí Tự")]
        public string Email { get; set; }



        [Display(Name = "Số Điện Thoại")]
        [Column(TypeName = "varchar")]
        [Required(ErrorMessage = "Số Điện Thoại Không Được Bỏ Trống")]
        [MaxLength(11, ErrorMessage = "Số Điện Thoại Vượt Quá 11 Kí Tự")]
        public string Phone { get; set; }


        [Display(Name = "Nội dung")]
        [Column(TypeName = "ntext")]
        [Required(ErrorMessage = "Mời Bạn Nhập Nội Dung")]
        public string Content { get; set; }



        public bool ViewStatus { get; set; }

        [Display(Name = "Ngày đăng")]
        public DateTime? CreatedAt { get; set; }

    }
}