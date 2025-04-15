// checkoutsController.cs
using CatPhoneShop.Areas.Admin.Models.BusinessModel;
using CatPhoneShop.Areas.Admin.Models.DataModel;
using CatPhoneShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Diagnostics;

namespace CatPhoneShop.Controllers
{
    public class checkoutsController : Controller
    {
        private ConnectionDBContext db = new ConnectionDBContext();

        // GET: checkouts
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

        [HttpPost]
        public ActionResult AddOrderDetails(string customerName, string Email, string Phone, string Address, int PaymentID, decimal TotalProduct)
        {
            try
            {
                Debug.WriteLine($"Start processing order for {customerName}, PaymentID: {PaymentID}, TotalProduct: {TotalProduct}");

                // Kiểm tra PaymentID và xử lý thanh toán
                if (PaymentID == 2)  // Nếu PaymentID là 2, nghĩa là chọn VNPAY
                {
                    Debug.WriteLine("Payment method is VNPAY. Creating VNPAY payment URL.");

                    // Lưu thông tin đơn hàng tạm thời vào Session để sau khi thanh toán có thể truy xuất
                    Session["PendingOrder"] = new
                    {
                        CustomerName = customerName,
                        Email = Email,
                        Phone = Phone,
                        Address = Address,
                        TotalProduct = TotalProduct
                    };

                    // Lưu giỏ hàng vào session để sử dụng sau khi thanh toán
                    Session["PendingCart"] = Session["CartItem"];

                    // Tiến hành tạo URL thanh toán VNPAY và chuyển hướng người dùng đến cổng thanh toán
                    string paymentUrl = GetVnpayPaymentUrl(customerName, Email, Phone, Address, TotalProduct);
                    Debug.WriteLine($"Redirecting to VNPAY payment URL: {paymentUrl}");

                    // Chuyển hướng người dùng đến cổng thanh toán VNPAY
                    return Redirect(paymentUrl);
                }

                // Nếu không phải VNPAY, tạo đơn hàng như bình thường
                var order = new Order
                {
                    CreatedAt = DateTime.Now,
                    customerName = customerName,
                    Email = Email,
                    Phone = Phone,
                    Address = Address,
                    PaymentID = PaymentID,
                    Status = 1,
                    TotalMoney = TotalProduct,
                    ViewStatus = false
                };

                db.Orders.Add(order);
                Debug.WriteLine($"Order created with OrderID: {order.orderID}");

                var session_cart = (List<CartModel>)Session["CartItem"];
                foreach (var item in session_cart)
                {
                    var orderDetail = new OrderDetail
                    {
                        ProductID = item.Product.ProductID,
                        orderID = order.orderID,
                        Price = (decimal)item.Product.Price,
                        Quanlity = item.Quanlity,
                        TotalProduct = (float)(item.Product.Price * item.Quanlity),
                        Status = true
                    };
                    db.OrderDetails.Add(orderDetail);

                    Debug.WriteLine($"OrderDetail added for ProductID: {item.Product.ProductID}, Quantity: {item.Quanlity}");
                }

                db.SaveChanges();
                Session["CartItem"] = null;
                Debug.WriteLine("Order and OrderDetails saved successfully.");

                return RedirectToAction("thankyou");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in AddOrderDetails: {ex.Message}");
                return View("Error");
            }
        }

        private string GetVnpayPaymentUrl(string customerName, string Email, string Phone, string Address, decimal TotalProduct)
        {
            try
            {
                Debug.WriteLine("Generating VNPAY payment URL.");

                var vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html"; // URL của VNPAY sandbox
                var vnp_ReturnUrl = "https://localhost:1234/checkouts/VnpayReturn"; // URL callback - Thay đổi port phù hợp với ứng dụng của bạn
                var vnp_TmnCode = "IJNGPBDG"; // Mã website tại VNPAY
                var vnp_HashSecret = "HT98LJRMBKI6ZOK40P62Z2RY45T50375"; // Chuỗi bí mật

                var vnpay = new VnPayLibrary();

                // Thêm thông tin đơn hàng
                vnpay.AddRequestData("vnp_Version", "2.1.0");
                vnpay.AddRequestData("vnp_Command", "pay");
                vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
                vnpay.AddRequestData("vnp_Amount", Convert.ToString(Convert.ToInt64(TotalProduct * 100))); // Nhân 100 vì VNPay yêu cầu số tiền x100

                // Thêm thông tin đơn hàng
                var orderId = DateTime.Now.Ticks.ToString();
                vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
                vnpay.AddRequestData("vnp_CurrCode", "VND");
                vnpay.AddRequestData("vnp_IpAddr", GetIpAddress());
                vnpay.AddRequestData("vnp_Locale", "vn");
                vnpay.AddRequestData("vnp_OrderInfo", $"Thanh toan don hang {orderId} - {customerName}");
                vnpay.AddRequestData("vnp_OrderType", "other");
                vnpay.AddRequestData("vnp_ReturnUrl", vnp_ReturnUrl);
                vnpay.AddRequestData("vnp_TxnRef", orderId); // Mã tham chiếu của giao dịch tại hệ thống của merchant

                // Tạo URL thanh toán
                string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
                Debug.WriteLine($"Generated VNPAY payment URL: {paymentUrl}");

                return paymentUrl;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in GetVnpayPaymentUrl: {ex.Message}");
                throw;
            }
        }

