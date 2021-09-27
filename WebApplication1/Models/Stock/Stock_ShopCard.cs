using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Stock
{
    public class Stock_ShopCard
    {
        [Key]
        public int Stock_ShopCard_Id { get; set; }
        public string Stock_ShopCard_desc { get; set; }
        public string Stock_ShopCard_Notes { get; set; }
        public long? Stock_ShopCard_AccountId { get; set; }
        public int? Stock_ShopCard_BranchId { get; set; }
    }
}
