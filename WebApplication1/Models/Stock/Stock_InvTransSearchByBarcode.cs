using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Stock
{
    public class Stock_InvTransSearchByBarcode
    {
        [Key]
        public string Barcode_Id { get; set; }
        public long? Item_Id { get; set; }        
        public double? Barcode_Price { get; set; }
        public double? Barcode_PurchasePrice { get; set; }
        public int? Stock_Units_id { get; set; }
        public double? Quntity { get; set; }
        public int? Stock_ItemTypeAndSize_Id { get; set; }
        public long? Stock_ItemTypeAndGroups_Details_id { get; set; }
        public int? Items_Discount { get; set; }
        public double? Stock_items_tax { get; set; }
        public bool Items_IsUnderLot { get; set; }
        public string ItemUnitList { get; set; }
        public double? OfferPrice { get; set; }
        public double? ItemStock { get; set; }
        public long? Items_AgentId { get; set; }
    }
}
