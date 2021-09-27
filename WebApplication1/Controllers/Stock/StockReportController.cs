using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.Stock;

namespace WebApplication1.Controllers.Stock
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class StockReportController : Controller
    {
        private readonly TodoContext _context;
        private string id_var = "admin";
        private string pass_var = "123";

        public StockReportController(TodoContext context)
        {
            _context = context;
        }

        // POST: api/StockReport/ItemsMovment
        [HttpPost]
        [Route("ItemsMovment")]
        public IActionResult PostDetailsTrans([FromForm] string invType_id,
                                            [FromForm] string items_VendourId,
                                            [FromForm] string fatherID,
                                            [FromForm] string items_id,
                                            [FromForm] string accounts_Id,
                                            [FromForm] string invMain_user_id,
                                            [FromForm] string invMain_PaymentTypeID,
                                            [FromForm] string to_Stock_ShopCard_id,
                                            [FromForm] string bransh_id,
                                            [FromForm] string emp_id,
                                            [FromForm] string invMain_datem_from,
                                            [FromForm] string invMain_datem_to,
                                            [FromQuery] string api_id,
                                            [FromQuery] string pass)
        {
            if ((api_id != id_var) || (pass != pass_var))
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Cond = "";

            Cond = Cond + (invType_id != null ? " AND (InvType_id IN (" + invType_id.ToString() + ")) " : "");
            Cond = Cond + (items_VendourId != null ? " AND (Items_VendourId = " + items_VendourId.ToString() + ") " : "");

            Cond = Cond + (fatherID != null ? " AND (FatherID = " + fatherID.ToString() + ") " : "");
            Cond = Cond + (items_id != null ? " AND (Items_id = " + items_id.ToString() + ") " : "");
            Cond = Cond + (accounts_Id != null ? " AND (Accounts_Id = " + accounts_Id.ToString() + ") " : "");
            Cond = Cond + (invMain_user_id != null ? " AND (InvMain_user_id = " + invMain_user_id.ToString() + ") " : "");
            Cond = Cond + (invMain_PaymentTypeID != null ? " AND (InvMain_PaymentTypeID = " + invMain_PaymentTypeID.ToString() + ") " : "");
            Cond = Cond + (to_Stock_ShopCard_id != null ? " AND (To_Stock_ShopCard_id = " + to_Stock_ShopCard_id.ToString() + ") " : "");
            Cond = Cond + (bransh_id != null ? " AND (Bransh_id = " + bransh_id.ToString() + ") " : "");
            Cond = Cond + (emp_id != null ? " AND (Emp_id = " + emp_id.ToString() + ") " : "");

            if ((invMain_datem_from != null) && (invMain_datem_to != null))
            {
                Cond = Cond + " AND (InvMain_datem >= CONVERT(DATETIME, '" + invMain_datem_from.ToString() + "', 102)) AND " +
                                "(InvMain_datem <= CONVERT(DATETIME, '" + invMain_datem_to.ToString() + "', 102)) ";
            }

            
            var SQL_Raseed_Cond = "";
            /*
            if (show_begining_balance != null)
            {
                if ((show_begining_balance == "1") && (date_from != null) && (account != null))
                {
                    var Rassed_Var1 = " CAST(ROUND(dbo.Account_GetRaseedByAcountIdToDate(" + account.ToString() + ", '" + date_from.ToString() + "'), 2, 0) AS decimal(18, 2)) ";
                    SQL_Raseed_Cond = "SELECT  0 AS ROWNUMBER1,  0 AS Account_Sheet_id, 0 AS M1, 0 AS D1, 'رصيد اول المدة' AS Account_Sheet_Details_Notes, '" +
                                        date_from.ToString() + "' AS Account_Sheet_Details_date, " +
                                        "'' AS Account_Sheet_Details_DocNo, " + Rassed_Var1 + " AS rasseed " +
                                    " " +
                                    "UNION ALL ";
                }
            }
            */

            
            if (Cond != "")
            {
                Cond = Cond.Substring(5, Cond.Length - 5);
            }


            //var Cond1 = Cond == "" ? "" : " AND " + Cond;
            var Cond2 = Cond == "" ? "" : " WHERE " + Cond;



            var sql = SQL_Raseed_Cond;/*
                        "SELECT ROW_NUMBER()  OVER (ORDER BY " + arrangeBy.ToString() + ", Account_Sheet_Details_id) as ROWNUMBER1, " +
                              "Account_Sheet_id, M1, D1, Account_Sheet_Details_Notes, Account_Sheet_Details_date, Account_Sheet_Details_DocNo, " +
                              " rt.runningTotal AS rasseed " +
                        "FROM         AccountTransactionDetails AS t " +
                            "cross apply(select sum(M1) - sum(D1) as runningTotal " +
                                            "from AccountTransactionDetails " +
                                  "where (ROWNUMBER <= t.ROWNUMBER) " +
                                                                     Cond1 + " ) as rt " +
                                                                     Cond2 + " ";// +
                        //"ORDER BY ; ";
            */
            sql = SQL_Raseed_Cond +
                    "SELECT     InvMain_id, InvMain_TypeSerial, InvType_id, InvType_name, Accounts_Id, Accounts_Name, InvMain_datem, InvMain_user_id, InvMain_PaymentTypeID, Items_id, " +
                        "Items_name_ar, InvType_Type, InvMainDetails_Quntity, FatherID, FatherName, To_Stock_ShopCard_Id, " +
                        "InvMainDetails_Quntity_add, InvMainDetails_Quntity_sub, 0.0 AS Rasseed " + 
                     "FROM StockRep_ItemMovement "+
                    Cond2 +
                    "UNION " +
                    "SELECT     InvMain_id, InvMain_TypeSerial, InvType_id, InvType_name, Accounts_Id, Accounts_Name, InvMain_datem, InvMain_user_id, InvMain_PaymentTypeID, Items_id, " +
                        "Items_name_ar, InvType_Type, InvMainDetails_Quntity, FatherID, FatherName, To_Stock_ShopCard_Id, " +
                        "InvMainDetails_Quntity_add, InvMainDetails_Quntity_sub, 0.0 AS Rasseed " +
                     "FROM StockRep_ItemMovement " +
                    Cond2.Replace("To_Stock_ShopCard_Id =", "FROM_Stock_ShopCard_Id =").ToString() + "  " +
                    "ORDER BY InvMain_datem, InvMain_id ";
            //.Database.ExecuteSqlCommand(sql); //
            var spResult = _context.StockRep_ItemMovement
                .FromSql(sql)
                .ToList();

            return Ok(spResult);

        }


        // POST: api/StockReport/ItemsStock
        [HttpPost]
        [Route("ItemsStock")]
        public IActionResult PostItemsStock([FromForm] string items_VendourId,
                                            [FromForm] string fatherID,
                                            [FromForm] string items_id,
                                            [FromForm] string to_Stock_ShopCard_id,
                                            [FromForm] string itemStock,
                                            [FromForm] string itemStock_val,
                                            [FromForm] string check_items_that_exceded,
                                            [FromForm] string date_to,
                                            [FromQuery] string api_id,
                                            [FromQuery] string pass)
        {
            if ((api_id != id_var) || (pass != pass_var))
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Cond = "";


            Cond = Cond + (items_VendourId != null ? " AND (Stock_items.Items_VendourId = " + items_VendourId.ToString() + ") " : "");

            Cond = Cond + (fatherID != null ? " AND (Stock_items_1.Items_id = " + fatherID.ToString() + ") " : "");
            Cond = Cond + (items_id != null ? " AND (Stock_items.Items_id = " + items_id.ToString() + ") " : "");
            to_Stock_ShopCard_id = (to_Stock_ShopCard_id != null ? to_Stock_ShopCard_id : "0");

            /*
             
            Dim ff As String = ""
            If IsDate(InvMain_datem.Text) = True Then
                ff = "'" & CDate(CheckDateMaskForSave(InvMain_datem.Text)).ToString("yyyy-MM-dd") & "'"
            Else
                ff = "NULL"
            End If
            */
            var ff = "";
            if (date_to != null)
            {
                ff = "'" + date_to.ToString() + "'";
            }
            else
            {
                ff = "NULL";
            }

            var Lot_Id_Var = "0";
            var Stock_ItemTypeAndSize_Id_Var = "NULL";
            var Stock_ItemTypeAndGroups_Details_id_Var = "NULL";
            var ExpireDate_From_TO = "NULL, NULL";

            if (check_items_that_exceded != null)
            {
                if (check_items_that_exceded.ToString() == "true")
                {
                    Cond = Cond + " AND (dbo.GetItemStockByItemId(dbo.Stock_items.Items_id, " + to_Stock_ShopCard_id + ", " + Lot_Id_Var + ", " + Stock_ItemTypeAndSize_Id_Var + ", " + Stock_ItemTypeAndGroups_Details_id_Var + ", " + ff + ", " + ExpireDate_From_TO + ") < ISNULL(Stock_items.Items_MinimumQty, 0)) ";
                }                    
            }

            if (itemStock != null && itemStock_val != null)
            {
                Cond = Cond + " AND (dbo.GetItemStockByItemId(dbo.Stock_items.Items_id, " + to_Stock_ShopCard_id + ", " + Lot_Id_Var + ", " + Stock_ItemTypeAndSize_Id_Var + ", " + Stock_ItemTypeAndGroups_Details_id_Var + ", " + ff + ", " + ExpireDate_From_TO + ") " + itemStock.ToString() + " " + itemStock_val.ToString() + ") ";
            }

            /*
            Cond = Cond + (invMain_user_id != null ? " AND (InvMain_user_id = " + invMain_user_id.ToString() + ") " : "");
            Cond = Cond + (invMain_PaymentTypeID != null ? " AND (InvMain_PaymentTypeID = " + invMain_PaymentTypeID.ToString() + ") " : "");
            Cond = Cond + (to_Stock_ShopCard_id != null ? " AND (To_Stock_ShopCard_id = " + to_Stock_ShopCard_id.ToString() + ") " : "");
            Cond = Cond + (bransh_id != null ? " AND (Bransh_id = " + bransh_id.ToString() + ") " : "");
            Cond = Cond + (emp_id != null ? " AND (Emp_id = " + emp_id.ToString() + ") " : "");

            if ((invMain_datem_from != null) && (invMain_datem_to != null))
            {
                Cond = Cond + " AND (InvMain_datem >= CONVERT(DATETIME, '" + invMain_datem_from.ToString() + "', 102)) AND " +
                                "(InvMain_datem <= CONVERT(DATETIME, '" + invMain_datem_to.ToString() + "', 102)) ";
            }
            */


            var SQL_Raseed_Cond = "";
            /*
            if (show_begining_balance != null)
            {
                if ((show_begining_balance == "1") && (date_from != null) && (account != null))
                {
                    var Rassed_Var1 = " CAST(ROUND(dbo.Account_GetRaseedByAcountIdToDate(" + account.ToString() + ", '" + date_from.ToString() + "'), 2, 0) AS decimal(18, 2)) ";
                    SQL_Raseed_Cond = "SELECT  0 AS ROWNUMBER1,  0 AS Account_Sheet_id, 0 AS M1, 0 AS D1, 'رصيد اول المدة' AS Account_Sheet_Details_Notes, '" +
                                        date_from.ToString() + "' AS Account_Sheet_Details_date, " +
                                        "'' AS Account_Sheet_Details_DocNo, " + Rassed_Var1 + " AS rasseed " +
                                    " " +
                                    "UNION ALL ";
                }
            }
            */


            //Cond = Cond + " AND (dbo.Stock_items.Items_is_main = 0) ";

            if (Cond != "")
            {
                Cond = Cond.Substring(5, Cond.Length - 5);
            }

            //var Cond1 = Cond == "" ? "" : " AND " + Cond;
            var Cond2 = Cond == "" ? "" : " WHERE " + Cond;

            //"DefaultConnection": "Data Source=sql6001.site4now.net;Initial Catalog=DB_A43699_db;User Id=DB_A43699_db_admin;Password=123456a@;"

            var sql = SQL_Raseed_Cond;/*
                        "SELECT ROW_NUMBER()  OVER (ORDER BY " + arrangeBy.ToString() + ", Account_Sheet_Details_id) as ROWNUMBER1, " +
                              "Account_Sheet_id, M1, D1, Account_Sheet_Details_Notes, Account_Sheet_Details_date, Account_Sheet_Details_DocNo, " +
                              " rt.runningTotal AS rasseed " +
                        "FROM         AccountTransactionDetails AS t " +
                            "cross apply(select sum(M1) - sum(D1) as runningTotal " +
                                            "from AccountTransactionDetails " +
                                  "where (ROWNUMBER <= t.ROWNUMBER) " +
                                                                     Cond1 + " ) as rt " +
                                                                     Cond2 + " ";// +
                        //"ORDER BY ; ";
            */
            sql = "SELECT     Stock_items.Items_id, Stock_items.Items_code, Stock_items.Items_name_ar, Stock_items.Items_name_en, " +
                        "Stock_items.Items_perchase_price, Stock_items.Items_sell_price, dbo.GetItemStockByItemId(dbo.Stock_items.Items_id, " + to_Stock_ShopCard_id + ", " + Lot_Id_Var + ", " + Stock_ItemTypeAndSize_Id_Var + ", " + Stock_ItemTypeAndGroups_Details_id_Var + ", " + ff + ", " + ExpireDate_From_TO + ") AS ItemStock, " +
                        "Stock_items_1.Items_id AS FatherID, Stock_items_1.Items_name_ar AS FatherName, dbo.Stock_GetAverageCostForItemByItemId(dbo.Stock_items.Items_id, " + ff + ") AS AverageCost , " +
                            "(SELECT     TOP (1) Barcode_Id " +
                                "FROM dbo.Stock_Items_Barcode " +
                                "WHERE   " +
                                    "(Item_Id = dbo.Stock_items.Items_id)) AS FirstItemBarcode,  " +
                        "dbo.Stock_GetItemDescListTable(dbo.Stock_items.Items_id) AS ItemDescListTable " +
                    "FROM         Stock_items INNER JOIN " +
                        "Stock_items AS Stock_items_1 ON dbo.Stock_items.Items_FatherId = Stock_items_1.Items_id " +
                        Cond2 +
                    " ORDER BY Stock_items.Items_name_ar ";

            //.Database.ExecuteSqlCommand(sql); //
            var spResult = _context.StockRep_ItemsStock
                .FromSql(sql)
                .ToList();

            return Ok(spResult);

        }

        // POST: api/StockReport/BillReview
        [HttpPost]
        [Route("BillReview")]
        public IActionResult PostBillReview([FromForm] string invType_id,
                                            [FromForm] string items_VendourId,
                                            [FromForm] string invMain_user_id,
                                            [FromForm] string bransh_id,
                                            [FromForm] string emp_id,
                                            [FromForm] string invMain_datem_from,
                                            [FromForm] string invMain_datem_to,
                                            [FromForm] string reportType,
                                            [FromQuery] string api_id,
                                            [FromQuery] string pass)
        {
            if ((api_id != id_var) || (pass != pass_var))
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Cond = "";

            Cond = Cond + (invType_id != null ? " AND (Stock_InvMain.InvType_id IN (" + invType_id.ToString() + ")) " : "");
            Cond = Cond + (items_VendourId != null ? " AND (Stock_InvMain.Accounts_Id = " + items_VendourId.ToString() + ") " : "");

            Cond = Cond + (invMain_user_id != null ? " AND (id = " + invMain_user_id.ToString() + ") " : "");

            if (reportType != null)
            {
                if (reportType == "1")
                {
                    Cond = Cond + (bransh_id != null ? " AND (Stock_InvMain.Bransh_id = " + bransh_id.ToString() + ") " : "");
                }
                else if (reportType == "2")
                {
                    Cond = Cond + (bransh_id != null ? " AND (Bransh_id = " + bransh_id.ToString() + ") " : "");
                }
            }
            //Cond = Cond + (bransh_id != null ? " AND (Stock_InvMain.Bransh_id = " + bransh_id.ToString() + ") " : "");
            Cond = Cond + (emp_id != null ? " AND (SalesMan_id = " + emp_id.ToString() + ") " : "");

            /*
             If NotCompletePay.Checked = True Then
                SQL_Condition = " (TotalSanadat = 0 OR TotalSanadat <> (TotalAfterDiscount + ISNULL(Stock_InvMain_tax_amount, 0))) "
                If SQL_Where = "" Then
                    SQL_Where = " WHERE " & SQL_Condition
                Else
                    SQL_Where = SQL_Where & " AND " & SQL_Condition
                End If
            End If
             */
            
            if ((invMain_datem_from != null) && (invMain_datem_to != null))
            {
                Cond = Cond + " AND (InvMain_datem >= CONVERT(DATETIME, '" + invMain_datem_from.ToString() + "', 102)) AND " +
                                "(InvMain_datem <= CONVERT(DATETIME, '" + invMain_datem_to.ToString() + "', 102)) ";
            }
            

            var SQL_Raseed_Cond = "";
          
            if (Cond != "")
            {
                Cond = Cond.Substring(5, Cond.Length - 5);
            }

            var Cond2 = Cond == "" ? "" : " WHERE " + Cond;

            var sql = SQL_Raseed_Cond;

            object spResult = null;

            if (reportType != null)
            {
                if (reportType == "1")
                {
                    
                    sql = "SELECT     dbo.Stock_InvMain.InvMain_id, dbo.Stock_InvMain.InvMain_TypeSerial, dbo.Stock_InvType.InvType_id, dbo.Stock_InvType.InvType_name, " +
                                "dbo.Accounts.Accounts_Id, dbo.Accounts.Accounts_Name, dbo.Stock_InvMain.InvMain_datem, dbo.Stock_InvMain.InvMain_add_date, " +
                                "dbo.Stock_InvMain.InvMain_discount, dbo.payments_types.payments_types_id, " +
                                "dbo.payments_types.payments_types_desc, dbo.users.id, dbo.users.name, dbo.Stock_InvMain.Bransh_id,  " +
                                "ISNULL(dbo.Stock_InvMain.InvMain_TotalDiscountForRows, 0) AS InvMain_TotalDiscountForRows, " +
                                "dbo.Stock_InvMain.SalesMan_id, dbo.Stock_InvType.InvType_Type, dbo.Stock_InvMain.InvMain_Payment,  " +
                                "ISNULL(dbo.Stock_InvMain.Stock_InvMain_tax_amount, 0) AS Stock_InvMain_tax_amount " +
                         "into #Stock_InvMain_temp " +
                         "FROM         dbo.Stock_InvMain INNER JOIN " +
                                "dbo.Stock_InvType ON dbo.Stock_InvMain.InvType_id = dbo.Stock_InvType.InvType_id INNER JOIN " +
                                "dbo.users ON dbo.Stock_InvMain.InvMain_user_id = dbo.users.id LEFT OUTER JOIN " +
                                "dbo.payments_types ON dbo.Stock_InvMain.InvMain_PaymentTypeID = dbo.payments_types.payments_types_id LEFT OUTER JOIN " +
                                "dbo.Accounts ON dbo.Stock_InvMain.InvMain_Client_ID = dbo.Accounts.Accounts_Id " +
                        Cond2 +
                        " ORDER BY InvType_id, InvMain_TypeSerial; " +
                        "SELECT InvMain_id, 0 AS InvMain_TypeSerial, InvType_id, InvType_name, Accounts_Id, Accounts_Name,  InvMain_datem , InvMain_add_date, " +
                              "STR((SELECT     CAST(SUM(InvMainDetails_ItemPrice * InvMainDetails_Quntity * InvMainDetails_ItemUnitCount) AS real)  " +
                                                         "* CASE InvType_Type WHEN '+' THEN - 1 WHEN '-' THEN 1 END AS InvTotal " +
                                  "FROM          dbo.Stock_InvMainDetails " +
                                  "WHERE      (InvMain_id = HairStockRep_RevAllInv.InvMain_id))) AS InvTotal,  " +
                              "InvMain_discount, payments_types_id, payments_types_desc, id, name, Bransh_id, InvMain_TotalDiscountForRows,  " +
                            "0 AS TotalSanadat, InvMain_Payment, " +
                            "Stock_InvMain_tax_amount, InvType_Type, SalesMan_id  " +
                        "FROM #Stock_InvMain_temp HairStockRep_RevAllInv  " +
                        " ORDER BY InvType_id, InvMain_TypeSerial; " +
                        "drop table #Stock_InvMain_temp; ";

                    spResult = _context.StockRep_RevAllInv
                               .FromSql(sql)
                               .ToList();
                }
                else if (reportType == "2")
                {
                    sql = "SELECT     InvMain_id, InvMain_TypeSerial, InvType_id, InvMain_datem, Bransh_id, Items_id, Items_code, Items_name_ar, Items_name_en, InvMainDetails_ItemUnitCount, " +
                            "InvMainDetails_ItemPrice, InvMainDetails_Quntity, Stock_InvMainDetails_TotalAfterDiscount, Stock_InvMainDetails_DiscountTotal, Items_FatherId, FatherName " +
                        "FROM StockRep_RevAllInvGroupsMainItems " +
                        Cond2 +
                        " ORDER BY InvType_id, InvMain_TypeSerial; ";

                    sql = "SELECT     Items_id, Items_code, Items_name_ar, Items_name_en, " +
                                "SUM(InvMainDetails_ItemUnitCount * InvMainDetails_Quntity) AS Qty, " +
                                "SUM(Stock_InvMainDetails_TotalAfterDiscount) AS SumAfterDiscount " +
                            "FROM StockRep_RevAllInvGroupsMainItems " +
                        Cond2 +
                         "GROUP BY Items_id, Items_code, Items_name_ar, Items_name_en ";

                    spResult = _context.StockRep_RevAllInv_Group2
                               .FromSql(sql)
                               .ToList();
                }
            }
            
            return Ok(spResult);

        }
    }
}
