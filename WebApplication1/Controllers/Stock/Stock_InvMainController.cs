using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplication1.DTO;
using WebApplication1.Models;
using WebApplication1.Models.Stock;

namespace WebApplication1.Controllers.Stock
{
    [Route("api/[controller]")]
    [ApiController]
    public class Stock_InvMainController : ControllerBase
    {
        private readonly TodoContext _context;
        private string id_var = "admin";
        private string pass_var = "123";

        public Stock_InvMainController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Stock_InvMain
       
        [HttpGet]
        //public IActionResult GetStock_InvMain([FromQuery] string api_id, [FromQuery] string pass)
        //{
        //    Stock_InvMainDetails stock_InvMainDetails = new Stock_InvMainDetails();
        //    if ((api_id == id_var) && (pass == pass_var))
        //    {
        //        var obj = _context.Stock_InvMain.Include(p => p.payments_types).Include(a=>a.Stock_InvMainDetails).Include(c => c.Account).ToList();
        //        return Ok(obj);
        //    }
        //    else
        //    {
        //        return BadRequest(ModelState);
        //    }
        //}

       
        public IActionResult GetStock_InvMain([FromQuery] string api_id, [FromQuery] string pass)
        {
            //  var obj = _context.Stock_InvMain.Include(p => p.payments_types).Include(a => a.Stock_InvMainDetails).Include(c => c.Account).ToList();
            var obj = _context.Stock_InvMain.Include(p => p.payments_types).Include(c => c.Account).Include(e => e.Stock_InvMainDetails).ToList();



                return Ok(obj);
          
         
        }


        // GET: api/Stock_InvMain/GetStock_InvMain_ByTypeId/5
        [HttpGet]
        [Route("GetStock_InvMain_ByTypeId/{id}")]
        public ActionResult GetStock_InvMain_ByTypeId([FromRoute] long id,
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

            var stock_InvMain = _context.Stock_InvMain.Where(x => x.InvType_id == id)
                .Include(p => p.payments_types)
                .Include(c => c.Account).Include(a=>a.Stock_InvMainDetails).ToList();

            if (stock_InvMain == null)
            {
                return NotFound();
            }

            return Ok(stock_InvMain);
        }

        // GET: api/Stock_InvMain/5
        [HttpGet("{id}")]
        public ActionResult GetStock_InvMain([FromRoute] long id,
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

            //var stock_InvMain = await _context.Stock_InvMain.FindAsync(id);
            var stock_InvMain = _context.Stock_InvMain.Where(x => x.InvMain_id == id).Include(c=>c.Account).Include(p => p.payments_types)
                                                    .Include(d => d.Stock_InvMainDetails).ToList();
            //.OrderBy(f => f.Stock_InvMainDetails_id)

            if (stock_InvMain == null)
            {
                return NotFound();
            }

            return Ok(stock_InvMain);
        }

        // PUT: api/Stock_InvMain/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStock_InvMain([FromRoute] long id, 
                                                    [FromForm] string stock_InvMain,
                                                    [FromForm] string stock_InvMainDetails,
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

          
            Stock_InvMain stock_InvMain_arr = JsonConvert.DeserializeObject<Stock_InvMain>(stock_InvMain);
            _context.Entry(stock_InvMain_arr).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();              
                await addDetails(stock_InvMain_arr.InvMain_id, stock_InvMainDetails);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Stock_InvMainExists(id))
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

        
        // POST: api/Stock_InvMain
        [HttpPost]
        public async Task<IActionResult> PostStock_InvMain([FromForm] string stock_InvMain,
                                                            [FromForm] string stock_InvMainDetails,
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

              Stock_InvMain stock_InvMain_arr = JsonConvert.DeserializeObject<Stock_InvMain>(stock_InvMain);

            long typeSerial = 0;
            try
            {
                typeSerial = _context.Stock_InvMain.Where(y => y.InvType_id == stock_InvMain_arr.InvType_id).Max(x => x.InvMain_TypeSerial).Value;
            }
            catch (Exception)
            {
                typeSerial = 0;
                //throw;
            }            
            stock_InvMain_arr.InvMain_TypeSerial = typeSerial + 1;

            _context.Stock_InvMain.Add(stock_InvMain_arr);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                //throw;
            }

            

            //long TypeSerial = _context.Stock_InvMain.Max(x => x.InvMain_TypeSerial).Value;

            // Save Details
            await addDetails(stock_InvMain_arr.InvMain_id , stock_InvMainDetails);
            
