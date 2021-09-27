using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using System.Data.SqlClient;
using System.Globalization;

namespace WebApplication1.Models
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class DetailsTransController : Controller
    {
        private readonly TodoContext _context;
        private string id_var = "admin";
        private string pass_var = "123";

        public DetailsTransController(TodoContext context)
        {
            _context = context;
        }
        /*

        // GET: api/DetailsTrans
        [HttpGet]
        public IEnumerable<DetailsTrans> GetDetailsTrans()
        {
            return _context.DetailsTrans;
        }

        // GET: api/DetailsTrans/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetailsTrans([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var detailsTrans = await _context.DetailsTrans.SingleOrDefaultAsync(m => m.ROWNUMBER1 == id);

            if (detailsTrans == null)
            {
                return NotFound();
            }

            return Ok(detailsTrans);
        }

        // PUT: api/DetailsTrans/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetailsTrans([FromRoute] long id, [FromBody] DetailsTrans detailsTrans)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != detailsTrans.ROWNUMBER1)
            {
                return BadRequest();
            }

            _context.Entry(detailsTrans).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetailsTransExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        */
        // POST: api/DetailsTrans
        [HttpPost]
        //[Route("DetailsTrans")]
        public IActionResult PostDetailsTrans([FromForm] string account,
                                            [FromForm] string branch,
                                            [FromForm] string date_from,
                                            [FromForm] string date_to,
                                            [FromForm] string doc_no,
                                            [FromForm] string notes,
                                            [FromForm] string m,
                                            [FromForm] string d,
                                            [FromForm] string arrangeBy,
                                            [FromForm] string show_not_trans,
                                            [FromForm] string show_begining_balance,
                                            [FromForm] string dont_show_zero_trans,
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

            Cond = Cond + (account != null ? " AND (Accounts_Id = " + account.ToString() + ") " : "");
            Cond = Cond + (branch != null ? " AND (Bransh_id = " + branch.ToString() + ") " : "");

            if ((date_from!= null) && (date_to != null)){
                Cond = Cond + " AND (Account_Sheet_Details_date >= CONVERT(DATETIME, '" + date_from.ToString() + "', 102)) AND " +
                                "(Account_Sheet_Details_date <= CONVERT(DATETIME, '" + date_to.ToString() + "', 102)) ";
            }
            Cond = Cond + (doc_no != null ? " AND (Account_Sheet_Details_DocNo = N'" + doc_no.ToString() + "') " : "");
            Cond = Cond + (notes != null ? " AND (Account_Sheet_Details_Notes LIKE N'%" + notes.ToString() + "%' OR " +
                                                            "Account_Sheet_notes LIKE N'%" + notes.ToString() + "%') " : "");
            
            Cond = Cond + (m != null ? " AND (M1 = " + m.ToString() + ") " : "");
            Cond = Cond + (d != null ? " AND (D1 = " + d.ToString() + ") " : "");

            Cond = Cond + (show_not_trans != null ? " AND (Account_Sheet_IsTrans = 1) " : "");

            var SQL_Raseed_Cond = "";
            if (show_begining_balance != null){
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
            
            

            Cond = Cond + (dont_show_zero_trans != null ? " AND  (ISNULL(M1, 0) > 0 OR ISNULL(D1, 0) > 0) " : "");

            if (Cond != "")
            {
                Cond = Cond.Substring(5, Cond.Length - 5);
            }
            

            var Cond1 = Cond == "" ? "" : " AND " + Cond;
            var Cond2 = Cond == "" ? "" : " WHERE " + Cond;

            var sql = SQL_Raseed_Cond +
                        "SELECT ROW_NUMBER()  OVER (ORDER BY " + arrangeBy.ToString() + ", Account_Sheet_Details_id) as ROWNUMBER1, " +
                              "Account_Sheet_id, M1, D1, Account_Sheet_Details_Notes, Account_Sheet_Details_date, Account_Sheet_Details_DocNo, " +
                              " rt.runningTotal AS rasseed " +
                        "FROM         AccountTransactionDetails AS t " +
                            "cross apply(select sum(M1) - sum(D1) as runningTotal " +
                                            "from AccountTransactionDetails " + 
                                  "where (ROWNUMBER <= t.ROWNUMBER) " +
                                                                     Cond1 + " ) as rt " + 
                                                                     Cond2 + " " +
                        "ORDER BY " + arrangeBy.ToString() + "; ";

            var spResult = _context.DetailsTrans
                .FromSql(sql)
                .ToList();

            return Ok(spResult);
           
        }

        [HttpPost]
        [Route("TrailBalance")]
        public IActionResult PostTrailBalance([FromForm] string account,                                            
                                            [FromForm] string date_from,
                                            [FromForm] string date_to,
                                            [FromForm] string branch,
                                            [FromForm] string isTrans,
                                            [FromForm] string user_id,
                                            [FromForm] string showRaseedType,
                                            [FromForm] string dontShowSubAccounts,
                                            [FromForm] string dontShowNotActiveAccounts,
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

            var RaseedType = "";
            switch (showRaseedType)
            {
                case "1":
                    RaseedType = " AND (rassed > 0) ";
                    break;
                case "2":
                    RaseedType = " AND (rassed < 0) ";
                    break;
                case "3":
                    RaseedType = " AND (rassed <> 0) ";
                    break;
                default:
                    RaseedType = "";
                    break;
            }

            var dontShowSubAccounts_var = "";
            if (dontShowSubAccounts == "1")
            {
                dontShowSubAccounts_var = " AND (Accounts_IsMain = 1) AND (level = 0) ";
            }

            var dontShowNotActiveAccounts_var = "";
            if (dontShowNotActiveAccounts == "1")
            {
                dontShowNotActiveAccounts = " AND (Accounts_IsNotActive = 0 OR Accounts_IsNotActive IS NULL) ";
            }

            var sql = "exec AccountGetChildsRassed_All " +
                                            "'" + account.ToString() + "', " +
                                            "'" + date_from.ToString() + "', " +
                                            "'" + date_to.ToString() + "', " +
                                            "'" + branch.ToString() + "', " +
                                            "'" + isTrans.ToString() + "', " +
                                            "" + user_id.ToString() + "; " +
                    "SELECT * FROM AccountGetChildsRassed_All_Table " +
                   "WHERE " +
                       "(SessionID = " + user_id.ToString() + ") " +
                       RaseedType + dontShowSubAccounts_var + dontShowNotActiveAccounts_var +
                       " ORDER BY Sort  ";
           
            var spResult = _context.TrailBalance
                            .FromSql(sql)
                            .ToList();
          
           
            /*
            sql = "SELECT * FROM AccountGetChildsRassed_All_Table " +
                   "WHERE " +
                       "(SessionID = " + user_id.ToString() + ") " +
                       showRaseedType.ToString() + dontShowSubAccounts.ToString() + dontShowNotActiveAccounts.ToString() +
                       " ORDER BY Sort  ";

            var spResult = _context.TrailBalance
                .FromSql(sql)
                .ToList();
                */

            return Ok(spResult);

        }

        [HttpPost]
        [Route("MezanBalance")]
        public IActionResult PostMezanBalance([FromForm] string account,
                                    [FromForm] string date_from,
                                    [FromForm] string date_to,
                                    [FromForm] string branch,
                                    [FromForm] string isTrans,
                                    [FromForm] string user_id,
                                    [FromForm] string showRaseedType,
                                    [FromForm] string dontShowSubAccounts,
                                    [FromForm] string dontShowNotActiveAccounts,
                                    [FromForm] string arrangeBy,
                                    [FromForm] string level,
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

            var RaseedType = "";
            switch (showRaseedType)
            {
                case "1":
                    RaseedType = " AND (rassed > 0) ";
                    break;
                case "2":
                    RaseedType = " AND (rassed < 0) ";
                    break;
                case "3":
                    RaseedType = " AND (rassed <> 0) ";
                    break;
                default:
                    RaseedType = "";
                    break;
            }

            var dontShowSubAccounts_var = "";
            if (dontShowSubAccounts == "1")
            {
                dontShowSubAccounts_var = " AND (Accounts_IsMain = 1) AND (level = 0) ";
            }

            var dontShowNotActiveAccounts_var = "";
            if (dontShowNotActiveAccounts == "1")
            {
                dontShowNotActiveAccounts = " AND (Accounts_IsNotActive = 0 OR Accounts_IsNotActive IS NULL) ";
            }

            
            var level_var = "";
            if (level != "0")
            {
                level_var = " AND (level < " + level + ") ";
            }

            var account_var = account == null ? "" : account;
            var date_f = date_from == "0" || date_from == null? "" : date_from.ToString();
            var date_t = date_to == "0" || date_to == null ? "" : date_to.ToString();

            var sql = "exec AccountGetChildsRassed_Mezan " +
                                            "'" + account_var + "', " +
                                            "'" + date_f + "', " +
                                            "'" + date_t + "', " +
                                            "'" + branch.ToString() + "', " +
                                            "'" + isTrans.ToString() + "', " +
                                            "" + user_id.ToString() + "; " +
                    "SELECT * FROM AccountGetChildsRassed_All_TableMezan " +
                    "WHERE " +
                       "(SessionID = " + user_id.ToString() + ") " +
                       RaseedType + level_var + dontShowSubAccounts_var + dontShowNotActiveAccounts_var +
                       " ORDER BY  " + arrangeBy;

            var spResult = _context.MizanBalance
                            .FromSql(sql)
                            .ToList();


            /*
            sql = "SELECT * FROM AccountGetChildsRassed_All_Table " +
                   "WHERE " +
                       "(SessionID = " + user_id.ToString() + ") " +
                       showRaseedType.ToString() + dontShowSubAccounts.ToString() + dontShowNotActiveAccounts.ToString() +
                       " ORDER BY Sort  ";

            var spResult = _context.TrailBalance
                .FromSql(sql)
                .ToList();
                */

            return Ok(spResult);

        }

        [HttpPost]
        [Route("FinalBalance")]
        public IActionResult PostFinalBalance([FromForm] string account,
                                            [FromForm] string date_from,
                                            [FromForm] string date_to,
                                            [FromForm] string branch,
                                            [FromForm] string user_id,
                                            [FromForm] string level,
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
           
            var level_var = "";
            if (level != "0")
            {
                level_var = " AND (level < " + level + ") ";
            }

            var account_var = account == null ? "" : account;
            var date_f = date_from == "0" || date_from == null ? "" : date_from.ToString();
            var date_t = date_to == "0" || date_to == null ? "" : date_to.ToString();

            var isTrans = "";
            var dontShowSubAccounts_var = "";

            var sql = "declare @sss float; " +
                        "exec AccountGetChildsRassed_FinalAccountSheets " +
                                            "'" + account_var + "', " +
                                            "'" + date_f + "', " +
                                            "'" + date_t + "', " +
                                            "'" + branch.ToString() + "', " +
                                            "'" + isTrans.ToString() + "', " +
                                            "" + user_id.ToString() + ", " +
                                            "@sss output ; " +
                    "SELECT * FROM AccountGetChildsRassed_FinalAccountSheetsTable " +
                    "WHERE " +
                       "(SessionID = " + user_id.ToString() + ") " +
                       level_var + dontShowSubAccounts_var +
                       " ORDER BY Sort; ";

            var spResult = _context.AccountFinalBalance
                            .FromSql(sql)
                            .ToList();
            
            return Ok(spResult);

        }

        /*
        // DELETE: api/DetailsTrans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetailsTrans([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var detailsTrans = await _context.DetailsTrans.SingleOrDefaultAsync(m => m.ROWNUMBER1 == id);
            if (detailsTrans == null)
            {
                return NotFound();
            }

            _context.DetailsTrans.Remove(detailsTrans);
            await _context.SaveChangesAsync();

            return Ok(detailsTrans);
        }

        private bool DetailsTransExists(long id)
        {
            return _context.DetailsTrans.Any(e => e.ROWNUMBER1 == id);
        }
        */
    }
}