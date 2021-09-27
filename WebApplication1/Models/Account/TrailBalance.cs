using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class TrailBalance
    {
        [Key]
        public long Accounts_Id { get; set; }
        public string Accounts_Name { get; set; }
        public Boolean? Accounts_IsMain { get; set; }
        public long? Accounts_FatherId { get; set; }
        public int? level { get; set; }
        public double? rassed { get; set; }
        public string Accounts_Code { get; set; }
        public int? SessionID { get; set; }
        public string Sort { get; set; }
        public Boolean? Accounts_IsNotActive { get; set; }
    }
}
