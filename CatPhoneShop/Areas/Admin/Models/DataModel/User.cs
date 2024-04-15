using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CatPhoneShop.Areas.Admin.Models.DataModel
{
    [Table("User")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }


        [Display(Name = "Tên Truy Cập")]
        [Required(ErrorMessage = "Tên Truy Cập Không Được Bỏ Trống")]
        [MaxLength(50, ErrorMessage = "Tên Truy Cập Không Được Vượt Quá 50 Kí Tự")]
        [Column(TypeName = "varchar")]
        [Index(IsUnique =true)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Mật Khẩu Không Được Bỏ Trống")]
        [Display(Name = "Mật Khẩu")]
        [MaxLength(250, ErrorMessage = "Mật Khẩu Không Được Vượt Quá 250 Kí Tự")]
        [Column(TypeName = "varchar")]
        [DataType(DataType.Password)]
        public string Password { get; set; }




        [Required(ErrorMessage = "Họ Và Tên Không Được Bỏ Trống")]
        [Display(Name = "Họ Và Tên")]
        public string Fullname { get; set; }


        [Display(Name = "Hình Ảnh")]
        [MaxLength(250, ErrorMessage = "Tên Hình Ảnh Không Được Vượt Quá 250 Kí Tự")]
        public string Image { get; set; }

        [Display(Name = "Email")]
        [Column(TypeName = "varchar")]
        [Required(ErrorMessage = "Email Không Được Bỏ Trống")]
        [MaxLength(50, ErrorMessage = "Email Không Được Quá 50 Kí Tự")]
        public string Email { get; set; }



        [Required(ErrorMessage = "Địa Chỉ Không Được Bỏ Trống")]
        [Display(Name = "Địa Chỉ")]
        [MaxLength(150, ErrorMessage = "Địa Chỉ Không Được Vượt Quá 150 Kí Tự")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Số Điện Thoại Không Được Bỏ Trống")]
        [Display(Name = "Số Điện Thoại")]
        [MaxLength(20, ErrorMessage = "Số Điện Thoại Không Được Vượt Quá 20 Kí Tự")]
        public string Phone { get; set; }

        [Display(Name = "Ngày Cập Nhật")]
        public DateTime? CreatedAt { get; set; } 

        
        [Display(Name ="Trạng Thái")]
        public bool Status { get; set; }



        public virtual  ICollection<UserGrantPermission> UserGrantPermissions { get; set; }


    }
}