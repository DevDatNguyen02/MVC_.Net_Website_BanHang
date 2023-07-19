using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSell.Context;

namespace WebSell.Models
{
    public class CartModel
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}