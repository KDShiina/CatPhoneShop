using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using CatPhoneShop.Areas.Admin.Models.DataModel;

namespace CatPhoneShop.Areas.Admin.Models.BusinessModel
{
    public class InitializerConnectionDB:DropCreateDatabaseIfModelChanges<ConnectionDBContext>
    {
        protected override void Seed(ConnectionDBContext context)
        {
            Md5Encode md5 = new Md5Encode();
            var admin = new User() {
                Username = "Thang4597",
                Phone = "0976016104",
                Email = "thangpicker@gmail.com",
                Status = true,
                Image= "/Upload/Default/man-avatar.jpg",
                Password = md5.EncodeMd5Encrypt("123456"),
                Address="Ha Noi",
                Fullname="Cat Phone"
            };
            context.User.Add(admin);



            var user01 = new User()
            {
                Username = "Admin",
                Phone = "0976016104",
                Email = "thang9xvp@gmail.com",
                Status = true,
                Image = "/Upload/Default/man-avatar.jpg",
                Password = md5.EncodeMd5Encrypt("1234567"),
                Address = "Ha Noi",
                Fullname = "Admin"
            };

            context.User.Add(user01);




            context.SaveChanges();
        }
    }
}