using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSell.Context;
using PagedList;
using PagedList.Mvc;

namespace WebSell.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        WebSellEntities objWebSellEntities = new WebSellEntities();
        // GET: Admin/Product
        public ActionResult Index(string SearchString, string currentFilter, int? page)
        {
            var listProduct = new List<Product>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                listProduct = objWebSellEntities.Products.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                listProduct = objWebSellEntities.Products.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 4;
            int pageNumber = (page ?? 1);

            listProduct = listProduct.OrderByDescending(n => n.Id).ToList();
            return View(listProduct.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            var listProduct = objWebSellEntities.Products.ToList();
            return View(listProduct);
        }
        [HttpPost]
        public ActionResult Create(Product objProduct)
        {
            try
            {
                if (objProduct.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                    string extention = Path.GetExtension(objProduct.ImageUpload.FileName);
                    fileName = fileName + extention + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss"));
                    objProduct.Avatar = fileName;
                    objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
                }
                objWebSellEntities.Products.Add(objProduct);
                objWebSellEntities.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Details(int Id)
        {
            var objProduct = objWebSellEntities.Products.Where(n => n.Id == Id).FirstOrDefault();
            return View(objProduct);
        }

        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var objProduct = objWebSellEntities.Products.Where(n => n.Id == Id).FirstOrDefault();
            return View(objProduct);
        }

        [HttpPost]
        public ActionResult Delete(Product objProducts)
        {
            var objProduct = objWebSellEntities.Products.Where(n => n.Id == objProducts.Id).FirstOrDefault();
            objWebSellEntities.Products.Remove(objProduct);
            objWebSellEntities.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var objProduct = objWebSellEntities.Products.Where(n => n.Id == Id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpPost]
        public ActionResult Edit(Product objProduct)
        {
            if (objProduct.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                string extention = Path.GetExtension(objProduct.ImageUpload.FileName);
                fileName = fileName + extention + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss"));
                objProduct.Avatar = fileName;
                objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
            }
            objWebSellEntities.Entry(objProduct).State = EntityState.Modified;
            objWebSellEntities.SaveChanges();
            return View(objProduct);
        }

    }
}