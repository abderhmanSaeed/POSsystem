using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Stock
{
    public class Stock_Settings
    {
        [Key]
        public int Settings_id { get; set; }
        public long? MainAccountId { get; set; }
        public long? VindorAccountId { get; set; }
        public bool? IsCarServShop { get; set; }
        public string StatmentsThatPrintInInvFooter { get; set; }
        public bool? IsContinuousInventory { get; set; }
        public bool? PutItemCodeWithItemName { get; set; }
        public bool? ConvertCodeToBarcode { get; set; }
        public string Txt_MainItemTitleName { get; set; }
        public string Txt_MainItemTitleName_en { get; set; }
        public string TaxRegNum { get; set; }
        public string Custom_StockReport { get; set; }
        public string Custom_StockItemMovement { get; set; }
        public string Custom_StockAllInvRep_Groups { get; set; }
        public string Custom_StockAllInvRep_All { get; set; }
        public string Custom_StockAllInvRep_Details { get; set; }
        public string Custom_StockAllInvRep_AllTotal { get; set; }
        public string Custom_Stock_GetItemsProfits { get; set; }
        public string Custom_StockRep_Emp_Entag { get; set; }
        public bool? HideQtyFromInfoBar { get; set; }
        public string Custom_Stock_AllInvRep_Tax { get; set; }
        public string InvType_Sales { get; set; }
        public string InvType_SalesR { get; set; }
        public string InvType_Purchase { get; set; }
        public string InvType_PurchaseR { get; set; }
        public string InvType_Export { get; set; }
        public string InvType_ExportReturn { get; set; }
        public string InvType_Import { get; set; }
        public string InvType_ImportReturn { get; set; }
        public string Custom_Stock_PrintBarcodeLabel { get; set; }
        public string TaxCode { get; set; }



    }
}
