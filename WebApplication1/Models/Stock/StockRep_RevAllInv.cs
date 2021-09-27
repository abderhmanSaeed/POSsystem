using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Stock
{
    public class StockRep_RevAllInv
    {
        [Key]
        public long InvMain_id { get; set; }
        public int? InvMain_TypeSerial { get; set; }
        public int InvType_id { get; set; }
        public string InvType_name { get; set; }
        public long? Accounts_Id { get; set; }
        public string Accounts_Name { get; set; }
        public DateTime? InvMain_datem { get; set; }
        public DateTime? InvMain_add_date { get; set; }
        public string InvTotal { get; set; }
        public double? InvMain_discount { get; set; }
        public int? payments_types_id { get; set; }
        public string payments_types_desc { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int? Bransh_id { get; set; }
        public double InvMain_TotalDiscountForRows { get; set; }
        public long? SalesMan_id { get; set; }
        public string InvType_Type { get; set; }
        public double? InvMain_Payment { get; set; }
        public double Stock_InvMain_tax_amount { get; set; }
        /*
        public long InvMain_id { get; set; }
        public int? InvMain_TypeSerial { get; set; }
        public int InvType_id { get; set; }
        public string InvType_name { get; set; }
        public long? Accounts_Id { get; set; }
        public string Accounts_Name { get; set; }
        public DateTime? InvMain_datem { get; set; }
        public DateTime? InvMain_add_date { get; set; }
        public double? InvMain_discount { get; set; }
        //public int? payments_types_id { get; set; }
        //public string payments_types_desc { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        //public int? Bransh_id { get; set; }
        public double InvMain_TotalDiscountForRows { get; set; }
        public int? SalesMan_id { get; set; }
        //public string InvType_Type { get; set; }
        //public double? InvMain_Payment { get; set; }
        public double Stock_InvMain_tax_amount { get; set; }
        */
    }
}
