using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class payments_typesController : ControllerBase
    {
        private readonly TodoContext _context;
        private string id_var = "admin";
        private string pass_var = "123";

        public payments_typesController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/payments_types
        [HttpGet]
        public IActionResult Getpayments_types([FromQuery] string api_id, [FromQuery] string pass)
        {
            if ((api_id == id_var) && (pass == pass_var))
            {
                return Ok(_context.payments_types.ToList());
            }
            else
            {
                return BadRequest(ModelState);
            }
           // return _context.payments_types;
        }

        // GET: api/payments_types/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Getpayments_types([FromRoute] int id,
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

            var payments_types = await _context.payments_types.FindAsync(id);

            if (payments_types == null)
            {
                return NotFound();
            }

            return Ok(payments_types);
        }

        // PUT: api/payments_types/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Putpayments_types([FromRoute] int id, [FromBody] payments_types payments_types,
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

            if (id != payments_types.payments_types_id)
            {
                return BadRequest();
            }

            _context.Entry(payments_types).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!payments_typesExists(id))
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

        // POST: api/payments_types
        [HttpPost]
        public async Task<IActionResult> Postpayments_types([FromBody] payments_types payments_types,
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

            _context.payments_types.Add(payments_types);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getpayments_types", new { id = payments_types.payments_types_id }, payments_types);
        }

        // DELETE: api/payments_types/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletepayments_types([FromRoute] int id,
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

            var payments_types = await _context.payments_types.FindAsync(id);
            if (payments_types == null)
            {
                return NotFound();
            }

            _context.payments_types.Remove(payments_types);
            await _context.SaveChangesAsync();

            return Ok(payments_types);
        }

        private bool payments_typesExists(int id)
        {
            return _context.payments_types.Any(e => e.payments_types_id == id);
        }
    }
}