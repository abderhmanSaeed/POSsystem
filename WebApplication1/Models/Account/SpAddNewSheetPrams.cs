using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class SpAddNewSheetPrams
    {
        [Key]
        public long Account_Sheet_id { get; set; }
        public long Close_Accounts_Id { get; set; }
        public int Bransh_id { get; set; }
        public string Account_Sheet_date { get; set; }
        public string Account_Sheet_notes { get; set; }
        public int user_id { get; set; }
        public string Account_Sheet_Details_M { get; set; }
        public string Account_Sheet_Details_D { get; set; }
        public string Accounts_Id { get; set; }
        public string Account_Sheet_Details_Notes { get; set; }
        public string Account_Sheet_Details_Date { get; set; }
        public int HasTazamonVar { get; set; }
        public string TazamonFirstCodeFormate { get; set; }
        public string Account_Sheet_FormId { get; set; }
        public string Account_Sheet_FormSerial { get; set; }
        public string Account_Sheet_FormControlName { get; set; }
        public int BookFieldId { get; set; }
        public string OpenFormBookFuncName { get; set; }
        public int Account_Sheet_File_id { get; set; }
        public long Account_Sheet_File_Serial { get; set; }
        public string Account_Sheet_Details_IsClosedDetails { get; set; }
    }
}
