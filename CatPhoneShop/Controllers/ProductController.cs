using CatPhoneShop.Areas.Admin.Models.BusinessModel;
using CatPhoneShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatPhoneShop.Controllers
{
    public class ProductController : Controller
    {
        private ConnectionDBContext db = new ConnectionDBContext();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductHome(string Alias,int ProductID,int CategoryID)
        {
            var model = from p in db.Products
                        where p.Alias == Alias && p.Status == true
                        select new ProductModel {
                            ProductID=p.ProductID,
                            ProductName=p.ProductName,
                            Price=p.Price,
                            PriceSale=p.PriceSale,
                            Alias=p.Alias,
                            CategoryID=p.CategoryID,
                            MetaDescription=p.MetaDescription,
                            MetaKeyword=p.MetaKeyword,
                            DescriptShort=p.DescriptShort,
                            Author=p.Author,
                            Images=p.Images,
                            Content=p.Content,
                            MetaTitle=p.MetaTitle
                            

                        };




            var image_more = from img in db.ImageProduct
                        where img.ProductID == ProductID
                        select new ProductModel {
                            FileImages=img.FileImages
                        };
            ViewBag.image_more = image_more;



            var product_related = from p in db.Products
                                  join c in db.Categorys
                                  on p.CategoryID equals c.CategoryID
                                  where p.CategoryID == CategoryID orderby Guid.NewGuid()
                                  select new ProductModel {
                                      ProductID = p.ProductID,
                                      ProductName = p.ProductName,
                                      Price = p.Price,
                                      PriceSale = p.PriceSale,
                                      Alias = p.Alias,
                                      CategoryID = p.CategoryID,
                                      MetaDescription = p.MetaDescription,
                                      MetaKeyword = p.MetaKeyword,
                                      DescriptShort = p.DescriptShort,
                                      Author = p.Author,
                                      Images = p.Images,
                                      Content = p.Content,
                                      MetaTitle = p.MetaTitle

                                  };

            ViewBag.product_related = product_related.ToList();


            var getbread = from p in db.Categorys
                           where p.CategoryID == CategoryID
                           select new CategoryModel {
                               CategoryID=p.CategoryID,
                               CategoryName=p.CategoryName
                           };

            ViewBag.getbread = getbread.ToList();


            return View(model.ToList());
        }
    }
}