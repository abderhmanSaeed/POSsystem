using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace WebApplication1.Models
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class Account_SheetController : Controller
    {
        private readonly TodoContext _context;
        private string id_var = "admin";
        private string pass_var = "123";

        public Account_SheetController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Account_Sheet
        [HttpGet]
        public IActionResult GetAccount_Sheet([FromQuery] string api_id, [FromQuery] string pass, [FromQuery] string BookIdVar = "0")
        {
            if ((api_id == id_var) && (pass == pass_var)){
                int dd = int.Parse(BookIdVar);
                if (dd != 0)
                {
                    return Ok(_context.Account_Sheet.Where(m => m.Account_Sheet_File_id == dd));
                }
                else
                {
                    return Ok(_context.Account_Sheet.OrderBy(x => x.Account_Sheet_id));
                }                
            }
            else
            {
                return BadRequest(ModelState);
            }
            //return _context.Account_Sheet;
        }

        // GET: api/Account_Sheet/5
        [HttpGet("{id}")]
        public IActionResult GetAccount_Sheet([FromRoute] long id, [FromQuery] string api_id, [FromQuery] string pass)
        {
            if ((api_id != id_var) || (pass != pass_var))
            {            
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var account_Sheet = _context.Account_Sheet.Include(z => z.Account_Sheet_Details).Where(x => x.Account_Sheet_id == id);

            if (account_Sheet == null)
            {
                return NotFound();
            }

            return Ok(account_Sheet);
        }

        // PUT: api/Account_Sheet/5
        /*
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount_Sheet([FromRoute] long id, 
                                                          [FromBody] Account_Sheet account_Sheet, 
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

            if (id != account_Sheet.Account_Sheet_id)
            {
                return BadRequest();
            }

            _context.Entry(account_Sheet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Account_SheetExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }*/

        // PUT: api/Account_Sheet/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount_Sheet([FromRoute] long id,
                                                            [FromForm] Account_Sheet_Detail[] account_Sheet_Detail,
                                                         [FromForm] Account_Sheet account_Sheet,
                                                         [FromQuery] string api_id, [FromQuery] string pass)
        {
            if ((api_id != id_var) || (pass != pass_var))
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Stock_ItemsGroup stock_ItemsGroup = new Stock_ItemsGroup();
            //stock_ItemsGroup.Stock_ItemsGroup_id = id;
            //stock_ItemsGroup.Stock_ItemsGroup_desc = GroupDesc;

            _context.Entry(account_Sheet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                await AddDetailsAsync(id, account_Sheet_Detail, api_id, pass);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Account_SheetExists(id))
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

        // POST: api/Account_Sheet
        [HttpPost]
        public IActionResult PostAccount_Sheet([FromForm] SpAddNewSheetPrams spAddNewSheetPrams,
                                                [FromQuery] string api_id,
                                                [FromQuery] string pass,
                                                [FromQuery] string TableName = "", 
                                                [FromQuery] string FieldIdName = "", 
                                                [FromQuery] string FieldSheetIdName = "")
        {
            if ((api_id != id_var) || (pass != pass_var))
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Account_Sheet_id = new SqlParameter("Account_Sheet_id", spAddNewSheetPrams.Account_Sheet_id);

            var date11 = spAddNewSheetPrams.Account_Sheet_Details_Date != "" ? "'" + spAddNewSheetPrams.Account_Sheet_Details_Date + "'" : "''";
            var Account_Sheet_File_id = spAddNewSheetPrams.Account_Sheet_File_id != 0 ? "'" + spAddNewSheetPrams.Account_Sheet_File_id + "'" : "NULL";

            var sql = "exec dbo.Account_AddNewSheet @Account_Sheet_id output, " +
                                spAddNewSheetPrams.Bransh_id + ", " +
                                "'" + spAddNewSheetPrams.Account_Sheet_date + "', " +
                                "'" + spAddNewSheetPrams.Account_Sheet_notes + "', " +
                                spAddNewSheetPrams.user_id + ", " +
                                "'" + spAddNewSheetPrams.Account_Sheet_Details_M + "', " +
                                "'" + spAddNewSheetPrams.Account_Sheet_Details_D + "', " +
                                "'" + spAddNewSheetPrams.Accounts_Id + "', " +
                                "'" + spAddNewSheetPrams.Account_Sheet_Details_Notes + "', " +
                                date11 + ", " +
                                "" + spAddNewSheetPrams.HasTazamonVar + ", " +
                                "'" + spAddNewSheetPrams.TazamonFirstCodeFormate + "', " +
                                "'" + spAddNewSheetPrams.Account_Sheet_FormId + "'," +
                                "'" + spAddNewSheetPrams.Account_Sheet_FormSerial + "'," +
                                "'" + spAddNewSheetPrams.Account_Sheet_FormControlName + "', " +
                                "" + spAddNewSheetPrams.BookFieldId + ", " +
                                "'" + spAddNewSheetPrams.OpenFormBookFuncName + "', " +
                                Account_Sheet_File_id + ", " +
                                spAddNewSheetPrams.Account_Sheet_File_Serial + ", " +
                                "'" + spAddNewSheetPrams.Account_Sheet_Details_IsClosedDetails + "';" +
                            " ";
            List<SpResult> spResult = null;
            try
            {
                spResult = _context.SpResult
                .FromSql(sql, Account_Sheet_id)
                .ToList();
            }        
            catch (Exception ex1)
            {
                throw;
            }
            

            if (spResult[0].Sheet_id != 0 && TableName != null)
            {
                try
                {
                    if (TableName != "")
                    {
                        string sql1 = "UPDATE  " + TableName + " " +
                                    "SET " + FieldSheetIdName + " = " + spResult[0].Sheet_id + " " +
                                    "WHERE (" + FieldIdName + " = " + spAddNewSheetPrams.BookFieldId.ToString() + ")";

                        int count = _context.Database.ExecuteSqlCommand(sql1);
                    }                    
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            return Ok(spResult);

        }

        // POST: api/Account_Sheet/Account_Sheet_json
        [HttpPost]
        [Route("Account_Sheet_json")]
        public async Task<IActionResult> PostAccount_Sheet_json([FromForm] Account_Sheet account_Sheet,
                                                         [FromForm] Account_Sheet_Detail[] account_Sheet_Detail,
                                                         [FromQuery] string api_id, [FromQuery] string pass)
        {
            if ((api_id != id_var) || (pass != pass_var))
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Add Items Group Main
            //Stock_ItemsGroup stock_ItemsGroup = new Stock_ItemsGroup();
            //stock_ItemsGroup.Stock_ItemsGroup_desc = GroupDesc;

            _context.Account_Sheet.Add(account_Sheet);
            await _context.SaveChangesAsync();
            long account_Sheet_id = account_Sheet.Account_Sheet_id;

            return await AddDetailsAsync(account_Sheet_id, account_Sheet_Detail, api_id, pass);

        }

        private async Task<IActionResult> AddDetailsAsync(long account_Sheet_id, Account_Sheet_Detail[] account_Sheet_Detail,
                                                         string api_id, string pass)
        {
            //Convert Json String To Object
            //Stock_ItemsGroup_Details[] stock_ItemsGroup_Details = JsonConvert.DeserializeObject<Stock_ItemsGroup_Details[]>(detailsGroup);

            //Loop Every Item In Details To Put The itemsGroup_id 
            for (int i = 0; i < account_Sheet_Detail.Length; i++)
            {
                account_Sheet_Detail[i].Account_Sheet_id = account_Sheet_id;
            }

            // 
            await DeleteAccount_Sheet(account_Sheet_id, api_id, pass, true);

            // Adding Details
            _context.Account_Sheet_Details.AddRange(account_Sheet_Detail);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetStock_ItemsGroup_Details", new { id = account_Sheet_Detail[0].Account_Sheet_id }, account_Sheet_Detail);
        }

        // DELETE: api/Account_Sheet/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount_Sheet([FromRoute] long id,
                                                [FromQuery] string api_id,
                                                [FromQuery] string pass,
                                                bool callFromAdd = false)
        {
            if ((api_id != id_var) || (pass != pass_var))
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var account_Sheet_Details = _context.Account_Sheet_Details.Where(m => m.Account_Sheet_id == id).ToList();
            if (account_Sheet_Details == null)
            {
                return NotFound();
            }

            _context.Account_Sheet_Details.RemoveRange(account_Sheet_Details);
            await _context.SaveChangesAsync();

            if (callFromAdd == false)
            {
                await DeleteAccount_Sheet2(id);
            }
            return Ok(account_Sheet_Details);
        }

        private async Task<IActionResult> DeleteAccount_Sheet2(long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var account_Sheet = _context.Account_Sheet.Where(x => x.Account_Sheet_id == id).ToList();
            // var stock_ItemsGroup_Details = await _context.Stock_ItemsGroup_Details.FindAsync(id);

            if (account_Sheet == null)
            {
                return NotFound();
            }

            _context.Account_Sheet.RemoveRange(account_Sheet);
            //_context.Stock_ItemsGroup_Details.Remove(stock_ItemsGroup_Details);
            await _context.SaveChangesAsync();

            return Ok(account_Sheet);
        }

        private bool Account_SheetExists(long id)
        {
            return _context.Account_Sheet.Any(e => e.Account_Sheet_id == id);
        }
    }
}