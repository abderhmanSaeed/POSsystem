using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Stock
{
    [Route("api/[controller]")]
    [ApiController]
    public class program_proprtiesController : ControllerBase
    {
        private readonly TodoContext _context;
        private string id_var = "admin";
        private string pass_var = "123";

        public program_proprtiesController(TodoContext context)
        {
            _context = context;
        }
        [HttpGet]
     //   [Route("Getprogram_proprties/{id}")]
        public IActionResult Getprogram_proprties([FromQuery] string api_id, [FromQuery] string pass)
        {
            if ((api_id == id_var) && (pass == pass_var))
            {
                var Obj= _context.program_Proprties.ToList();
                return Ok(Obj);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
