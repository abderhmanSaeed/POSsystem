using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class DetailsTrans
    {
        [Key]
        public long ROWNUMBER1 { get; set; }
        public long Account_Sheet_id  { get; set; }
        //public string Accounts_Name { get; set; }
        //public int? Bransh_id { get; set; }
        //public string Bransh_desc { get; set; }
        //public DateTime? Account_Sheet_date { get; set; }
        //public string Account_Sheet_date_H { get; set; }
        //public string Account_Sheet_notes { get; set; }
        //public Boolean? Account_Sheet_IsTrans { get; set; }
        //public Boolean? Account_Sheet_IsRev { get; set; }
        //public int? user_id { get; set; }
        //public string user_name { get; set; }
        public double? M1 { get; set; }
        public double? D1 { get; set; }
        //public long? Accounts_Id { get; set; }
        public string Account_Sheet_Details_Notes { get; set; }
        public DateTime? Account_Sheet_Details_date { get; set; }
        public string Account_Sheet_Details_DocNo { get; set; }
        //public double? Account_Sheet_Details_CurrRate { get; set; }
        //public double? Account_Sheet_Details_CurrM { get; set; }
        //public double? Account_Sheet_Details_CurrD { get; set; }
        //public int? Account_Currency_id { get; set; }
        //public string Account_Currency_name { get; set; }
        //public string Account_Sheet_FormId { get; set; }
        //public long? Account_Sheet_FormSerial { get; set; }
        //public string Account_Sheet_FormControlName { get; set; }
        //public string FormName { get; set; }
        //public int? BookFieldId { get; set; }
        //public string OpenFormBookFuncName { get; set; }
        public Double? rasseed { get; set; }
    }
}
/*
  
ROWNUMBER1, 
Account_Sheet_id, 
Accounts_Name, 
Bransh_id, 
Bransh_desc, 
Account_Sheet_date, 
Account_Sheet_date_H, 
Account_Sheet_notes, 
Account_Sheet_IsTrans, 
Account_Sheet_IsRev, 
user_id, 
user_name, 
M1, 
D1, 
Accounts_Id, 
Account_Sheet_Details_Notes, 
Account_Sheet_Details_date, 
Account_Sheet_Details_DocNo, 
Account_Sheet_Details_CurrRate, 
Account_Sheet_Details_CurrM, 
Account_Sheet_Details_CurrD, 
Account_Currency_id, 
Account_Currency_name, 
Account_Sheet_FormId, 
Account_Sheet_FormSerial, 
Account_Sheet_FormControlName, 
FormName, 
BookFieldId, 
OpenFormBookFuncName, 
runningTotal
                      */