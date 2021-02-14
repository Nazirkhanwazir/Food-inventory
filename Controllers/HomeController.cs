﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodInventory.Models;

namespace FoodInventory.Controllers
{
    public class HomeController : Controller
    {
        POS_InventoryEntities1 db = new POS_InventoryEntities1();

        [HttpGet]
        public ActionResult Index()
        {
            if (Session["U_Name"] != null)
            {
                return View();
            }

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult Index(int? amount)
        { 
            
            List <Salespersondetail> sps = db.Salespersondetails.ToList();

            sps.ForEach(x =>
            {
                x.Loginammount = amount;
            });
           
            db.SaveChanges();

            return View();
        }

        public ActionResult MakeSales()
        {
            if (Session["U_Name"] != null)
            {
                return View();
            }

            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public ActionResult AddNewItem()
        {
            if (Session["U_Name"] != null)
            {
                return View();
            }

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult AddNewItem(Item item)
        {
            try
            {
                db.Items.Add(item);
                db.SaveChanges();
                ViewBag.Success = "Item has been added.";
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Failed to save item.";
            }

            return View();
        }

        [HttpGet]
        public ActionResult AddStock()
        {
            if (Session["U_Name"] != null)
            {
                return View();
            }

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult AddStock(Item newItem)
        {
            try
            {
                Item item = db.Items.Find(newItem.Item_No);

                if (item == null)
                {
                    ViewBag.Error = "This item does not exist.";
                }
                else
                {
                    db.Items.Attach(item);

                    item.Quantity += newItem.Quantity;

                    db.Entry(item).Property(i => i.Quantity).IsModified = true;
                    db.SaveChanges();

                    ViewBag.Success = "Updated successfully.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Failed to Update Stock.";
            }

            return View();
        }

        [HttpPost]
        public ActionResult SearchItem(int? itemNo)
        {
            Item item = db.Items.FirstOrDefault(i => i.Item_No == itemNo);

            if (item != null)
            {
                return View("AddStock", item);
            }

            ViewBag.SearchError = "Item with item no '" + itemNo + "' not found.";
            return View("AddStock");
        }
        [HttpPost]
        public ActionResult SearchItemUpdate(int? itemNo)
        {
            Item item = db.Items.FirstOrDefault(i => i.Item_No == itemNo);

            if (item != null)
            {
                return View("EditItem", item);
            }

            ViewBag.SearchError = "Item with item no '" + itemNo + "' not found.";
            return View("EditItem");
        }

        [HttpGet]
        public ActionResult SalesReport()
        {
            if (Session["U_Name"] != null)
            {
                return View();
            }

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult SalesReport(DateTime? from, DateTime? to, string code)
        {
            try
            {
                //List<Sales_Detail> sales = db.Sales_Detail.ToList().Where(x => x.Item_No.ToString().Equals(code) && from >= x.Sale_Date && to <= x.Sale_Date ).ToList();
                List<Sales_Detail> sales = db.Sales_Detail.ToList().Where(x => x.Item_No.ToString().Equals(code)).ToList();

                if (sales.Count < 1)
                {
                    ViewBag.Error = "No Sale Records.";
                }
                else
                {
                    return View(sales);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Fail to get Sales Report.";
            }

            return View();
        }

        [HttpGet]
        public ActionResult StockReport()
        {
            if (Session["U_Name"] != null)
            {
                return View();
            }

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult StockReport(string from, string to, string code)
        {
            try
            {
                //List<Sales_Detail> sales = db.Sales_Detail.ToList().Where(x => x.Item_No.ToString().Equals(code) && from >= x.Sale_Date && to <= x.Sale_Date).ToList();
                List<Stock_Detail> stocks = db.Stock_Detail.ToList().Where(x => x.Itemno.ToString().Equals(code)).ToList();

                if (stocks.Count < 1)
                {
                    ViewBag.Error = "No Stock Records.";
                }
                else
                {
                    return View(stocks);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Fail to get Stock Report.";
            }

            return View();
        }

        [HttpGet]
        public ActionResult EditItem()
        {
            if (Session["U_Name"] != null)
            {
                return View();
            }

            return RedirectToAction("Index", "Login");
        }
        [HttpPost]
        public ActionResult EditItem(Item newItem)
        {
            try
            {
                Item item = db.Items.Find(newItem.Item_No);

                if (item == null)
                {
                    ViewBag.Error = "This item does not exist.";
                }
                else
                {
                    db.Items.Attach(item);

                    item.Quantity += newItem.Quantity;
                    item.Item_Name = newItem.Item_Name;
                    item.Item_No = newItem.Item_No;
                    item.Brand = newItem.Brand;
                    item.Category = newItem.Category;
                    item.Cost_Price = newItem.Cost_Price;
                    item.Retail_Price = newItem.Retail_Price;
                    item.Threshhold_Quantity = newItem.Threshhold_Quantity;

                    db.Entry(item).Property(i => i.Quantity).IsModified = true;
                    db.Entry(item).Property(i => i.Item_No).IsModified = true;
                    db.Entry(item).Property(i => i.Item_Name).IsModified = true;
                    db.Entry(item).Property(i => i.Brand).IsModified = true;
                    db.Entry(item).Property(i => i.Category).IsModified = true;
                    db.Entry(item).Property(i => i.Retail_Price).IsModified = true;
                    db.Entry(item).Property(i => i.Cost_Price).IsModified = true;
                    db.Entry(item).Property(i => i.Threshhold_Quantity).IsModified = true;

                    db.SaveChanges();

                    ViewBag.Success = "Updated successfully.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Failed to Update Stock.";
            }

            return View();
        }
        [HttpGet]
        public ActionResult SalePersonDetails()
        {
            if (Session["U_Name"] != null)
            {
                return View();
            }

            return RedirectToAction("Index", "Login");
        }
        [HttpPost]
        public ActionResult SalePersonDetails(Salespersondetail newItem)
        {
            try
            {
                Salespersondetail item = db.Salespersondetails.Find(newItem.Salesperson);

                
            }
            catch (Exception ex)
            {
                ViewBag.salepersonfound = "sale person found.";
            }

            return View();
        }
        [HttpPost]
        public ActionResult SalePersonSearch(string itemNo)
        {
            Salespersondetail item = db.Salespersondetails.FirstOrDefault(i => i.Salesperson == itemNo);

            if (item != null)
            {
                return View("SalePersonDetails", item);
            }

            ViewBag.SearchError = "Sale Person '" + itemNo + "' not found.";
            return View("SalePersonDetails");
        }
    }

   


}