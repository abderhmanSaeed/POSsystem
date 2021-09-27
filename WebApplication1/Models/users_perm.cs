using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class users_perm
    {
        //[Key]
        [ForeignKey("users")]
        [Column("emp_id")]
        public int id { get; set; }
        //[Key]
        //[Column(Order = 2)]
        public int prog_id { get; set; }
        //[Key]
        //[Column(Order = 3)]
        public int form_id { get; set; }
        //[Key]
        //[Column(Order = 4)]
        public int perm_type_id { get; set; }
        public bool? have_perm { get; set; }
        public int? book_id { get; set; }
    }
}
