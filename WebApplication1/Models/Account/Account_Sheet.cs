using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Account_Sheet
    {
        [Key]
        public long Account_Sheet_id { get; set; }
        public long? Close_Accounts_Id { get; set; }
        public int? Bransh_id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}")]
        public DateTime? Account_Sheet_date { get; set; }
        public string Account_Sheet_notes { get; set; }
        public bool? Account_Sheet_IsTrans { get; set; }
        public bool? Account_Sheet_IsRev { get; set; }
        public int? user_id { get; set; }
        public string Account_Sheet_FormId { get; set; }
        public long? Account_Sheet_FormSerial { get; set; }
        public int? Account_Sheet_File_id { get; set; }
        public long? Account_Sheet_File_Serial { get; set; }
        public bool? IsSuccessTazamon { get; set; }
        public string Account_Sheet_FormControlName { get; set; }
        public int? BookFieldId { get; set; }
        public string OpenFormBookFuncName { get; set; }
        public string Account_Sheet_ReceivedBy { get; set; }

        [ForeignKey(nameof(Account_Sheet_id))]
        public ICollection<Account_Sheet_Detail> Account_Sheet_Details { get; set; }
    }
}
