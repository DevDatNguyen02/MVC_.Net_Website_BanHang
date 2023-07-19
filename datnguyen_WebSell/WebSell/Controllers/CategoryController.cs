using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSell.Context;


namespace WebSell.Controllers
{
    public class CategoryController : Controller
    {
        WebSellEntities objWebSellEntities = new WebSellEntities();
        // GET: Category
        public ActionResult Index()
        {
            var listCategory = objWebSellEntities.Categories.ToList();
            return View(listCategory);
        }
        public ActionResult ProductCategory(int Id)
        {
            var listProduct = objWebSellEntities.Products.Where(n => n.CategoryId == Id).ToList();
            return View(listProduct);
        }
    }
}