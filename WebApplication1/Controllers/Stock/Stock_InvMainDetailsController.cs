using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplication1.Models;
using WebApplication1.Models.Stock;

namespace WebApplication1.Controllers.Stock
{
    [Route("api/[controller]")]
    [ApiController]
    public class Stock_InvMainDetailsController : ControllerBase
    {
        private readonly TodoContext _context;
        private string id_var = "admin";
        private string pass_var = "123";

        public Stock_InvMainDetailsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Stock_InvMainDetails
        [HttpGet]
        public IActionResult GetStock_InvMainDetails([FromQuery] string api_id, [FromQuery] string pass)
        {
            if ((api_id == id_var) && (pass == pass_var))
            {
                return Ok(_context.Stock_InvMainDetails.Include(a=>a.Stock_Units).Include(a=>a.Stock_Items).ToList());
            }
            else
            {
                return BadRequest(ModelState);
            }
            //return _context.Stock_InvMainDetails;
        }

        // GET: api/Stock_InvMainDetails/5
        [HttpGet("{id}")]
        public  IActionResult GetStock_InvMainDetails([FromRoute] long id,
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

            var stock_InvMainDetails =  _context.Stock_InvMainDetails.Where(a=>a.Stock_InvMainDetails_id==id).Include(u=>u.Stock_Units).Include(a=>a.Stock_Items).FirstOrDefault();

            if (stock_InvMainDetails == null)
            {
                return NotFound();
            }

            return Ok(stock_InvMainDetails);
        }

        // PUT: api/Stock_InvMainDetails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStock_InvMainDetails([FromRoute] long id, [FromBody] Stock_InvMainDetails stock_InvMainDetails,
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

            if (id != stock_InvMainDetails.Stock_InvMainDetails_id)
            {
                return BadRequest();
            }

            _context.Entry(stock_InvMainDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Stock_InvMainDetailsExists(id))
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

        // POST: api/Stock_InvMainDetails
        [HttpPost]
        public async Task<IActionResult> PostStock_InvMainDetails([FromForm] string stock_InvMainDetails_str,
                                                    [FromQuery] string invMain_id, 
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

            try
            {
                Stock_InvMainDetails[] stock_InvMainDetails = JsonConvert.DeserializeObject<Stock_InvMainDetails[]>(stock_InvMainDetails_str);
                for (int i = 0; i < stock_InvMainDetails.Length; i++)
                {
                    stock_InvMainDetails[i].InvMain_id = long.Parse(invMain_id);
                }

                _context.Stock_InvMainDetails.AddRange(stock_InvMainDetails);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetStock_InvMainDetails", new { id = stock_InvMainDetails[0].Stock_InvMainDetails_id }, stock_InvMainDetails);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
            

            
        }

        // DELETE: api/Stock_InvMainDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock_InvMainDetails([FromRoute] long id,
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

            var stock_InvMainDetails = await _context.Stock_InvMainDetails.FindAsync(id);
            if (stock_InvMainDetails == null)
            {
                return NotFound();
            }

            _context.Stock_InvMainDetails.Remove(stock_InvMainDetails);
            await _context.SaveChangesAsync();

            return Ok(stock_InvMainDetails);
        }

        private bool Stock_InvMainDetailsExists(long id)
        {
            return _context.Stock_InvMainDetails.Any(e => e.Stock_InvMainDetails_id == id);
        }
        //[Route("EditStock_InvMainDetails/{Stock_InvMainDetails_id}")]
        //[HttpPut]
        //public async Task<IActionResult> EditStock_InvMainDetails([FromRoute] long Stock_InvMainDetails_id, [FromBody] Stock_InvMainDetails stock_InvMainDetails,
        //                                            [FromQuery] string api_id, [FromQuery] string pass)
        //{
        //    if ((api_id != id_var) || (pass != pass_var))
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != stock_InvMainDetails.Stock_InvMainDetails_id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(stock_InvMainDetails).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!Stock_InvMainDetailsExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}


    }
}