        private string GetIpAddress()
        {
            string ipAddress;
            try
            {
                ipAddress = HttpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                if (string.IsNullOrEmpty(ipAddress) || (ipAddress.ToLower() == "unknown") || ipAddress.Length > 45)
                    ipAddress = HttpContext.Request.ServerVariables["REMOTE_ADDR"];
            }
            catch (Exception ex)
            {
                ipAddress = "127.0.0.1";
                Debug.WriteLine($"Error getting IP Address: {ex.Message}");
            }

            return ipAddress;
        }

        // Xử lý kết quả trả về từ VNPAY
        public ActionResult VnpayReturn()
        {
            Debug.WriteLine("Handling VNPAY return callback.");
            var vnpayData = Request.QueryString;
            var vnp_HashSecret = "HT98LJRMBKI6ZOK40P62Z2RY45T50375"; // Chuỗi bí mật

            // Lấy dữ liệu đơn hàng từ Session
            if (Session["PendingOrder"] == null)
            {
                Debug.WriteLine("No pending order found in session.");
                TempData["ErrorMessage"] = "Không tìm thấy thông tin đơn hàng.";
                return RedirectToAction("Index");
            }

            dynamic pendingOrder = Session["PendingOrder"];

            try
            {
                var vnpay = new VnPayLibrary();
                var responseData = vnpay.GetFullResponseData(vnpayData, vnp_HashSecret);

                // Chuyển SortedList sang Dictionary để thực hiện validate
                Dictionary<string, string> vnpayResponseDict = new Dictionary<string, string>();
                foreach (var key in responseData.Keys)
                {
                    vnpayResponseDict.Add(key, responseData[key]);
                }

                // Kiểm tra chữ ký
                bool validSignature = vnpay.ValidateSignature(vnpayResponseDict, vnp_HashSecret);
                if (!validSignature)
                {
                    Debug.WriteLine("Invalid signature from VNPAY response.");
                    TempData["ErrorMessage"] = "Lỗi xác thực chữ ký từ VNPAY.";
                    return RedirectToAction("Index");
                }

                // Kiểm tra mã lỗi
                string vnp_ResponseCode = responseData["vnp_ResponseCode"];
                Debug.WriteLine($"VNPAY Response Code: {vnp_ResponseCode}");

                if (vnp_ResponseCode == "00") // Thanh toán thành công
                {
                    Debug.WriteLine("Payment successful. Creating order in database.");

                    // Tạo đơn hàng trong database
                    var order = new Order
                    {
                        CreatedAt = DateTime.Now,
                        customerName = pendingOrder.CustomerName,
                        Email = pendingOrder.Email,
                        Phone = pendingOrder.Phone,
                        Address = pendingOrder.Address,
                        PaymentID = 2, // VNPAY
                        Status = 1, // Đã thanh toán
                        TotalMoney = pendingOrder.TotalProduct,
                        ViewStatus = false
                    };

                    db.Orders.Add(order);
                    Debug.WriteLine($"Order created with OrderID: {order.orderID}");

                    // Thêm chi tiết đơn hàng
                    var session_cart = (List<CartModel>)Session["PendingCart"];
                    if (session_cart != null)
                    {
                        foreach (var item in session_cart)
                        {
                            var orderDetail = new OrderDetail
                            {
                                ProductID = item.Product.ProductID,
                                orderID = order.orderID,
                                Price = (decimal)item.Product.Price,
                                Quanlity = item.Quanlity,
                                TotalProduct = (float)(item.Product.Price * item.Quanlity),
                                Status = true
                            };
                            db.OrderDetails.Add(orderDetail);
                            Debug.WriteLine($"OrderDetail added for ProductID: {item.Product.ProductID}, Quantity: {item.Quanlity}");
                        }
                    }

                    db.SaveChanges();
                    Debug.WriteLine("Order and OrderDetails saved successfully.");

                    // Xóa session
                    Session["CartItem"] = null;
                    Session["PendingOrder"] = null;
                    Session["PendingCart"] = null;

                    return RedirectToAction("thankyou");
                }
                else // Thanh toán thất bại
                {
                    Debug.WriteLine($"Payment failed. Response code: {vnp_ResponseCode}");
                    TempData["ErrorMessage"] = "Thanh toán không thành công. Mã lỗi: " + vnp_ResponseCode;
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in VnpayReturn: {ex.Message}");
                TempData["ErrorMessage"] = "Đã xảy ra lỗi khi xử lý kết quả thanh toán.";
                return RedirectToAction("Index");
            }
        }

        public ActionResult thankyou()
        {
            return View();
        }
    }
}