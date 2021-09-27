using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Stock
{
    public class program_proprties
    {
        public int id { get; set; }
        public bool? Use_HijriDate { get; set; }
        public string backup_path { get; set; }
        public string Co_Name { get; set; }
        public int? DawamEnd { get; set; }
        public int? FingerPrint_id { get; set; }
        public string VFD_PortNo { get; set; }
        public string BackGroundImage2 { get; set; }
        public string ImageHeader { get; set; }
        public int? ProgramLanguage { get; set; }
        public bool? HasTazamon { get; set; }
        public int? Program_SMS_List_Id { get; set; }
        public int? HijriCorectionDayes { get; set; }
        public long? VFD_BaudRate { get; set; }
        public string ImageFooter { get; set; }
        public bool? IsPOS { get; set; }
        public int? Header_Hight { get; set; }
        public int? Footer_Hight { get; set; }
        public int? VFDCharCount { get; set; }
        public int? VFDCountSpaceBefore { get; set; }
        public string MailSMTP_SenderEmail { get; set; }
        public string MailSMTP { get; set; }
        public string MailSMTPPort { get; set; }
        public string MailSMTP_UserName { get; set; }
        public string MailSMTP_Password { get; set; }
        public bool? MailSMTP_EnableSsl { get; set; }
        public bool? EInv_IsActive { get; set; }
        public string EInv_FTPServerName { get; set; }
        public string EInv_FTPServerPath { get; set; }
        public string EInv_FTPUsername { get; set; }
        public string EInv_FTPPassword { get; set; }

    }
}
