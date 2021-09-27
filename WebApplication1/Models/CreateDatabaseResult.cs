using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class CreateDatabaseResult
    {
        [Key]
        public string CustomerId { get; set; }
        public string DatabaseName { get; set; }
    }
}