            return CreatedAtAction("GetStock_InvMain", new { id = stock_InvMain_arr.InvMain_id }, stock_InvMain_arr);
        }

        private async Task<IActionResult> addDetails(long invMain_id, string stock_InvMainDetails)
        {
            // Save Details
            Stock_InvMainDetails[] stock_InvMainDetails_arr = JsonConvert.DeserializeObject<Stock_InvMainDetails[]>(stock_InvMainDetails);
            for (int i = 0; i < stock_InvMainDetails_arr.Length; i++)
            {
                stock_InvMainDetails_arr[i].InvMain_id = invMain_id;
            }

            await DeleteStock_InvMain(invMain_id, id_var, pass_var, true);

            _context.Stock_InvMainDetails.AddRange(stock_InvMainDetails_arr);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStock_InvMain", new { id = invMain_id }, stock_InvMainDetails_arr);           
        }
        [Route("AddInv_Main")]
        [HttpPost]
        public IActionResult AddInv_Main(Stock_InvMain stock_InvMain,List<Stock_InvMainDetails> stock_InvMainDetails
            , [FromQuery] string api_id, [FromQuery] string pass)
        {
            if ((api_id != id_var) || (pass != pass_var))
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            long typeSerial = 0;
            try
            {
                typeSerial = _context.Stock_InvMain.Where(y => y.InvType_id == stock_InvMain.InvType_id).Max(x => x.InvMain_TypeSerial).Value;
            }
            catch (Exception)
            {
                typeSerial = 0;
            }
            stock_InvMain.InvMain_TypeSerial = typeSerial + 1;

            _context.Stock_InvMain.Add(stock_InvMain);
            _context.SaveChanges();
            foreach (var item in stock_InvMainDetails)
            {
                item.InvMain_id = stock_InvMain.InvMain_id;
                _context.Stock_InvMainDetails.Add(item);
                _context.SaveChanges();
            }


            return CreatedAtAction("GetStock_InvMain", new { id = stock_InvMain.InvMain_id }, stock_InvMain);
        }

       [Route("EditStock_InvMain/{id}")]
       [HttpPut]
        public  IActionResult EditStock_InvMain([FromRoute] long id,
                                               Stock_InvMain stock_InvMain,
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

            _context.Entry(stock_InvMain).State = EntityState.Modified;
            _context.SaveChanges();

            try
            {

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Stock_InvMainExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }



        // DELETE: api/Stock_InvMain/5
        [HttpDelete("{InvMain_id}")]
        public async Task<IActionResult> DeleteStock_InvMain([FromRoute] long InvMain_id,
                                                    [FromQuery] string api_id, [FromQuery] string pass,
                                                    bool callFromAdd = false)
        {
            if ((api_id != id_var) || (pass != pass_var))
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stock_InvMainDetails = _context.Stock_InvMainDetails.Where(x => x.InvMain_id == InvMain_id).ToList();
            if (stock_InvMainDetails == null)
            {
                return NotFound();
            }

            _context.Stock_InvMainDetails.RemoveRange(stock_InvMainDetails);
            await _context.SaveChangesAsync();

            if (callFromAdd == false)
            {                
                try
                {
                    var sheet_no = _context.Stock_InvMain.Where(z => z.InvMain_id == InvMain_id).ToList()[0].AccountSheetNo.ToString();
                    await DeleteStock_InvMain2(InvMain_id);
                    if (sheet_no != null)
                    {
                        Account_SheetController account_SheetController = new Account_SheetController(_context);
                        await account_SheetController.DeleteAccount_Sheet(long.Parse(sheet_no), api_id, pass, false);
                    }
                }
                catch (Exception)
                {
                    return Ok(stock_InvMainDetails);
                }               
            }

            return Ok(stock_InvMainDetails);
        }

        public async Task<IActionResult> DeleteStock_InvMain2([FromRoute] long id)
        {            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stock_InvMain = _context.Stock_InvMain.Where(x => x.InvMain_id == id).ToList();
            if (stock_InvMain == null)
            {
                return NotFound();
            }

            _context.Stock_InvMain.RemoveRange(stock_InvMain);
            await _context.SaveChangesAsync();         

            return Ok(stock_InvMain);
        }

        private bool Stock_InvMainExists(long id)
        {
            return _context.Stock_InvMain.Any(e => e.InvMain_id == id);
        }

        [Route("EditStock_InvMainAndDetails/{id}")]
        [HttpPut]
        public IActionResult EditStock_InvMainAndDetails([FromRoute] long id,
                                        Stock_InvMain stock_InvMain,
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
                _context.Entry(stock_InvMain).State = EntityState.Modified;
                _context.SaveChanges();
                var InvDetails = _context.Stock_InvMainDetails.Where(a => a.InvMain_id == stock_InvMain.InvMain_id).ToList();
                foreach (var item1 in InvDetails)
                {
                    _context.Stock_InvMainDetails.Remove(item1);
                    _context.SaveChanges();
                }
                foreach (var item2 in stock_InvMain.Stock_InvMainDetails)
                {
                    item2.InvMain_id = stock_InvMain.InvMain_id;
                  //  _context.Entry(item2).State = EntityState.Modified;
                    _context.Stock_InvMainDetails.Add(item2);
                    _context.SaveChanges();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Stock_InvMainExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }



    }
}