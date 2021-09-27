using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebApplication1.Models;
using WebApplication1.Models.Stock;

namespace WebApplication1.Controllers.Stock
{
    [Route("api/[controller]")]
    [ApiController]
    public class Stock_ItemsGroup_DetailsController : ControllerBase
    {
        private readonly TodoContext _context;
        private string id_var = "admin";
        private string pass_var = "123";

        public Stock_ItemsGroup_DetailsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Stock_ItemsGroup_Details
        [HttpGet("{id}")]
        public IActionResult GetStock_ItemsGroup_Details([FromRoute] long id,
                                                         [FromQuery] string api_id, [FromQuery] string pass)
        {
            if ((api_id != id_var) || (pass != pass_var))
            {
                return BadRequest(ModelState);
            }
            var stock_ItemsGroup_var = _context.Stock_ItemsGroup_Details.Where(x => x.Stock_ItemsGroup_id == id).ToList();
            return Ok(stock_ItemsGroup_var);
        }

        /**
        // GET: api/Stock_ItemsGroup_Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStock_ItemsGroup_Details([FromRoute] long id,
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

            var stock_ItemsGroup_Details = await _context.Stock_ItemsGroup_Details.FindAsync(id);

            if (stock_ItemsGroup_Details == null)
            {
                return NotFound();
            }

            return Ok(stock_ItemsGroup_Details);
        }
        
    **/
        // PUT: api/Stock_ItemsGroup_Details/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStock_ItemsGroup_Details([FromRoute] long id,
                                                            [FromForm] string detailsGroup,
                                                         [FromForm] string GroupDesc,
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

            Stock_ItemsGroup stock_ItemsGroup = new Stock_ItemsGroup();
            stock_ItemsGroup.Stock_ItemsGroup_id = id;
            stock_ItemsGroup.Stock_ItemsGroup_desc = GroupDesc;

            _context.Entry(stock_ItemsGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                await AddDetailsAsync(id, detailsGroup, api_id, pass);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Stock_ItemsGroupExists(id))
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


        // POST: api/Stock_ItemsGroup_Details
        [HttpPost]
        public async Task<IActionResult> PostStock_ItemsGroup_Details([FromForm] string detailsGroup,
                                                         [FromForm] string GroupDesc, 
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

            //Add Items Group Main
            Stock_ItemsGroup stock_ItemsGroup = new Stock_ItemsGroup();
            stock_ItemsGroup.Stock_ItemsGroup_desc = GroupDesc;

            _context.Stock_ItemsGroup.Add(stock_ItemsGroup);
            await _context.SaveChangesAsync();
            long itemsGroup_id = stock_ItemsGroup.Stock_ItemsGroup_id;

            return await AddDetailsAsync(itemsGroup_id, detailsGroup, api_id, pass);

        }

        private async Task<IActionResult> AddDetailsAsync(long itemsGroup_id, string detailsGroup,
                                                         string api_id, string pass)
        {
            //Convert Json String To Object
            Stock_ItemsGroup_Details[] stock_ItemsGroup_Details = JsonConvert.DeserializeObject<Stock_ItemsGroup_Details[]>(detailsGroup);

            //Loop Every Item In Details To Put The itemsGroup_id 
            for (int i = 0; i < stock_ItemsGroup_Details.Length; i++)
            {
                stock_ItemsGroup_Details[i].Stock_ItemsGroup_id = itemsGroup_id;
            }

            // 
            await DeleteStock_ItemsGroup_Details(itemsGroup_id, api_id, pass, true);

            // Adding Details
            _context.Stock_ItemsGroup_Details.AddRange(stock_ItemsGroup_Details);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetStock_ItemsGroup_Details", new { id = stock_ItemsGroup_Details[0].Stock_ItemsGroup_id }, stock_ItemsGroup_Details);
        }

        // DELETE: api/Stock_ItemsGroup_Details/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock_ItemsGroup_Details([FromRoute] long id,
                                                         [FromQuery] string api_id, [FromQuery] string pass, bool callFromAdd = false)
        {
            if ((api_id != id_var) || (pass != pass_var))
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stock_ItemsGroup_Details = _context.Stock_ItemsGroup_Details.Where(x => x.Stock_ItemsGroup_id == id).ToList();
            // var stock_ItemsGroup_Details = await _context.Stock_ItemsGroup_Details.FindAsync(id);

            if (stock_ItemsGroup_Details == null)
            {
                return NotFound();
            }

            _context.Stock_ItemsGroup_Details.RemoveRange(stock_ItemsGroup_Details);
            //_context.Stock_ItemsGroup_Details.Remove(stock_ItemsGroup_Details);
            await _context.SaveChangesAsync();

            if (callFromAdd == false)
            {
                await DeleteStock_ItemsGroup2(id);
            }
            

            return Ok(stock_ItemsGroup_Details);
        }

        private async Task<IActionResult> DeleteStock_ItemsGroup2(long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stock_ItemsGroup = _context.Stock_ItemsGroup.Where(x => x.Stock_ItemsGroup_id == id).ToList();
            // var stock_ItemsGroup_Details = await _context.Stock_ItemsGroup_Details.FindAsync(id);

            if (stock_ItemsGroup == null)
            {
                return NotFound();
            }

            _context.Stock_ItemsGroup.RemoveRange(stock_ItemsGroup);
            //_context.Stock_ItemsGroup_Details.Remove(stock_ItemsGroup_Details);
            await _context.SaveChangesAsync();

            return Ok(stock_ItemsGroup);
        }
        private bool Stock_ItemsGroupExists(long id)
        {
            return _context.Stock_ItemsGroup.Any(e => e.Stock_ItemsGroup_id == id);
        }
    }
}