using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Databases
    {
        [Key]
        public int Number { get; set; }
        public string Name { get; set; }
        public string data_version { get; set; }
        public string Note { get; set; }
        public string server { get; set; }
        public string login { get; set; }
        public string password { get; set; }
    }
}
