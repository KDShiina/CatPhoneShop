using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace CatPhoneShop.Areas.Admin.Models.BusinessModel
{
    public class Md5Encode
    {
        public string EncodeMd5Encrypt(string data)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] b = System.Text.Encoding.UTF8.GetBytes(data);
            b = md5.ComputeHash(b);

            StringBuilder s = new StringBuilder();

            foreach(byte p in b)
            {
                s.Append(p.ToString("x").ToLower());
            }
            return s.ToString();
        }
    }
}