using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.DTO
{
    public class Stock_InvMainDetailsDTO
    {
        public long Stock_InvMainDetails_id { get; set; }
        public long Items_id { get; set; }
        public string Items_name_ar { get; set; }
        public string Items_name_en { get; set; }
        public double? InvMainDetails_ItemPrice { get; set; }
        public double? InvMainDetails_Quntity { get; set; }
        public string InvMainDetails_notes { get; set; }
        public int? Stock_Units_id { get; set; }
        public long? emp_id { get; set; }
        public double? InvMainDetails_ItemUnitCount { get; set; }
        public bool? Stock_InvMainDetails_IsSubItem { get; set; }
        public bool? Stock_InvMainDetails_IsUsed { get; set; }
        public long? Stock_InvMainDetails_MainItemId { get; set; }
        public long InvMain_id { get; set; }
        public bool? IsTazamonOK { get; set; }
        public bool? IsSuccessTazamon { get; set; }
        public double? InvMainDetails_Quntity_add { get; set; }
        public double? InvMainDetails_Quntity_sub { get; set; }
        public double? InvMainDetails_RealStockQty { get; set; }
        public long? Stock_Lot_id { get; set; }
        public int? Stock_ItemTypeAndSize_Id { get; set; }
        public long? Stock_ItemTypeAndGroups_Details_id { get; set; }
        public double? Stock_InvMainDetails_DiscountPerstage { get; set; }
        public double? Stock_InvMainDetails_DiscountTotal { get; set; }
        public double? Stock_InvMainDetails_TotalAfterDiscount { get; set; }
        public string InvMainDetails_ItemBarcode { get; set; }
        public double? InvMainDetails_Count { get; set; }
        public double? InvMainDetails_FreeQuntityAdd { get; set; }
        public double? InvMainDetails_FreeQuntitySub { get; set; }
        public double? Stock_InvMainDetails_tax { get; set; }
        public double? Stock_InvMainDetails_tax_amount { get; set; }
        public bool? InvMainDetails_IsPrintPrepareInv { get; set; }
    }
}
