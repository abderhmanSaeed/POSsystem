using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Stock
{
    public class Stock_InvType
    {
        [Key]
        public int InvType_id { get; set; }
        public string InvType_name { get; set; }
        public string InvType_Type { get; set; }
        public int? Clients_Type_id { get; set; }
        public long? Client_AccountId { get; set; }
        public bool? InvType_IsCarServiceInv { get; set; }
        public bool? InvType_PrintA4Size { get; set; }
        public bool? InvType_ShowImpInPrint { get; set; }
        public long? MainAccountId { get; set; }
        public long? SandoukAccountId { get; set; }
        public long? DiscountAccountId { get; set; }
        public long? CommitionAccountId { get; set; }
        public string InvType_CustomerFieldName { get; set; }
        public bool? InvType_IsMoveInv { get; set; }
        public int? From_Stock_ShopCard_Id { get; set; }
        public bool? From_Stock_ShopCard_Id_IsFixed { get; set; }
        public int? Stock_ShopCard_Id { get; set; }
        public bool? Stock_ShopCard_Id_IsFixed { get; set; }
        public int? InvType_NCopies { get; set; }
        public long? CashClientAccount { get; set; }
        public bool? InvType_ShowSubItem { get; set; }
        public bool? InvType_ShowBarcodeField { get; set; }
        public bool? InvType_ShowClientField { get; set; }
        public int? InvType_ItemButtonWidth { get; set; }
        public int? InvType_ItemButtonHight { get; set; }
        public int? BranchId { get; set; }
        public bool? InvType_IsFactoryInv { get; set; }
        public long? FactoryAccountId { get; set; }
        public long? RawMaterialAccountId { get; set; }
        public long? MediatorAccountId { get; set; }
        public bool? InvType_IsAdjustmentStockInv { get; set; }
        public long? ItemsInputAccountId { get; set; }
        public long? ItemsOutputAccountId { get; set; }
        public bool? IsFirstStockPeriod { get; set; }
        public bool? InvType_HasExtrenalReport { get; set; }
        public string InvType_name_En { get; set; }
        public int? print_count { get; set; }
        public bool? InvType_FullScreenPOSUser { get; set; }
        public bool? InvType_DontMergSimlarItems { get; set; }
        public bool? InvType_AddDiscountAutomatic { get; set; }
        public int? Stock_CountLastDayes { get; set; }
        public bool? InvType_UpdateItemWithLastPrice { get; set; }
        public string InvFormBackColor { get; set; }
        public bool? InvType_ShowHallsAndTables { get; set; }
        public string InvType_ExtrenalReportName { get; set; }
        public long? Stock_InvType_TaxAccountId { get; set; }
        public bool? InvType_PrintAndNewWhenSave { get; set; }
        public int? Stock_GridFontSize { get; set; }
        public string InvType_SendEmailToThisEmail { get; set; }
        public bool? InvType_HideItemNameOnGroups { get; set; }
        public bool? InvType_NoCashByDefault { get; set; }
        public bool? InvType_NoExitExceptAdmin { get; set; }
        public bool? InvType_ShowLastItemOnGridAtFirst { get; set; }
    }
}
