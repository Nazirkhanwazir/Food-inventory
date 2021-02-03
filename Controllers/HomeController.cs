using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodInventory.Models;

namespace FoodInventory.Controllers {
    public class HomeController : Controller {
        POS_InventoryEntities db = new POS_InventoryEntities();

        [HttpGet]
        public ActionResult Index() {
            if(Session["U_Name"] != null) {
                return View();
            }

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult Index(int? amount) {
            List<Salespersondetail> sps = db.Salespersondetails.ToList();

            sps.ForEach(x => {
                x.Loginammount = amount;
            });

            db.SaveChanges();

            return View();
        }

        public ActionResult MakeSales() {
            if(Session["U_Name"] != null) {
                return View();
            }

            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public ActionResult AddNewItem() {
            if(Session["U_Name"] != null) {
                return View();
            }

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult AddNewItem(Item item) {
            try {
                db.Items.Add(item);
                db.SaveChanges();
                ViewBag.Success = "Item has been added.";
            } catch(Exception ex) {
                ViewBag.Error = "Failed to save item.";
            }

            return View();
        }

        [HttpGet]
        public ActionResult AddStock() {
            if(Session["U_Name"] != null) {
                return View();
            }

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult AddStock(Item newItem) {
            try {
                Item item = db.Items.Find(newItem.Item_No);

                if(item == null) {
                    ViewBag.Error = "This item does not exist.";
                } else {
                    db.Items.Attach(item);

                    item.Quantity = newItem.Quantity;

                    db.Entry(item).Property(i => i.Quantity).IsModified = true;
                    db.SaveChanges();
                    ViewBag.Success = "Updated successfully.";
                }
            } catch(Exception ex) {
                ViewBag.Error = "Failed to Update Stock.";
            }

            return View();
        }

        [HttpPost]
        public ActionResult SearchItem(int? itemNo) {
            Item item = db.Items.FirstOrDefault(i => i.Item_No == itemNo);

            if(item != null) {
                return View("AddStock", item);
            }

            ViewBag.SearchError = "Item with item no '" + itemNo + "' not found.";
            return View("AddStock");
        } 

        public ActionResult SalesReport() {
            if(Session["U_Name"] != null) {
                return View();
            }

            return RedirectToAction("Index", "Login");
        }

        public ActionResult StockReport() {
            if(Session["U_Name"] != null) {
                return View();
            }

            return RedirectToAction("Index", "Login");
        }       
    }
}