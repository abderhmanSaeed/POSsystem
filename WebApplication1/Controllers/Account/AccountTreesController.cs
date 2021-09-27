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
    [Route("api/AccountTrees")]
    public class AccountTreesController : Controller
    {
        private readonly TodoContext _context;
        private string id_var = "admin";
        private string pass_var = "123";

        public AccountTreesController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/AccountTrees
        [HttpGet]
        public IActionResult GetAccountTree([FromQuery] string api_id, [FromQuery] string pass)
        {
            if ((api_id == id_var) && (pass == pass_var))
            {
                return Ok(_context.AccountTree.Include(e => e.Children).ToList());
            }
            else
            {
                return BadRequest(ModelState);
            }
            //return _context.AccountTree;
        }
    }
}