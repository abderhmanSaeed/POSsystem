using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Stock
{
    public class StockRep_RevAllInv_Group
    {
        [Key]
        public long InvMain_id { get; set; }
        public long? InvMain_TypeSerial { get; set; }
        public int? InvType_id { get; set; }
        public DateTime? InvMain_datem { get; set; }
        public int? Bransh_id { get; set; }
        public long Items_id { get; set; }
        public string Items_code { get; set; }
        public string Items_name_ar { get; set; }
        public string Items_name_en { get; set; }
        public int? InvMainDetails_ItemUnitCount { get; set; }
        public double? InvMainDetails_ItemPrice { get; set; }
        public double? InvMainDetails_Quntity { get; set; }
        public double Stock_InvMainDetails_TotalAfterDiscount { get; set; }
        public double Stock_InvMainDetails_DiscountTotal { get; set; }
        public long? Items_FatherId { get; set; }
        public string FatherName { get; set; }
    }
}
