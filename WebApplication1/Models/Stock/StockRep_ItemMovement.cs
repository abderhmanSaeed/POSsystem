using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Stock
{
    public class StockRep_ItemMovement
    {
        [Key]
        public long InvMain_id { get; set; }
        public long? InvMain_TypeSerial { get; set; }
        public int InvType_id { get; set; }
        public string InvType_name { get; set; }
        public long? Accounts_Id { get; set; }
        public string Accounts_Name { get; set; }
        public DateTime? InvMain_datem { get; set; }
        public int? InvMain_user_id { get; set; }
        public int? InvMain_PaymentTypeID { get; set; }
        public long Items_id { get; set; }
        public string Items_name_ar { get; set; }
        public string InvType_Type { get; set; }
        public double? InvMainDetails_Quntity { get; set; }
        public long FatherID { get; set; }
        public string FatherName { get; set; }
        public int? To_Stock_ShopCard_Id { get; set; }
        public double? InvMainDetails_Quntity_add { get; set; }
        public double? InvMainDetails_Quntity_sub { get; set; }
        public decimal? Rasseed { get; set; }
    }
}
