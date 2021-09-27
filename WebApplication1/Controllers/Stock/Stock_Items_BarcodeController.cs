using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Models.Stock;

namespace WebApplication1.Controllers.Stock
{
    [Route("api/[controller]")]
    [ApiController]
    public class Stock_Items_BarcodeController : ControllerBase
    {
        private readonly TodoContext _context;
        private string id_var = "admin";
        private string pass_var = "123";

        public Stock_Items_BarcodeController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Stock_Items_Barcode
        [HttpGet]
        public IActionResult GetStock_Items_Barcode([FromQuery] string api_id, [FromQuery] string pass)
        {
            if ((api_id == id_var) && (pass == pass_var))
            {
                return Ok(_context.Stock_Items_Barcode);
            }
            else
            {
                return BadRequest(ModelState);
            }
            //return _context.Stock_Items_Barcode;
        }

        // GET: api/Stock_Items_Barcode/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStock_Items_Barcode([FromRoute] long id,
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

            var stock_Items_Barcode = await _context.Stock_Items_Barcode.FindAsync(id);

            if (stock_Items_Barcode == null)
            {
                return NotFound();
            }

            return Ok(stock_Items_Barcode);
        }

        // GET: api/Stock_Items_Barcode/Stock_Items_BarcodeByBarcode/5
        [HttpGet]
        [Route("Stock_Items_BarcodeByBarcode/{id}")]
        public IActionResult GetStock_Items_BarcodeByBarcode([FromRoute] string id,
                                                    [FromQuery] string Branch, [FromQuery] string ShopCard_id, 
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

            var Branch11 = Branch != null ? "'" + Branch.ToString() + "'" : "NULL";
            var ShopCard_id11 = ShopCard_id != null ? "'" + ShopCard_id.ToString() + "'" : "NULL";

            var sql = "SELECT     Stock_Items_Barcode.Barcode_Id, Stock_Items_Barcode.Item_Id, Stock_Items_Barcode.Barcode_Price,  " +
                        "Stock_Items_Barcode.Barcode_PurchasePrice, Stock_Items_Barcode.Stock_Units_id,  " +
                        "Stock_Items_Barcode.Quntity, Stock_Items_Barcode.Stock_ItemTypeAndSize_Id,  " +
                        "Stock_Items_Barcode.Stock_ItemTypeAndGroups_Details_id, Stock_items.Items_Discount, " +
                        "Stock_items.Stock_items_tax, ISNULL(Stock_items.Items_IsUnderLot, 0) AS Items_IsUnderLot, " +
                        "dbo.Stock_GetItemUnitListArr_func_Json(Stock_Items_Barcode.Item_Id) AS ItemUnitList, " + 
                        "dbo.Stock_GetItemOfferByBarcode(Stock_Items_Barcode.Barcode_Id, datepart(hour, getdate()), " + Branch11 + ") AS OfferPrice, " +
                        "dbo.GetItemStockByItemId(Stock_items.Items_id, " + ShopCard_id11 + ", ISNULL(Stock_items.Items_IsUnderLot, 0), Stock_ItemTypeAndSize_Id, Stock_ItemTypeAndGroups_Details_id, NULL, NULL, NULL) AS ItemStock, " +
                        "Stock_items.Items_AgentId " +
                      "FROM         Stock_Items_Barcode INNER JOIN " +
                            "Stock_items ON Stock_Items_Barcode.Item_Id = Stock_items.Items_id " +
                      "WHERE(Stock_Items_Barcode.Barcode_Id = N'" + id.ToString() + "')";

          //  var spResult1 = _context.Stock_InvTransSearchByBarcode
            var spResult1 = _context.Stock_InvTransSearchByBarcode.FromSql(sql).ToList();

            //var stock_Items_Barcode = await _context.Stock_Items_Barcode.FindAsync(id);

            if (spResult1 == null)
            {
                return NotFound();
            }

            return Ok(spResult1);
        }

        // PUT: api/Stock_Items_Barcode/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStock_Items_Barcode([FromRoute] long id, 
                                                        [FromBody] Stock_Items_Barcode stock_Items_Barcode,
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

            if (id != stock_Items_Barcode.id)
            {
                return BadRequest();
            }

            _context.Entry(stock_Items_Barcode).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Stock_Items_BarcodeExists(id))
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

        // POST: api/Stock_Items_Barcode
        [HttpPost]
        public async Task<IActionResult> PostStock_Items_Barcode([FromBody] Stock_Items_Barcode stock_Items_Barcode,
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

            _context.Stock_Items_Barcode.Add(stock_Items_Barcode);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStock_Items_Barcode", new { id = stock_Items_Barcode.id }, stock_Items_Barcode);
        }

        // DELETE: api/Stock_Items_Barcode/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock_Items_Barcode([FromRoute] long id,
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

            var stock_Items_Barcode = await _context.Stock_Items_Barcode.FindAsync(id);
            if (stock_Items_Barcode == null)
            {
                return NotFound();
            }

            _context.Stock_Items_Barcode.Remove(stock_Items_Barcode);
            await _context.SaveChangesAsync();

            return Ok(stock_Items_Barcode);
        }

        private bool Stock_Items_BarcodeExists(long id)
        {
            return _context.Stock_Items_Barcode.Any(e => e.id == id);
        }
    }
}