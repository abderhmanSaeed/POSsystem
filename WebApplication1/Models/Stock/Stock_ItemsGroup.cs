using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.Stock;

namespace WebApplication1.Models
{
    public class Stock_ItemsGroup
    {
        [Key]
        public long Stock_ItemsGroup_id { get; set; }
        public string Stock_ItemsGroup_desc { get; set; }
        public List<Stock_ItemsGroup_Details> Stock_ItemsGroup_Details { get; set; }
    }
}
