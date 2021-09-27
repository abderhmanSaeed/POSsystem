using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Stock
{
    public class Stock_Units
    {
        [Key]
        public int Stock_Units_id { get; set; }
        public string Stock_Units_desc { get; set; }
    }
}
