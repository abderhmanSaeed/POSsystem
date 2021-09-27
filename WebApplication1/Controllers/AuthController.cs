using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace WebApplication1.Models
{
    [Produces("application/json")]
    [Route("api/Auth")]
    public class AuthController : Controller
    {
        private readonly TodoContext _context;
        private string id_var = "admin";
        private string pass_var = "123";

        public AuthController(TodoContext context)
        {
            _context = context;
        }

        /*
        // GET: api/Auth
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Auth/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        */
        // POST: api/Auth
        [HttpPost]
        public IActionResult Post([FromForm]string username, 
                                    [FromForm]string password,
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

            var sql = "SELECT  ROW_NUMBER() OVER(ORDER BY users.name ASC) AS row_id, " +
                                        "users.id, users.name, users.dep_id, users.email, " +
                                        "users.is_admin, users.IsPOSUser, users.MaxDiscount, users.UserCanChangeItemPrices, " +
                                        "users.FormId, users.ProgId, users.Levels_id, users.Stock_ShopCard_Id, " +
                                        "users.JustUseExactShop, users.Bransh_id, users.JustUseExactBransh, " +
                                        "users.UserCannotSoldByNegative, users.UserCannotSeeDocForOthers, users.ProgramLanguage, " +
                                        "users.IsPOSUser_Market, " +
                                        "users_perm.form_id, users_perm.perm_type_id, users_perm.have_perm, " +
                                        "forms.form_design_name, users_perm.book_id, 'aa' AS token " +
                            "FROM forms INNER JOIN " +
                                "users_perm ON forms.form_id = users_perm.form_id RIGHT OUTER JOIN " +
                                "users ON users_perm.emp_id = users.id " +
                            "WHERE" +
                                "(users.name = N'" + username + "') AND " +
                                "(users.password = N'" + password + "') AND " +
                                "(ISNULL(users.out, 0) = 0)";

            var perm = _context.userPermCustom.FromSql(sql).ToList();

            var jwt = new JwtSecurityToken();
            var encodedJwt = new JwtSecurityTokenHandler()
                    .WriteToken(jwt);

            for (int i = 0; i < perm.Count; i++)
            {
                perm[i].token = encodedJwt.ToString();
            }

            //perm.Insert()
            if (perm == null || perm.Count == 0)
            {
                return NotFound();
            }

            return Ok(perm);
        }


        /*
        // PUT: api/Auth/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
    }
}
