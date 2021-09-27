using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Stock
{
    public class Stock_ItemsAndUnits
    {
        [Key]
        public long Stock_ItemsAndUnits_Id { get; set; }
        public int Stock_Units_id { get; set; }
        public double? Stock_ItemsAndUnitsCount { get; set; }
        [ForeignKey("Stock_items")]
        public long Items_id { get; set; }

    }
}
