using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Account_Sheet_File
    {
        [Key]
        public int Account_Sheet_File_id { set; get; }
        public string Account_Sheet_File_Name { set; get; }
        public string Account_Sheet_File_Name_EN { set; get; }
        public int? Bransh_id { set; get; }
        public long? Account_Sheet_File_Close_Accounts_Id { set; get; }
        public Boolean? Account_Sheet_File_Static_Close_Accounts_Id { set; get; }
        public Boolean? Account_Sheet_File_M { set; get; }
        public string Account_Sheet_File_M_Desc { set; get; }
        public Boolean? Account_Sheet_File_D { set; get; }
        public string Account_Sheet_File_D_Desc { set; get; }
        public int? Account_Sheet_File_PrintCount { set; get; }

        public Boolean? Account_Sheet_File_IsAutoTrans { set; get; }
        public Boolean? Account_Sheet_File_IsSheetForm { set; get; }
        public Boolean? Account_Sheet_File_HasExternalReport { set; get; }
        public string Account_Sheet_File_ExternalReportName { set; get; }


    }
}
