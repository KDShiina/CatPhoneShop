using CatPhoneShop.Areas.Admin.Models.DataModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

public class VnpayTransaction
{
    [Key]
    public int Id { get; set; }
    public int OrderID { get; set; }
    public string VnpayTransactionID { get; set; }
    public decimal Amount { get; set; }
    public string BankCode { get; set; }
    public string BankTranNo { get; set; }
    public string CardType { get; set; }
    public string PayDate { get; set; }
    public string OrderInfo { get; set; }
    public string TransactionStatus { get; set; }
    public DateTime CreatedDate { get; set; }

    [ForeignKey("OrderID")]
    public virtual Order Order { get; set; }
}