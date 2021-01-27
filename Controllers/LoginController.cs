using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodInventory.Models;

namespace FoodInventory.Controllers
{
    public class LoginController : Controller
    {
        POS_InventoryEntities db = new POS_InventoryEntities();

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
            var login = db.Logins.Where(u => u.U_Name.Equals(username) && u.Password.ToString() == password).FirstOrDefault();

            if(login != null) {
                Session["U_Name"] = login.U_Name.ToString();
                Session["Password"] = login.Password.ToString();
                Session["Role"] = login.Role.ToString();

                return RedirectToAction("Index", "Home");

            }

            ViewBag.Error = "Invalid login credentials.";
            return View();
        }

        public ActionResult Logout() {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}