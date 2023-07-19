using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSell.Context;
using WebSell.Models;

namespace WebSell.Controllers
{
    public class PaymentController : Controller
    {
        WebSellEntities objWebSellEntities = new WebSellEntities();
        // GET: Payment
        public ActionResult Index()
        {
            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var listCart = (List<CartModel>) Session["cart"];
                Order objOrder = new Order();
                objOrder.Name = "DonHang-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                objOrder.UserId = int.Parse(Session["idUer"].ToString());
                objOrder.CreatedOnUtc = DateTime.Now;
                objOrder.Status = 1;
                objWebSellEntities.Orders.Add(objOrder);
                objWebSellEntities.SaveChanges();


                int intOrderId = objOrder.Id;
                List<OrderDetail> listOrderDetail = new List<OrderDetail>();

                foreach(var item in listCart){
                    OrderDetail obj = new OrderDetail();
                    obj.Quantity = item.Quantity;
                    obj.OrderId = intOrderId;
                    obj.ProductId = item.Product.Id;
                    listOrderDetail.Add(obj);
                }
                objWebSellEntities.OrderDetails.AddRange(listOrderDetail);
                objWebSellEntities.SaveChanges();
            }
            return View();
        }
    }
}