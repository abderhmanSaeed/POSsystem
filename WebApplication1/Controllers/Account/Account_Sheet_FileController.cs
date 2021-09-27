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
    [Route("api/Account_Sheet_File")]
    public class Account_Sheet_FileController : Controller
    {
        private readonly TodoContext _context;
        private string id_var = "admin";
        private string pass_var = "123";

        public Account_Sheet_FileController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Account_Sheet_File
        [HttpGet]
        public IActionResult GetAccount_Sheet_File([FromQuery] string api_id, [FromQuery] string pass)
        {
            if ((api_id == id_var) && (pass == pass_var))
            {
                return Ok(_context.Account_Sheet_File);
            }
            else
            {
                return BadRequest(ModelState);
            }
            //return _context.Account_Sheet_File;
        }

        // GET: api/Account_Sheet_File/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccount_Sheet_File([FromRoute] long id,
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

            var account_Sheet_File = await _context.Account_Sheet_File.SingleOrDefaultAsync(m => m.Account_Sheet_File_id == id);

            if (account_Sheet_File == null)
            {
                return NotFound();
            }

            return Ok(account_Sheet_File);
        }

        // PUT: api/Account_Sheet_File/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount_Sheet_File([FromRoute] long id, 
                                                            [FromForm] Account_Sheet_File account_Sheet_Files,
                                                            [FromQuery] string api_id,
                                                            [FromQuery] string pass)
        {
            if ((api_id != id_var) || (pass != pass_var))
            {
                return BadRequest(ModelState);
            }
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            if (id != account_Sheet_Files.Account_Sheet_File_id)
            {
                return BadRequest();
            }

            _context.Entry(account_Sheet_Files).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Account_Sheet_FileExists(id))
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

        // POST: api/Account_Sheet_File
        [HttpPost]
        public async Task<IActionResult> PostAccount_Sheet_File([FromForm] Account_Sheet_File account_Sheet_File,
                                                                [FromQuery] string api_id,
                                                                [FromQuery] string pass)
        {
            if ((api_id != id_var) || (pass != pass_var))
            {
                return BadRequest(ModelState);
            }
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            _context.Account_Sheet_File.Add(account_Sheet_File);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccount_Sheet_File", new { id = account_Sheet_File.Account_Sheet_File_id }, account_Sheet_File);
        }

        // DELETE: api/Account_Sheet_File/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount_Sheet_File([FromRoute] long id,
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

            var account_Sheet_File = await _context.Account_Sheet_File.SingleOrDefaultAsync(m => m.Account_Sheet_File_id == id);
            if (account_Sheet_File == null)
            {
                return NotFound();
            }

            _context.Account_Sheet_File.Remove(account_Sheet_File);
            await _context.SaveChangesAsync();

            return Ok(account_Sheet_File);
        }

        private bool Account_Sheet_FileExists(long id)
        {
            return _context.Account_Sheet_File.Any(e => e.Account_Sheet_File_id == id);
        }
        
    }
}