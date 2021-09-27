using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Stock
{
    public class Stock_itemsSub
    {
        [Key]
        public long Stock_itemsSub_id { get; set; }
        [ForeignKey("Stock_items")]
        [Column("Stock_items_id")]
        public long? Items_id { get; set; }
        //[ForeignKey("Stock_items")]
        public long? Stock_itemsSub_items_id { get; set; }
        public double? Stock_itemsSub_Quantity { get; set; }
        public int? InvMainDetails_ItemUnitCount { get; set; }
        //[ForeignKey("Stock_Units")]
        public int? Stock_Units_id { get; set; }
    }
}
