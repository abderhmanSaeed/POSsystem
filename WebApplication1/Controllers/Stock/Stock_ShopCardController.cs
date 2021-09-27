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
    public class Stock_ShopCardController : ControllerBase
    {
        private readonly TodoContext _context;
        private string id_var = "admin";
        private string pass_var = "123";

        public Stock_ShopCardController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Stock_ShopCard
        [HttpGet]
        public IActionResult GetStock_ShopCard([FromQuery] string api_id, [FromQuery] string pass)
        {
            if ((api_id == id_var) && (pass == pass_var))
            {
                return Ok(_context.Stock_ShopCard);
            }
            else
            {
                return BadRequest(ModelState);
            }
            //return _context.Stock_ShopCard;
        }

        // GET: api/Stock_ShopCard/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStock_ShopCard([FromRoute] int id,
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

            var stock_ShopCard = await _context.Stock_ShopCard.FindAsync(id);

            if (stock_ShopCard == null)
            {
                return NotFound();
            }

            return Ok(stock_ShopCard);
        }

        // PUT: api/Stock_ShopCard/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStock_ShopCard([FromRoute] int id, [FromForm] Stock_ShopCard stock_ShopCard,
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

            if (id != stock_ShopCard.Stock_ShopCard_Id)
            {
                return BadRequest();
            }

            _context.Entry(stock_ShopCard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Stock_ShopCardExists(id))
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

        // POST: api/Stock_ShopCard
        [HttpPost]
        public async Task<IActionResult> PostStock_ShopCard([FromForm] Stock_ShopCard stock_ShopCard,
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

            _context.Stock_ShopCard.Add(stock_ShopCard);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStock_ShopCard", new { id = stock_ShopCard.Stock_ShopCard_Id }, stock_ShopCard);
        }

        // DELETE: api/Stock_ShopCard/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock_ShopCard([FromRoute] int id,
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

            var stock_ShopCard = await _context.Stock_ShopCard.FindAsync(id);
            if (stock_ShopCard == null)
            {
                return NotFound();
            }

            _context.Stock_ShopCard.Remove(stock_ShopCard);
            await _context.SaveChangesAsync();

            return Ok(stock_ShopCard);
        }

        private bool Stock_ShopCardExists(int id)
        {
            return _context.Stock_ShopCard.Any(e => e.Stock_ShopCard_Id == id);
        }
    }
}