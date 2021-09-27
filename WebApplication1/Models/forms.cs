using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class forms
    {
        [Key]
        public int form_id { get; set; }
        [ForeignKey("Programs")]
        public int prog_id { get; set; }
        public string form_name { get; set; }
        public string form_design_name { get; set; }
        public bool? IsBook { get; set; }
        public int? BookFather_form_id { get; set; }
        public string BookTableName { get; set; }
        public string BookTableIdName { get; set; }
        public int? BookFieldId { get; set; }
        public string form_name_en { get; set; }

    }
}
