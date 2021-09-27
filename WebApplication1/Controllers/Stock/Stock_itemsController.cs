using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplication1.Models;
using WebApplication1.Models.Stock;

namespace WebApplication1.Controllers.Stock
{
    [Route("api/[controller]")]
    [ApiController]
    public class Stock_itemsController : ControllerBase
    {
        private readonly TodoContext _context;
        private string id_var = "admin";
        private string pass_var = "123";

        public Stock_itemsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Stock_items
        [HttpGet]
        public IActionResult GetStock_items([FromQuery] string api_id, [FromQuery] string pass )
        {
            if ((api_id == id_var) && (pass == pass_var))
            {
                return Ok(_context.Stock_items
                    //.Include(u => u.Stock_ItemsAndUnits)
                     //  .Include(b => b.Stock_Items_Barcode)
                       .Include(f => f.Father).OrderBy(order => order.Items_name_ar)
                      .ToList());

            }
            else
            {
                return BadRequest(ModelState);
            }
            //return _context.Stock_items;
        }
        // GET: api/Stock_items
        [Route("GetAllStockItemsWithIsMain/{Items_Flag}")]
        [HttpGet]
        public IActionResult GetAllStockItemsWithIsMain([FromQuery] string api_id, [FromQuery] string pass, int Items_Flag)
        {
            bool Items_is_main;
            if ((api_id == id_var) && (pass == pass_var))
            {
                if (Items_Flag == 1)
                {
                     Items_is_main = true;
                }
                else
                {
                    Items_is_main = false;
                }
               
                return Ok(_context.Stock_items.Where(a => a.Items_is_main == Items_is_main)
                                                .Include(b => b.Stock_Items_Barcode)
                                                .Include(f => f.Father).OrderBy(order => order.Items_name_ar).ToList());

            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // GET: api/Stock_items/Stock_ItemsAndUnits
        [HttpGet]
        [Route("Stock_ItemsAndUnits/{id}")]
        public IActionResult Stock_ItemsAndUnits([FromRoute] long id,
                                                 [FromQuery] string api_id, [FromQuery] string pass)
        {
            if ((api_id == id_var) && (pass == pass_var))
            {
                return Ok(_context.Stock_items.Where(x => x.Items_id == id));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // GET: api/Stock_items/GetStock_ItemDetailsById
        [HttpGet]
        [Route("GetStock_ItemDetailsById/{id}")]
        public IActionResult GetStock_ItemDetailsById([FromRoute] long id,
                                                 [FromQuery] string api_id, [FromQuery] string pass)
        {
            if ((api_id == id_var) && (pass == pass_var))
            {
                var stock_items = _context.Stock_items.Where(x => x.Items_id == id).Include(u => u.Stock_ItemsAndUnits).ToList();
                                                
                                               
                if (stock_items == null)
                {
                    return NotFound();
                }
                return Ok(stock_items);
            }
            else
            {
                return BadRequest(ModelState);
            }
            //return _context.Stock_items;
        }

        // GET: api/Stock_items/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStock_items([FromRoute] long id,
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

            var stock_items = await _context.Stock_items.FindAsync(id);
           

            if (stock_items == null)
            {
                return NotFound();
            }

            return Ok(stock_items);
        }

        // PUT: api/Stock_items/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStock_items([FromRoute] long id, [FromForm] string stock_items,
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

            Stock_items stock_items_arr = JsonConvert.DeserializeObject<Stock_items>(stock_items);

            if (id != stock_items_arr.Items_id)
            {
                return BadRequest();
            }

            _context.Entry(stock_items_arr).State = EntityState.Modified;           

            try
            {
                for (int i = 0; i < stock_items_arr.Stock_ItemsAndUnits.Count; i++)
                {
                    stock_items_arr.Stock_ItemsAndUnits[i].Stock_ItemsAndUnits_Id = 0;
                }
                for (int i = 0; i < stock_items_arr.Stock_Items_Barcode.Count; i++)
                {
                    stock_items_arr.Stock_Items_Barcode[i].id = 0;
                }
                await _context.SaveChangesAsync();
                await addUnits(stock_items_arr.Items_id, JsonConvert.SerializeObject(stock_items_arr.Stock_ItemsAndUnits));
                await addBarcode(stock_items_arr.Items_id, JsonConvert.SerializeObject(stock_items_arr.Stock_Items_Barcode));                                
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Stock_itemsExists(id))
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

        // POST: api/Stock_items
        [HttpPost]
        public async Task<IActionResult> PostStock_items([FromForm] string stock_items,
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

            Stock_items stock_items_arr = JsonConvert.DeserializeObject<Stock_items>(stock_items);
            stock_items_arr.total_price = stock_items_arr.Items_sell_price;

            _context.Stock_items.Add(stock_items_arr);            
            
            try
            {
                for (int i = 0; i < stock_items_arr.Stock_ItemsAndUnits.Count; i++)
                {
                    stock_items_arr.Stock_ItemsAndUnits[i].Stock_ItemsAndUnits_Id = 0;
                }
                for (int i = 0; i < stock_items_arr.Stock_Items_Barcode.Count; i++)
                {
                    stock_items_arr.Stock_Items_Barcode[i].id = 0;
                }
                await _context.SaveChangesAsync();
                await addUnits(stock_items_arr.Items_id, JsonConvert.SerializeObject(stock_items_arr.Stock_ItemsAndUnits));
                await addBarcode(stock_items_arr.Items_id, JsonConvert.SerializeObject(stock_items_arr.Stock_Items_Barcode));
            }
            catch (Exception ee)
            {
                string sss = ee.Message;
                throw;
            }

            return CreatedAtAction("GetStock_items", new { id = stock_items_arr.Items_id }, stock_items);
        }


        private async Task<IActionResult> addUnits(long item_id, string stock_ItemsAndUnits)
        {
            // Save Details
            Stock_ItemsAndUnits[] stock_ItemsAndUnits_arr = JsonConvert.DeserializeObject<Stock_ItemsAndUnits[]>(stock_ItemsAndUnits);
            for (int i = 0; i < stock_ItemsAndUnits_arr.Length; i++)
            {
                stock_ItemsAndUnits_arr[i].Items_id = item_id;
            }

            //await DeleteStock_InvMain(item_id, id_var, pass_var, true);
            var stock_ItemsAndUnits_del = _context.Stock_ItemsAndUnits.Where(x => x.Items_id == item_id).ToList();
            if (stock_ItemsAndUnits_del == null)
            {
                return NotFound();
            }

            _context.Stock_ItemsAndUnits.RemoveRange(stock_ItemsAndUnits_del);
            await _context.SaveChangesAsync();

            for (int i = 0; i < stock_ItemsAndUnits_arr.Length; i++)
            {
                stock_ItemsAndUnits_arr[i].Stock_ItemsAndUnits_Id = 0;
            }
            _context.Stock_ItemsAndUnits.AddRange(stock_ItemsAndUnits_arr);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStock_ItemsAndUnits", new { id = item_id }, stock_ItemsAndUnits_arr);
        }

        private async Task<IActionResult> addBarcode(long item_id, string stock_Items_Barcode)
        {
            // Save Details
            Stock_Items_Barcode[] stock_Items_Barcode_arr = JsonConvert.DeserializeObject<Stock_Items_Barcode[]>(stock_Items_Barcode);
            for (int i = 0; i < stock_Items_Barcode_arr.Length; i++)
            {
                stock_Items_Barcode_arr[i].Items_id = item_id;
            }

            //await DeleteStock_InvMain(item_id, id_var, pass_var, true);
            var stock_Items_Barcode_del = _context.Stock_Items_Barcode.Where(x => x.Items_id == item_id).ToList();
            if (stock_Items_Barcode_del == null)
            {
                return NotFound();
            }

            _context.Stock_Items_Barcode.RemoveRange(stock_Items_Barcode_del);
            await _context.SaveChangesAsync();

            for (int i = 0; i < stock_Items_Barcode_arr.Length; i++)
            {
                stock_Items_Barcode_arr[i].id = 0;
            }
            _context.Stock_Items_Barcode.AddRange(stock_Items_Barcode_arr);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction("GetStock_Items_Barcode", new { id = item_id }, stock_Items_Barcode_arr);
        }

        // DELETE: api/Stock_items/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock_items([FromRoute] long id,
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

            var stock_items = await _context.Stock_items.FindAsync(id);
            if (stock_items == null)
            {
                return NotFound();
            }

            _context.Stock_items.Remove(stock_items);
            try
            {
                var stock_ItemsAndUnits_del = _context.Stock_ItemsAndUnits.Where(x => x.Items_id == id).ToList();
                if (stock_ItemsAndUnits_del != null)
                {
                    _context.Stock_ItemsAndUnits.RemoveRange(stock_ItemsAndUnits_del);
                    await _context.SaveChangesAsync();
                }

                var stock_Items_Barcode_del = _context.Stock_Items_Barcode.Where(x => x.Items_id == id).ToList();
                if (stock_Items_Barcode_del != null)
                {
                    _context.Stock_Items_Barcode.RemoveRange(stock_Items_Barcode_del);
                    await _context.SaveChangesAsync();
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception eee)
            {

                throw;
            }
            

            return Ok(stock_items);
        }

        private bool Stock_itemsExists(long id)
        {
            return _context.Stock_items.Any(e => e.Items_id == id);
        }
    }
}