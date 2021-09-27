using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Bransh
    {
        [Key]
        public int Bransh_id { get; set; }
        public string Bransh_desc { get; set; }
    }
}
