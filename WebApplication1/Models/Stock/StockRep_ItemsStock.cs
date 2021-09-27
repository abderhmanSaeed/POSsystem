using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Stock
{
    public class StockRep_ItemsStock
    {
        [Key]
        public long Items_id { get; set; }
        public string Items_code { get; set; }
        public string Items_name_ar { get; set; }
        public string Items_name_en { get; set; }
        public double? Items_perchase_price { get; set; }
        public double ItemStock { get; set; }
        public long FatherID { get; set; }
        public string FatherName { get; set; }
        public double? AverageCost { get; set; }
        public string FirstItemBarcode { get; set; }
        public string ItemDescListTable { get; set; }
        public double? Items_sell_price { get; set; }
    }
}
