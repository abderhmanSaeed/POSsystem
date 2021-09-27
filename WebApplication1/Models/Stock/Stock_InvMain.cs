using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DTO;

namespace WebApplication1.Models.Stock
{
    public class Stock_InvMain
    {
        [Key]
        public long InvMain_id { get; set; }
        public long? InvMain_TypeSerial { get; set; }
        public int? InvType_id { get; set; }
      //  [Display(Name="ClientID")]
        public long? InvMain_Client_ID { get; set; }
        [ForeignKey("InvMain_Client_ID")]
        public virtual Account Account { get; set; }
        public int? InvMain_user_id { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InvMain_datem { get; set; }
        public double? InvMain_discount { get; set; }
        public int? InvMain_PaymentTypeID { get; set; }
        [ForeignKey("InvMain_PaymentTypeID")]
        public virtual payments_types payments_types { get; set; }
        //public int? payments_types_id { get; set; }

        public double? InvMain_Payment { get; set; }
        public double? InvMain_Remain { get; set; }
        public string InvMain_notes { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InvMain_add_date { get; set; }
        public string InvMain_DocId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InvMain_DocDate { get; set; }
        public long? Stock_Cars_Id { get; set; }
        public string InvMain_CounterNum { get; set; }
        public long? AccountSheetNo { get; set; }
        public int? From_Stock_ShopCard_id { get; set; }
        public int? To_Stock_ShopCard_id { get; set; }
        public bool? IsTazamonOK { get; set; }
        public bool? IsSuccessTazamon { get; set; }
        public int? Bransh_id { get; set; }
        public int? InvMain_ArrangeNo { get; set; }
        public long? Stock_Lot_id { get; set; }
        public double? PaymentCash { get; set; }
        public double? RaminingCash { get; set; }
        public string Stock_DiscountCopons_GUID { get; set; }
        public int? print_count { get; set; }
        public double? InvMain_TotalDiscountForRows { get; set; }
        public long? SalesMan_id { get; set; }
        public bool? InvMain_Payment_IsNotCash { get; set; }
        public int? InvMain_DeliveryDayesCount { get; set; }
        public long? InvMain_SandoukAccountId { get; set; }
        public int? Stock_Hall_Tables_id { get; set; }
        public bool? Stock_InvMain_TableIsClosed { get; set; }
        public double? Stock_InvMain_tax_amount { get; set; }
        public int? Stock_DiscountCopons_MaxInvItems { get; set; }
        public double? total_price { get; set; }
        public double? total_discount { get; set; }
        public double? final_price { get; set; }
        public double? total_receipt_price { get; set; }
        public double? total_receipt_price_after_discount { get; set; }
        public double? total_receipt_discount { get; set; }
        public double? total_final_receipt { get; set; }
        public double? total_qty { get; set; }
        public double? paid_amount  { get; set; }
        public double? final_tax  { get; set; }
        public double? remain_amount  { get; set; }
        public bool? discount_exist  { get; set; }
        public string discount_type { get; set; }
        public double? discount_value { get; set; }
        public double? discount_amount { get; set; }
        public string payment_type { get; set; }

        public List<Stock_InvMainDetails> Stock_InvMainDetails { get; set; }
       // public List<Stock_InvMainDetailsDTO> Stock_InvMainDetailsDTO { get; set; }

    }
}
