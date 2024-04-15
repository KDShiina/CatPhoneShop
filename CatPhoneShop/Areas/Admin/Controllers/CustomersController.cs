using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CatPhoneShop.Areas.Admin.Models.BusinessModel;
using CatPhoneShop.Areas.Admin.Models.DataModel;
using PagedList;

namespace CatPhoneShop.Areas.Admin.Controllers
{
    [AuthorizeBusiness]
    public class CustomersController : Controller
    {
        private ConnectionDBContext db = new ConnectionDBContext();

        // GET: Admin/Customers
        public ActionResult Index(int? page,string q)
        {
            var count_customer = (from cus in db.Customers select cus.customerName).Count();

            ViewBag.count_customer = count_customer;

            var model = from p in db.Customers orderby p.CreatedAt descending select p;
            int pagesize = 15;
            int pageNumber = (page ?? 1);

            if (!string.IsNullOrEmpty(q))
            {
                model=model.Where(x=>x.customerName.Contains(q) || x.Email.Contains(q)).OrderByDescending(x=>x.CreatedAt);
            }

            ViewBag.keyword_search = q;

            return View(model.ToPagedList(pageNumber,pagesize));
        }

        // GET: Admin/Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "customerID,customerName,Email,Password,Address,Phone,CreatedAt,Status")] Customer customer, bool status_cus)
        {
            customer.CreatedAt = DateTime.Now;
            bool tus;
            if (status_cus == true)
            {
                tus = true;
            }
            else
            {
                tus = false;
            }

            if (ModelState.IsValid)
            {
                customer.Status = tus;
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Admin/Customer/Edit/{id}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Admin/Customer/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var existingCustomer = db.Customers.Find(customer.customerID);

                if (existingCustomer != null)
                {
                    existingCustomer.customerName = customer.customerName;
                    existingCustomer.Email = customer.Email;
                    existingCustomer.Password = customer.Password;
                    existingCustomer.Address = customer.Address;
                    existingCustomer.Phone = customer.Phone;
                    existingCustomer.CreatedAt = customer.CreatedAt;
                    existingCustomer.Status = customer.Status;

                    db.SaveChanges();
                    return RedirectToAction("Index", "Customers");
                }
                else
                {
                    ModelState.AddModelError("", "Không tìm thấy khách hàng để cập nhật.");
                }
            }
            return View(customer);
        }
        // GET: Admin/Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Admin/Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
