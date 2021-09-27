using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Models.Stock;

namespace WebApplication1.Models.Stock
{
    [Route("api/[controller]")]
    [ApiController]
    public class Stock_ItemsGroupController : ControllerBase
    {
        private readonly TodoContext _context;
        private string id_var = "admin";
        private string pass_var = "123";

        public Stock_ItemsGroupController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Stock_ItemsGroup
        [HttpGet]
        public IActionResult GetStock_ItemsGroup([FromQuery] string api_id, [FromQuery] string pass)
        {
            if ((api_id == id_var) && (pass == pass_var))
            {
                return Ok(_context.Stock_ItemsGroup.Include(d => d.Stock_ItemsGroup_Details).ToList());
            }
            else
            {
                return BadRequest(ModelState);
            }
            //return _context.Stock_ItemsGroup;
        }

        // GET: api/Stock_ItemsGroup/5
        [HttpGet("{id}")]
        public ActionResult GetStock_ItemsGroup([FromRoute] long id,
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

            //var stock_ItemsGroup = await _context.Stock_ItemsGroup.FindAsync(id);
            var stock_ItemsGroup = _context.Stock_ItemsGroup.Where(x => x.Stock_ItemsGroup_id == id)
                                                    .Include(d => d.Stock_ItemsGroup_Details).ToList();

                        //.Include(d => d.Stock_ItemsGroup_Details).ToList();
                    //Include(d => d.Stock_ItemsGroup_Details).ToList();

            if (stock_ItemsGroup == null)
            {
                return NotFound();
            }

            return Ok(stock_ItemsGroup);
        }

        // PUT: api/Stock_ItemsGroup/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStock_ItemsGroup([FromRoute] long id, [FromBody] Stock_ItemsGroup stock_ItemsGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stock_ItemsGroup.Stock_ItemsGroup_id)
            {
                return BadRequest();
            }

            _context.Entry(stock_ItemsGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Stock_ItemsGroupExists(id))
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

        // POST: api/Stock_ItemsGroup
        [HttpPost]
        public async Task<IActionResult> PostStock_ItemsGroup([FromBody] Stock_ItemsGroup stock_ItemsGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Stock_ItemsGroup.Add(stock_ItemsGroup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStock_ItemsGroup", new { id = stock_ItemsGroup.Stock_ItemsGroup_id }, stock_ItemsGroup);
        }

        // DELETE: api/Stock_ItemsGroup/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock_ItemsGroup([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stock_ItemsGroup = await _context.Stock_ItemsGroup.FindAsync(id);
            if (stock_ItemsGroup == null)
            {
                return NotFound();
            }

            _context.Stock_ItemsGroup.Remove(stock_ItemsGroup);
            await _context.SaveChangesAsync();

            return Ok(stock_ItemsGroup);
        }

        private bool Stock_ItemsGroupExists(long id)
        {
            return _context.Stock_ItemsGroup.Any(e => e.Stock_ItemsGroup_id == id);
        }
    }
}