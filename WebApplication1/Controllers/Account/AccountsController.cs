using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using Newtonsoft.Json.Linq;

namespace WebApplication1.Models
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class AccountsController : Controller
    {
        private readonly TodoContext _context;
        private string id_var = "admin";
        private string pass_var = "123";

        public AccountsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Accounts
        [HttpGet]
        [Route("{Accounts_FatherId}")]
        public IActionResult GetAccounts(long Accounts_FatherId,[FromQuery] string api_id, [FromQuery] string pass, [FromQuery] bool isMain = false)
        {
            if ((api_id == id_var) && (pass == pass_var))
            {
                return Ok(_context.Accounts.Where(m => m.Accounts_IsMain == isMain && m.Accounts_FatherId==Accounts_FatherId).OrderBy(x => x.Accounts_Name));
            }
            else
            {
                return BadRequest(ModelState);
            }
            //return _context.Accounts;
        }


        // GET: api/AllAccounts
        [HttpGet]
        [Route("AllAccounts")]
        public IActionResult GetAllAccounts([FromQuery] string api_id, [FromQuery] string pass)
        {
            if ((api_id == id_var) && (pass == pass_var))
            {
                return Ok(_context.Accounts.OrderBy(x => x.Accounts_Name));
            }
            else
            {
                return BadRequest(ModelState);
            }
            //return _context.Accounts;
        }


        // GET: api/Accounts/5
        [HttpGet]
        [Route("AccountById/{id}")]
        public async Task<IActionResult> GetAccountsById([FromRoute] long id, 
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

            var accounts = await _context.Accounts.SingleOrDefaultAsync(m => m.Accounts_Id == id);

            if (accounts == null)
            {
                return NotFound();
            }

            return Ok(accounts);
        }

        // GET: api/Accounts/5
        //Search---------------------
        [HttpGet]
        [Route("AccountByFatherId/{id}")]
        public IActionResult AccountByFatherId([FromRoute] long id,
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

            //var accounts[] = await _context.Accounts.Where(m => m.Accounts_FatherId == id);
            var accounts = _context.Accounts.Where(m => m.Accounts_FatherId == id).OrderBy(x => x.Accounts_Name);

            if (accounts == null)
            {
                return NotFound();
            }

            return Ok(accounts);
        }

        // PUT: api/Accounts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccounts([FromRoute] long id, 
                                                    [FromBody] Account accounts,
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

            if (id != accounts.Accounts_Id)
            {
                return BadRequest();
            }

            _context.Entry(accounts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(accounts);
        }
        // POST: api/Accounts
        [HttpPost]
        public IActionResult PostAccounts( [FromBody] Account accounts,
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

            _context.Accounts.Add(accounts);
             _context.SaveChanges();

            return CreatedAtAction("GetAllAccounts", new { id = accounts.Accounts_Id }, accounts);
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccounts([FromRoute] long id,
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

            var accounts = await _context.Accounts.SingleOrDefaultAsync(m => m.Accounts_Id == id);
            if (accounts == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(accounts);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool AccountsExists(long id)
        {
            return _context.Accounts.Any(e => e.Accounts_Id == id);
        }
        [Route("NewAccount")]
        [HttpPost]
        public IActionResult NewAccounts(Account account)
        {
            _context.Accounts.Add(account);
            _context.SaveChanges();
            return Ok(account);
          //  CreatedAtAction("GetAccountsById", new { id = account.Accounts_Id }, account);
        }

    }
}