using CatPhoneShop.Areas.Admin.Models.BusinessModel;
using CatPhoneShop.Areas.Admin.Models.DataModel;
using CatPhoneShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatPhoneShop.Controllers
{
    public class checkoutsController : Controller
    {
        // GET: checkouts
        private ConnectionDBContext db = new ConnectionDBContext();

        public ActionResult Index()
        {
            var session_cart = Session["CartItem"];
            var list = new List<CartModel>();
            if (session_cart != null)
            {
                list = (List<CartModel>)session_cart;
            }


            var payment = db.Payments.Where(x => x.Status == true).ToList().Distinct();

            ViewBag.payment = payment;

            return View(list);
        }

        public ActionResult AddOrderDetails(string customerName,string Email,string Phone,string Address,int PaymentID,decimal TotalProduct)
        {
            var order = new Order();

            order.CreatedAt = DateTime.Now;
            order.customerName = customerName;
            order.Email = Email;
            order.Phone = Phone;
            order.Address = Address;
            order.PaymentID = PaymentID;
            order.Status = 1;
            order.TotalMoney = TotalProduct;
            order.ViewStatus = false;

            db.Orders.Add(order);

            var session_cart = (List<CartModel>)Session["CartItem"];
            foreach (var item in session_cart)
            {
                var orderdl = new OrderDetail();
                orderdl.ProductID = item.Product.ProductID;
                orderdl.orderID = order.orderID;
                orderdl.Price = 0;
                orderdl.Quanlity = item.Quanlity;
                orderdl.TotalProduct = 0;
                db.OrderDetails.Add(orderdl);
                orderdl.Status = true;
            }

            db.SaveChanges();
            Session["CartItem"] = null;
            return RedirectToAction("thankyou");
        }

        public ActionResult thankyou()
        {
            return View();
        }
    }
}