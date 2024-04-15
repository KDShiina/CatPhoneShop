using CatPhoneShop.Areas.Admin.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CatPhoneShop.Areas.Admin.Models.BusinessModel
{
    public class ConnectionDBContext:DbContext
    {
        internal object Administrator;

        public ConnectionDBContext():base("name=ConnectionStringDBContext")
        {

        }
        public DbSet<User> User { get; set; }
        public DbSet<UserBusiness> UserBusiness { get; set; }
        public DbSet<UserPermission> UserPermission { get; set; }
        public DbSet<UserGrantPermission> UserGrantPermission { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Slide> Slides { get; set; }
        public DbSet<ImageProduct> ImageProduct { get; set; }

        public DbSet<Home> Homes { get; set; }

        public System.Data.Entity.DbSet<CatPhoneShop.Areas.Admin.Models.DataModel.MenuNavigation> MenuNavigations { get; set; }

        public System.Data.Entity.DbSet<CatPhoneShop.Areas.Admin.Models.DataModel.Configure> Configures { get; set; }
    }
}