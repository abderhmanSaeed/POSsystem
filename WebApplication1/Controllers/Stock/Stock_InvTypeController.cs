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
    public class Stock_InvTypeController : ControllerBase
    {
        private readonly TodoContext _context;
        private string id_var = "admin";
        private string pass_var = "123";

        public Stock_InvTypeController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Stock_InvType
        [HttpGet]
        public IActionResult GetStock_InvType([FromQuery] string api_id, [FromQuery] string pass)
        {
            if ((api_id == id_var) && (pass == pass_var))
            {
                return Ok(_context.Stock_InvType);
            }
            else
            {
                return BadRequest(ModelState);
            }
            //return _context.Stock_InvType;
        }
        
        // GET: api/Stock_InvType
        [HttpGet]
        [Route("GetStock_InvTypeMove")]
        public ActionResult GetStock_InvTypeMove([FromQuery] string api_id, [FromQuery] string pass)
        {
            if ((api_id != id_var) || (pass != pass_var))
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stock_InvTypeList = _context.Stock_InvType.Where(x => x.InvType_IsMoveInv == true).ToList();

            if (stock_InvTypeList == null)
            {
                return NotFound();
            }

            return Ok(stock_InvTypeList);
        }

        // GET: api/Stock_InvType/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStock_InvType([FromRoute] int id,
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

            var stock_InvType = await _context.Stock_InvType.FindAsync(id);

            if (stock_InvType == null)
            {
                return NotFound();
            }

            return Ok(stock_InvType);
        }

        // PUT: api/Stock_InvType/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStock_InvType([FromRoute] int id, [FromForm] string stock_InvType,
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

            Stock_InvType stock_InvType_arr = JsonConvert.DeserializeObject<Stock_InvType>(stock_InvType);

            if (id != stock_InvType_arr.InvType_id)
            {
                return BadRequest();
            }

            _context.Entry(stock_InvType_arr).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Stock_InvTypeExists(id))
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

        // POST: api/Stock_InvType
        [HttpPost]
        public async Task<IActionResult> PostStock_InvType([FromForm] string stock_InvType,
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

            Stock_InvType stock_InvType_arr = JsonConvert.DeserializeObject<Stock_InvType>(stock_InvType);
           
            _context.Stock_InvType.Add(stock_InvType_arr);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }
            

            return CreatedAtAction("GetStock_InvType", new { id = stock_InvType_arr.InvType_id }, stock_InvType_arr);
        }

        // DELETE: api/Stock_InvType/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock_InvType([FromRoute] int id,
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

            var stock_InvType = await _context.Stock_InvType.FindAsync(id);
            if (stock_InvType == null)
            {
                return NotFound();
            }

            _context.Stock_InvType.Remove(stock_InvType);
            await _context.SaveChangesAsync();

            return Ok(stock_InvType);
        }

        private bool Stock_InvTypeExists(int id)
        {
            return _context.Stock_InvType.Any(e => e.InvType_id == id);
        }
    }
}