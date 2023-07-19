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
    public class BrandController : Controller
    {
        WebSellEntities objWebSellEntities = new WebSellEntities();
        // GET: Admin/Product
        public ActionResult Index(string SearchString, string currentFilter, int? page)
        {
            var listBrand = new List<Brand>();
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
                listBrand = objWebSellEntities.Brands.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                listBrand = objWebSellEntities.Brands.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 4;
            int pageNumber = (page ?? 1);

            listBrand = listBrand.OrderByDescending(n => n.Id).ToList();
            return View(listBrand.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            var listBrand = objWebSellEntities.Brands.ToList();
            return View(listBrand);
        }
        [HttpPost]
        public ActionResult Create(Brand objBrand)
        {
            try
            {
                if (objBrand.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objBrand.ImageUpload.FileName);
                    string extention = Path.GetExtension(objBrand.ImageUpload.FileName);
                    fileName = fileName + extention + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss"));
                    objBrand.Avatar = fileName;
                    objBrand.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
                }
                objWebSellEntities.Brands.Add(objBrand);
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
            var objBrand = objWebSellEntities.Brands.Where(n => n.Id == Id).FirstOrDefault();
            return View(objBrand);
        }

        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var objBrand = objWebSellEntities.Brands.Where(n => n.Id == Id).FirstOrDefault();
            return View(objBrand);
        }

        [HttpPost]
        public ActionResult Delete(Product objBrands)
        {
            var objBrand = objWebSellEntities.Products.Where(n => n.Id == objBrands.Id).FirstOrDefault();
            objWebSellEntities.Products.Remove(objBrand);
            objWebSellEntities.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var objBrand = objWebSellEntities.Brands.Where(n => n.Id == Id).FirstOrDefault();
            return View(objBrand);
        }
        [HttpPost]
        public ActionResult Edit(Brand objBrand)
        {
            if (objBrand.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objBrand.ImageUpload.FileName);
                string extention = Path.GetExtension(objBrand.ImageUpload.FileName);
                fileName = fileName + extention + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss"));
                objBrand.Avatar = fileName;
                objBrand.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
            }
            objWebSellEntities.Entry(objBrand).State = EntityState.Modified;
            objWebSellEntities.SaveChanges();
            return View(objBrand);
        }

    }
}