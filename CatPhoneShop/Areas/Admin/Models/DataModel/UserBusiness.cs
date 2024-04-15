using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatPhoneShop.Areas.Admin.Models.DataModel
{
    [Table("UserBusiness")]
    public class UserBusiness
    {
        [Key]
        [Column(TypeName ="varchar")]
        [MaxLength(50)]
        [Display(Name ="Mã Control")]
        public string BusinessID { get; set; }



        [Required(ErrorMessage = "Vui Lòng Nhập Tên Mô tả Control")]
        [MaxLength(150)]
        [Display(Name = "Tên Mô tả Control")]
        public string BusinessName { get; set; }



        public virtual ICollection<UserPermission> UserPermissions { get; set; }

    }
}