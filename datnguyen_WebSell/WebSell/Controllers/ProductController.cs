using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSell.Context;
using WebSell.Models;

namespace WebSell.Controllers
{
    public class ProductController : Controller
    {
        WebSellEntities objWebSellEntities = new WebSellEntities();
        // GET: Product
        public ActionResult Detail(int Id)
        {
            var objProduct = objWebSellEntities.Products.Where(n => n.Id == Id).FirstOrDefault();
            return View(objProduct);
        }
    }
}