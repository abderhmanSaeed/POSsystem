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
    public class Stock_UnitsController : ControllerBase
    {
        //gjhgjh
        private readonly TodoContext _context;

        private string id_var = "admin";
        private string pass_var = "123";

        public Stock_UnitsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Stock_Units
        [HttpGet]
        public IActionResult GetStock_Units([FromQuery] string api_id, [FromQuery] string pass)
        {
            if ((api_id == id_var) && (pass == pass_var))
            {
                return Ok(_context.Stock_Units);
            }
            else
            {
                return BadRequest(ModelState);
            }
            //return _context.Stock_Units;
        }

        // GET: api/Stock_Units/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStock_Units([FromRoute] int id,
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

            var stock_Units = await _context.Stock_Units.FindAsync(id);

            if (stock_Units == null)
            {
                return NotFound();
            }

            return Ok(stock_Units);
        }

        // PUT: api/Stock_Units/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStock_Units([FromRoute] int id, [FromForm] Stock_Units stock_Units,
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

            if (id != stock_Units.Stock_Units_id)
            {
                return BadRequest();
            }

            _context.Entry(stock_Units).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Stock_UnitsExists(id))
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

        // POST: api/Stock_Units
        [HttpPost]
        public async Task<IActionResult> PostStock_Units([FromForm] Stock_Units stock_Units,
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

            _context.Stock_Units.Add(stock_Units);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStock_Units", new { id = stock_Units.Stock_Units_id }, stock_Units);
        }

        // DELETE: api/Stock_Units/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock_Units([FromRoute] int id,
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

            var stock_Units = await _context.Stock_Units.FindAsync(id);
            if (stock_Units == null)
            {
                return NotFound();
            }

            _context.Stock_Units.Remove(stock_Units);
            await _context.SaveChangesAsync();

            return Ok(stock_Units);
        }

        private bool Stock_UnitsExists(int id)
        {
            return _context.Stock_Units.Any(e => e.Stock_Units_id == id);
        }
    }
}