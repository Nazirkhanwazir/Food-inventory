﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodInventory.Models;

namespace FoodInventory.Controllers
{
    public class LoginController : Controller
    {
        POS_InventoryEntities1 db = new POS_InventoryEntities1();

        [HttpGet]
        public ActionResult Index()
        {
            if(Session["U_Name"] != null) {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Index(string username, string password) {
            Login login = db.Logins.Where(u => u.U_Name.Equals(username) && u.Password.ToString() == password).FirstOrDefault();

            if(login != null) {
                Session["U_Name"] = login.U_Name.ToString();
                Session["Password"] = login.Password.ToString();
                Session["Role"] = login.Role.ToString();

                if(login.Role.ToString().Equals("SalesPerson")) {
                    Salespersondetail sp = db.Salespersondetails.Where(x => x.Salesperson.ToLower().Equals(login.U_Name.ToLower())).FirstOrDefault();
                    
                    if(sp != null) {
                        Session["Totalsale"] = sp.Totalsale.ToString();
                        Session["Loginammount"] = sp.Loginammount.ToString();
                        Session["Logoutammount"] = sp.Logoutammount.ToString();
                    }
                }                

                return RedirectToAction("Index", "Home");

            }

            ViewBag.Error = "Invalid login credentials.";
            return View();
        }

        public ActionResult Logout() {
            if (Session["Role"].ToString().Equals("SalesPerson"))
            {
                var str = Session["U_Name"].ToString();
                Salespersondetail sp = db.Salespersondetails.FirstOrDefault(x => x.Salesperson.ToLower() == str.ToLower());
                sp.Logoutammount = sp.Loginammount;
                db.SaveChanges();
            }
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}