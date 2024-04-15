using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatPhoneShop.Areas.Admin.Models.DataModel
{
    [Table("Order")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int orderID { get; set; }


        [Display(Name = "Tên Khách Hàng")]
        [Required(ErrorMessage = "Họ Tên Không Được Bỏ Trống")]
        [MaxLength(50, ErrorMessage = "Họ Tên Không Được Vượt Quá 50 Kí Tự")]
        public string customerName { get; set; }

        [Required(ErrorMessage = "Địa Chỉ Không Được Bỏ Trống")]
        [Display(Name = "Địa Chỉ")]
        [MaxLength(250, ErrorMessage = "Địa Chỉ Không Được Vượt Quá 250 Kí Tự")]
        public string Address { get; set; }



        [Required(ErrorMessage = "Số Điện Thoại Không Được Bỏ Trống")]
        [Display(Name = "Số Điện Thoại")]
        [Column(TypeName = "varchar")]
        [MaxLength(11, ErrorMessage = "Số Điện Thoại Không Hợp Lệ")]
        public string Phone { get; set; }


        [Display(Name = "Email")]
        [Column(TypeName = "varchar")]
        [Required(ErrorMessage = "Email Không Được Bỏ Trống")]
        [MaxLength(50, ErrorMessage = "Email Không Được Quá 50 Kí Tự")]
        public string Email { get; set; }

        [ForeignKey("Payments")]
        [Display(Name ="Phương thức thanh toán")]
        public int PaymentID { get; set; }


        [DisplayFormat(DataFormatString = "{0:0,0₫}")]
        [Display(Name ="Tổng Tiền")]
        public decimal TotalMoney { get; set; }


        [Display(Name = "Ngày đặt hàng")]
        public DateTime? CreatedAt { get; set; }


        public bool ViewStatus { get; set; }

        [Display(Name = "Trạng Thái Đơn Hàng")]
        public long Status { get; set; }



        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual Payment Payments { get; set; }
    }
}