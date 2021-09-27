using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Programs
    {
        [Key]//prog_id
        public int prog_id { get; set; }
        public string prog_name { get; set; }
        public string prog_menu { get; set; }
        public string prog_name_en { get; set; }
        public List<forms> forms { set; get; }
    }
}
