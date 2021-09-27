using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Stock
{
    public class Stock_Items_Barcode
    {
        [Key]
        public long id { get; set; }
        public string Barcode_Id { get; set; }        
        [ForeignKey("Stock_items")]
        [Column("Item_Id")]
        public long Items_id { get; set; }        
        public double? Barcode_Price { get; set; }        
        [ForeignKey("Stock_Units")]
        public int Stock_Units_id { get; set; }
        public double? Quntity { get; set; }
        public double? Barcode_PurchasePrice { get; set; }
        public int? Stock_ItemTypeAndSize_Id { get; set; }
        public long? Stock_ItemTypeAndGroups_Details_id { get; set; }
    }
}
