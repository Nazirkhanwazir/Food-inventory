using System;
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
    }
}
