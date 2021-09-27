using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Stock
{
    public class Stock_items
    {
        [Key]
        public long Items_id { get; set; }
      //  public long? Stock_InvMainInvMain_id { get; set; }
        public string Items_code { get; set; }
        public bool? Items_is_main { get; set; }
        public long? Items_FatherId { get; set; }
        [ForeignKey("Items_FatherId")]
        public Stock_items Father { get; set; }
        public bool? Items_IsMaterial { get; set; }
        public string Items_name_ar { get; set; }
        public string Items_name_en { get; set; }
        public string Items_desc { get; set; }
        public double? Items_perchase_price { get; set; }
        public double? Items_sell_price { get; set; }
        public string Items_ItemUnit { get; set; }
        public string Items_notes { get; set; }
        public int? Stock_PrinterGroup_id { get; set; }
        public string ItemImagePath { get; set; }
        public bool? Items_IsUnderLot { get; set; }
        public int? Stock_ItemTypeAndGroups_Main_id { get; set; }
        public long? Stock_ItemsCategories_Id { get; set; }
        public double? BigginingStock { get; set; }
        public long? Items_MinimumQty { get; set; }
        public int? Items_Discount { get; set; }
        public int? Stock_Item_Count { get; set; }
        public long? Items_VendourId { get; set; }
        public double? Stock_items_tax { get; set; }
        public long? Items_AgentId { get; set; }
        public double? total_price { get; set; }
        public double? total_discount { get; set; }
      //  public int? Stock_Units_id { get; set; }
      //  [ForeignKey("Stock_Units_id")]
      //  public virtual Stock_Units Stock_Units {get; set;}
        public double? total_price_after_discount { get; set; }
        public double? total_tax { get; set; }
        public double? final_price { get; set; }
        public List<Stock_ItemsAndUnits> Stock_ItemsAndUnits { get; set; }
       // public List<Stock_Units> Stock_Units { get; set; }
        public List<Stock_Items_Barcode> Stock_Items_Barcode { get; set; }
        public List<Stock_itemsSub> Stock_itemsSub { get; set; }
        




    }
}
