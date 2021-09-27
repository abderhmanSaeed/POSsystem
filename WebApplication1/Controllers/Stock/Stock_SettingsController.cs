using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers.Stock
{
    [Route("api/[controller]")]
    [ApiController]
    public class Stock_SettingsController : ControllerBase
    {
        private readonly TodoContext _context;
        private string id_var = "admin";
        private string pass_var = "123";

        public Stock_SettingsController(TodoContext context)
        {
            _context = context;
        }
        [HttpGet]
        //   [Route("Getprogram_proprties/{id}")]
        public IActionResult GetStcok_Setting([FromQuery] string api_id, [FromQuery] string pass)
        {
            if ((api_id == id_var) && (pass == pass_var))
            {
                var Obj = _context.Stock_Settings.ToList();
                return Ok(Obj);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
