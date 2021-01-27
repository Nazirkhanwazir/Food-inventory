using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodInventory.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            return View();
        }

        public ActionResult MakeSales() {
            return View();
        }

        public ActionResult AddNewItem() {
            return View();
        }

        public ActionResult AddStock() {
            return View();
        }

        public ActionResult SalesReport() {
            return View();
        }

        public ActionResult StockReport() {
            return View();
        }       
    }
}