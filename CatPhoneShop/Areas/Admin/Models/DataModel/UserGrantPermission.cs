using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CatPhoneShop.Areas.Admin.Models.DataModel
{
    [Table("UserGrantPermission")]
    public class UserGrantPermission
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("UserPermission")]
        [Required]
        [Display(Name = "Mã Quyền")]
        public int PermissionID { get; set; }


        [Key]
        [Column(Order = 2)]
        [ForeignKey("User")]
        [Required]
        [Display(Name = "Mã Quyền")]
        public int UserID { get; set; }


        [Display(Name = "Mô tả")]
        [MaxLength(250)]
        public string Description { get; set; }


        public virtual UserPermission UserPermission { get; set; }
        public virtual User User { get; set; }
    }
}