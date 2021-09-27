using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class payments_types
    {
        [Key]
        public int payments_types_id { get; set; }
        public string payments_types_desc { get; set; }
        public long? AccountId { get; set; }
        public double? payments_types_PointValueSR { get; set; }
        public bool? payments_types_IsPoint { get; set; }
    }
}
