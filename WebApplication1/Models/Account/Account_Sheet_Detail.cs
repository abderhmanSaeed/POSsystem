using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Account_Sheet_Detail
    {
        public long Account_Sheet_id { get; set; }
        public double? Account_Sheet_Details_M { get; set; }
        public double? Account_Sheet_Details_D { get; set; }
        public long? Accounts_Id { get; set; }
        public string Account_Sheet_Details_Notes { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}")]
        public DateTime? Account_Sheet_Details_date { get; set; }
        public string Account_Sheet_Details_DocNo { get; set; }
        public double? Account_Sheet_Details_CurrRate { get; set; }
        public double? Account_Sheet_Details_CurrM { get; set; }
        public double? Account_Sheet_Details_CurrD { get; set; }
        public int? Account_Currency_id { get; set; }
        public long? Account_Sheet_Details_DistributorAccountId { get; set; }
        public bool? Account_Sheet_Details_IsClosedDetails { get; set; }
        public bool? IsSuccessTazamon { get; set; }        
        public int? Account_Sheet_Details_id { get; set; }
        public int? Bransh_id { get; set; }
        [Key]
        public long Account_Sheet_Details_id2 { get; set; }
    }
}
