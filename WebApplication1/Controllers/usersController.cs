using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    [Produces("application/json")]
    [Route("api/users")]
    public class usersController : Controller
    {
        private readonly TodoContext _context;
        private string id_var = "admin";
        private string pass_var = "123";

        public usersController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/users
        [HttpGet]
        public IActionResult Getusers([FromQuery] string api_id, [FromQuery] string pass)
        {
            if ((api_id == id_var) && (pass == pass_var))
            {
                return Ok(_context.users.Select(x => new { x.id, x.name}).ToList().OrderBy(z => z.name));
            }
            else
            {
                return BadRequest(ModelState);
            }
            //return _context.users;
        }

        // GET: api/users/GetAllusers
        [HttpGet]
        [Route("GetAllusers")]
        public IActionResult GetAllusers([FromQuery] string api_id, [FromQuery] string pass)
        {
            if ((api_id == id_var) && (pass == pass_var))
            {
                return Ok(_context.users.Include(v => v.users_perm)
                                                   .Select(x => new { x.id,
                                                           x.name,
                                                           x.is_admin,
                                                           x.MaxDiscount,
                                                           x.Bransh_id,
                                                           x.dep_id,
                                                           x.Stock_ShopCard_Id,
                                                           x.UserCanChangeItemPrices,
                                                           x.UserCannotSoldByNegative,
                                                           x.ProgramLanguage,
                                                           x.users_perm}).ToList().OrderBy(x => x.id));
            }
            else
            {
                return BadRequest(ModelState);
            }
            //return _context.users;
        }

       

        // GET: api/users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Getusers([FromRoute] int id,
                                                    [FromQuery] string api_id, [FromQuery] string pass)
        {
            if ((api_id == id_var) && (pass == pass_var))
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                users users = null;
                try
                {
                    users = await _context.users.Include(v => v.users_perm).SingleOrDefaultAsync(m => m.id == id);
                    //users.password = GetMd5HashData(users.password);
                }
                catch (Exception ex)
                {

                    throw;
                }
               

                if (users == null)
                {
                    return NotFound();
                }

                return Ok(users);
            }
            else
            {
                return BadRequest(ModelState);
            }            
        }

        public static string GetMd5HashData(string yourString)
        {
            return string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(yourString)).Select(s => s.ToString("x2")));
        }

        
        // PUT: api/users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Putusers([FromRoute] int id, [FromForm] string users,
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

            users users_arr = null;
            try
            {
                users_arr = JsonConvert.DeserializeObject<users>(users);
            }
            catch (Exception ex)
            {

                throw;
            }
            if (id != users_arr.id)
            {
                return BadRequest();
            }

            _context.Entry(users_arr).State = EntityState.Modified;

            try
            {               
                await _context.SaveChangesAsync();
                await addPerm(users_arr.id, JsonConvert.SerializeObject(users_arr.users_perm));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!usersExists(id))
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

        // POST: api/users
        [HttpPost]
        public async Task<IActionResult> Postusers([FromForm] string users,
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

            users users_arr = null;

            try
            {
                users_arr = JsonConvert.DeserializeObject<users>(users);
            }
            catch (Exception ex)
            {

                throw;
            }
            

            _context.users.Add(users_arr);
            
            try
            {              
                await _context.SaveChangesAsync();
                await addPerm(users_arr.id, JsonConvert.SerializeObject(users_arr.users_perm));
            }
            catch (Exception ee)
            {
                string sss = ee.Message;
                throw;
            }

            return CreatedAtAction("Getusers", new { id = users_arr.id }, users);
        }

        private async Task<IActionResult> addPerm(int user_id, string users_perm)
        {
            // Save Details
            users_perm[] users_perm_arr = JsonConvert.DeserializeObject<users_perm[]>(users_perm);
            for (int i = 0; i < users_perm_arr.Length; i++)
            {
                users_perm_arr[i].id = user_id;
            }

            var users_perm_del = _context.users_perm.Where(x => x.id == user_id).ToList();
            if (users_perm_del == null)
            {
                return NotFound();
            }

            _context.users_perm.RemoveRange(users_perm_del);
            await _context.SaveChangesAsync();           
            _context.users_perm.AddRange(users_perm_arr);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStock_ItemsAndUnits", new { id = user_id }, users_perm_arr);
        }
        
        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteusers([FromRoute] int id,
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

            var users_arr = await _context.users.FindAsync(id);
            if (users_arr == null)
            {
                return NotFound();
            }

            _context.users.Remove(users_arr);
            try
            {
                var users_perm_arr_del = _context.users_perm.Where(x => x.id == id).ToList();
                if (users_perm_arr_del != null)
                {
                    _context.users_perm.RemoveRange(users_perm_arr_del);
                    await _context.SaveChangesAsync();
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception eee)
            {

                throw;
            }

            return Ok(users_arr);
        }
        
        private bool usersExists(int id)
        {
            return _context.users.Any(e => e.id == id);
        }
        
    }
}