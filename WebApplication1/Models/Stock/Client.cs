using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Stock
{
    public class Client
    {
        public long? Clients_Id { get; set; }
        public string Clients_Name { get; set; }
        public string Clients_mobile { get; set; }
        public string Clients_Address { get; set; }
        public string Clients_Notes { get; set; }
        public float? Clients_Discount { get; set; }
        public int? Clients_Type_id { get; set; }
    }
}
