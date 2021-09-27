using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    [Produces("application/json")]
    [Route("api/Branshes")]
    public class BranshesController : Controller
    {
        private readonly TodoContext _context;
        private string id_var = "admin";
        private string pass_var = "123";

        public BranshesController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Branshes
        [HttpGet]
        public IActionResult GetBransh([FromQuery] string api_id, [FromQuery] string pass)
        {
            if ((api_id == id_var) && (pass == pass_var))
            {
                return Ok(_context.Bransh);
            }
            else
            {
                return BadRequest(ModelState);
            }
            //return _context.Bransh;
        }

        // GET: api/Branshes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBransh([FromRoute] int id,
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

            var bransh = await _context.Bransh.SingleOrDefaultAsync(m => m.Bransh_id == id);

            if (bransh == null)
            {
                return NotFound();
            }

            return Ok(bransh);
        }

        // PUT: api/Branshes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBransh([FromRoute] int id, 
                                                    [FromForm] Bransh bransh,
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

            if (id != bransh.Bransh_id)
            {
                return BadRequest();
            }

            _context.Entry(bransh).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BranshExists(id))
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

        // POST: api/Branshes
        [HttpPost]
        public async Task<IActionResult> PostBransh([FromForm] Bransh bransh,
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

            _context.Bransh.Add(bransh);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBransh", new { id = bransh.Bransh_id }, bransh);
        }

        // DELETE: api/Branshes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBransh([FromRoute] int id,
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

            var bransh = await _context.Bransh.SingleOrDefaultAsync(m => m.Bransh_id == id);
            if (bransh == null)
            {
                return NotFound();
            }

            _context.Bransh.Remove(bransh);
            await _context.SaveChangesAsync();

            return Ok(bransh);
        }

        private bool BranshExists(int id)
        {
            return _context.Bransh.Any(e => e.Bransh_id == id);
        }
        
    }
}