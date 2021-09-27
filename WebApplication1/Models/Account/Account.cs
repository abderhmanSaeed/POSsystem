using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Account
    {
        [Key]
        public long Accounts_Id { get; set; }
        public bool Accounts_IsMain { get; set; } 
        public long? Accounts_FatherId { get; set; }
        public string Accounts_Code { get; set; }
        public string Accounts_Name { get; set; }
        public string Accounts_TaxRegNum { get; set; }
        public string Accounts_mobile { get; set; }
        public string Accounts_Address { get; set; }
        public string Accounts_Notes { get; set; }
        public Double? Accounts_Discount { get; set; }
        
        public int? Accounts_Type_id { get; set; }
        
        public bool? Accounts_IsDistributor { get; set; }
        public bool? Accounts_IsEmp { get; set; }
        public bool? Accounts_IsClient { get; set; }
        public bool? Accounts_IsFinalAcount { get; set; }
        public long? Accounts_IsCloseInAccount { get; set; }
        
        public int? Acounts_Commition { get; set; }

        /*
        public string Accounts_ClientId { get; set; }
        public int? Nationality { get; set; }
        public string Accounts_Hewala_BankName { get; set; }
        public string Accounts_Hewala_AccountNo { get; set; }
        public string Accounts_Hewala_Mostafid_BankName { get; set; }
        public string Accounts_Hewala_Mostafid_BankAddres { get; set; }
        public string Accounts_Hewala_Mostafid_BankCode { get; set; }
        public int? Aqar_MokawalaType_id { get; set; }
        public string Accounts_ClientJob { get; set; }
        public string Accounts_ClientJobTell { get; set; }
        public string Accounts_ClientMobile2 { get; set; }
        public string Accounts_ClientMobile3 { get; set; }
        public string Accounts_ClientIdType_Id { get; set; }
        public string Accounts_ClientId_Place { get; set; }
        public string Accounts_ClientId_Date { get; set; }
        public bool? Accounts_IsSandouk { get; set; }
        public int? HR_TargetCard_id { get; set; }
        */
        public int? HR_TargetCard_id { get; set; }
        public bool? Accounts_IsSandouk { get; set; }
        public bool? Accounts_IsNotActive { get; set; }
        /*
        public string Accounts_ContactPerson_Name { get; set; }
        public string Accounts_ContactPerson_Postion { get; set; }
        public string Accounts_PostSignal { get; set; }
        public string Accounts_PostOffice { get; set; }
        public int? Levels_id { get; set; }
        public string Accounts_ContactPerson_TafweedNo { get; set; }
        public string Accounts_ContactPerson_TafweedDate { get; set; }
        */
    }
}
