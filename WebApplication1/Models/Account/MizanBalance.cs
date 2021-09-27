using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class MizanBalance
    {
        [Key]
        public long Accounts_Id { set; get; }
        public string Accounts_Name { set; get; }
        public Boolean? Accounts_IsMain { set; get; }
        public long? Accounts_FatherId { set; get; }
        public int? level { set; get; }
        public double? M1 { set; get; }
        public double? D1 { set; get; }
        public double? rassed { set; get; }
        public string Accounts_Code { set; get; }
        public int? SessionID { set; get; }
        public string Sort { set; get; }
        public double? R_M1 { set; get; }
        public double? R_D1 { set; get; }
        public Boolean? Accounts_IsNotActive { set; get; }

    }
}
