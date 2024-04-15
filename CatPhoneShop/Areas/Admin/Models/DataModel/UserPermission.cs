using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CatPhoneShop.Areas.Admin.Models.DataModel
{
    [Table("UserPermission")]
    public class UserPermission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PermissionID { get; set; }


        [Required(ErrorMessage ="Vui Lòng Nhập Tên Quyền")]
        [Column(TypeName="varchar")]
        [MaxLength(100)]
        [Display(Name ="Tên Quyền")]
        public string PermissionName { get; set; }

        [MaxLength(250)]
        [Display(Name = "Mô Tả Quyền")]
        [Required(ErrorMessage = "Vui Lòng Nhập Mô tả Cho Quyền")]
        public string PermissionDescription { get; set; }


        [Display(Name ="Mã Nghiệp Vụ")]
        [MaxLength(50)]
        [ForeignKey("UserBusinesses")]
        [Column(TypeName ="varchar")]
        public string BusinessID { get; set; }

        public virtual UserBusiness UserBusinesses { get; set; }
        public virtual ICollection<UserGrantPermission> UserGrantPermissions { get; set; }
    }
}