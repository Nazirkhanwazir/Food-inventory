﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FoodInventory.Models;

namespace FoodInventory.Controllers
{
    public class WebApiController : ApiController
    {
        POS_InventoryEntities db = new POS_InventoryEntities();
        
        [HttpPost]
        public HttpResponseMessage SearchItem(int itemNo) {
            try {
                Item item = db.Items.First(i => i.Item_No == itemNo);

                return Request.CreateResponse(HttpStatusCode.OK, item);
            } catch(Exception ex) {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage MakeSale(List<Sales_Detail> saleDetail) {
            try {
                var lastSale = db.Sales_Detail.OrderByDescending(s => s.id).FirstOrDefault();
                int id = 1;

                if(lastSale != null) {
                    id = lastSale.id + 1;
                }

                saleDetail.ForEach(s => {
                    s.Sale_Id = id;

                    Item i = db.Items.Where(x => x.Item_No == s.Item_No).FirstOrDefault();
                    db.Items.Attach(i);
                    i.Quantity = i.Quantity - s.Item_Quantity;
                    db.Entry(i).Property(x => x.Quantity).IsModified = true;

                    db.Sales_Detail.Add(s);
                    db.SaveChanges();
                });


                return Request.CreateResponse(HttpStatusCode.OK, "Sale done.");
            } catch(Exception ex) {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
