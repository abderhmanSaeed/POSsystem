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
    [Route("api/Account_Sheet_Detail")]
    public class Account_Sheet_DetailController : Controller
    {
        private readonly TodoContext _context;
        private string id_var = "admin";
        private string pass_var = "123";

        public Account_Sheet_DetailController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Account_Sheet_Detail
        /*
        [HttpGet]
        public IActionResult GetAccount_Sheet_Details([FromQuery] string api_id, [FromQuery] string pass)
        {
            if ((api_id == id_var) && (pass == pass_var))
            {
                return Ok(_context.Account_Sheet_Details);
            }
            else
            {
                return BadRequest(ModelState);
            }
            //return _context.Account_Sheet_Details;
        }
        */
        // GET: api/Account_Sheet_Detail/5
        [HttpGet("{id}")]
        public IActionResult GetAccount_Sheet_Detail([FromRoute] long id,
                                                    [FromQuery] string api_id,
                                                    [FromQuery] string pass,
                                                    [FromQuery] int getClosedDetails = 1)
        {
            if ((api_id != id_var) || (pass != pass_var))
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Object account_Sheet_Detail = null;

            if (getClosedDetails == 0)
            {
                account_Sheet_Detail = _context.Account_Sheet_Details.Where
                                    (m =>
                                        (m.Account_Sheet_id == id) &&
                                        (m.Account_Sheet_Details_IsClosedDetails == false)
                                     );
            }
            else
            {
                account_Sheet_Detail = _context.Account_Sheet_Details.Where
                                    (m => m.Account_Sheet_id == id);
            }

            if (account_Sheet_Detail == null)
            {
                return NotFound();
            }

            return Ok(account_Sheet_Detail);
        }

        /*

        // PUT: api/Account_Sheet_Detail/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount_Sheet_Detail([FromRoute] long id, [FromBody] Account_Sheet_Detail account_Sheet_Detail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != account_Sheet_Detail.Account_Sheet_Details_id2)
            {
                return BadRequest();
            }

            _context.Entry(account_Sheet_Detail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Account_Sheet_DetailExists(id))
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

        // POST: api/Account_Sheet_Detail
        [HttpPost]
        public async Task<IActionResult> PostAccount_Sheet_Detail([FromBody] Account_Sheet_Detail account_Sheet_Detail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Account_Sheet_Details.Add(account_Sheet_Detail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccount_Sheet_Detail", new { id = account_Sheet_Detail.Account_Sheet_Details_id2 }, account_Sheet_Detail);
        }

        // DELETE: api/Account_Sheet_Detail/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount_Sheet_Detail([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var account_Sheet_Detail = await _context.Account_Sheet_Details.SingleOrDefaultAsync(m => m.Account_Sheet_Details_id2 == id);
            if (account_Sheet_Detail == null)
            {
                return NotFound();
            }

            _context.Account_Sheet_Details.Remove(account_Sheet_Detail);
            await _context.SaveChangesAsync();

            return Ok(account_Sheet_Detail);
        }

        private bool Account_Sheet_DetailExists(long id)
        {
            return _context.Account_Sheet_Details.Any(e => e.Account_Sheet_Details_id2 == id);
        }*/
    }
}