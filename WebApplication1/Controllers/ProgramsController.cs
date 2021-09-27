using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramsController : ControllerBase
    {
        private readonly TodoContext _context;
        private string id_var = "admin";
        private string pass_var = "123";

        public ProgramsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Programs
        [HttpGet]
        public IActionResult GetPrograms([FromQuery] string api_id, [FromQuery] string pass)
        {
            if ((api_id == id_var) && (pass == pass_var))
            {
                return Ok(_context.Programs.Include(x => x.forms));
            }
            else
            {
                return BadRequest(ModelState);
            }
            //return _context.Bransh;
        }

        // GET: api/Programs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPrograms(int id, [FromQuery] string api_id, [FromQuery] string pass)
        {
            if ((api_id != id_var) || (pass != pass_var))
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var db = await _context.Programs.Include(x => x.forms).SingleOrDefaultAsync(m => m.prog_id == id);

            if (db == null)
            {
                return NotFound();
            }

            return Ok(db);
        }
    }
}
