using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Stock
{
    public class Stock_ItemsGroup_Details
    {
        [Key]
        public long Stock_ItemsGroup_Details_id { get; set; }        
        public long Items_id { get; set; }
        public long? Stock_ItemsGroup_Details_arange { get; set; }        
        public bool? Stock_ItemsGroup_Show { get; set; }
        [ForeignKey("Stock_ItemsGroup")]
        public long Stock_ItemsGroup_id { get; set; }
    }
}
