using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabasesController : ControllerBase
    {
        private readonly DbConfigContext _context;
        private string id_var = "admin";
        private string pass_var = "123";

        public DatabasesController(DbConfigContext context)
        {
            _context = context;
        }

        // GET: api/Databases/5
        [HttpGet("{name}")]
        public async Task<IActionResult> GetDatabase([FromRoute] string name,
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

            var db = await _context.Databases.SingleOrDefaultAsync(m => m.Note == name);

            if (db == null)
            {
                return NotFound();
            }

            return Ok(db);
        }
    }
}
