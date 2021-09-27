using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Stock
{
    public class StockRep_RevAllInv_Group2
    {
        [Key]
        public long Items_id { get; set; }
        public string Items_code { get; set; }
        public string Items_name_ar { get; set; }
        public string Items_name_en { get; set; }
        public double Qty { get; set; }
        public double SumAfterDiscount { get; set; }
    }
}
