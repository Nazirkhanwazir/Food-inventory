//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FoodInventory.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Stock
    {
        public int Stock_Id { get; set; }
        public string Stock_Date { get; set; }
        public string Category { get; set; }
        public Nullable<int> Item_No { get; set; }
        public string Item_Name { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string Brand { get; set; }
    }
}