using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Extensions.Configuration;


namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateDBController : ControllerBase
    {

        private readonly DbConfigContext _context;
        private string id_var = "admin";
        private string pass_var = "1978";

        public IConfiguration Configuration { get; }

        public CreateDBController(DbConfigContext context, 
                                   IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        // Get: api/CreateDBController
        [HttpGet]
        public IActionResult Get([FromQuery] string api_id,
                                                [FromQuery] string pass,
                                                [FromQuery] string CustomerId)
        {
            if ((api_id != id_var) || (pass != pass_var) || (CustomerId == null) || (CustomerId == "") )
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            var sql = "";

            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader("db.sql"))
                {
                    // Read the stream to a string, and write the string to the console.
                    sql = sr.ReadToEnd();
                    //Console.WriteLine(line);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            //توليد رقم عشوائي مكون من 5 ارقام
            var rnd1 = GetRandomString(5);
            //معرفة عدد قواعد البيانات الموجودة في قاعدة البيانات
            var db_c = _context.Databases.Count();
            //وضع اسم لقاعدة البيانات
            var db_name1 = "dd_db" + "_" + db_c + "_" + rnd1;


            CustomerId = CustomerId.Trim();
            var customerIdLocal = CustomerId + "_" + rnd1;
            
            //اسبدال علامة الاستفهام باسم قاعدة البيانات في الاوامر
            sql = sql.Replace("?", "_" + db_c + "_" + rnd1) ;

            //تقسيم الملف الي عدة اوامر مصغرة
            string[] sql_arr = sql.Split("GO");
            var sql_statment = "";

            // جلب بيانات الاتصال الافتراضية للقاعدة البيانات الكونفج و تنفيذ اوامر انشاء قاعدة البيانات
            var db_con = Configuration.GetConnectionString("DbConfigDatabaseValue");//_context.Database.GetDbConnection();
            SqlConnection myConn = new SqlConnection(db_con);
            myConn.Open();

            try
            {
                try
                {
                    foreach (var item in sql_arr)
                    {
                        SqlCommand myCommand = new SqlCommand(item, myConn);
                        sql_statment = item;
                        //_context.Database.ExecuteSqlCommand(item);                        
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception ex1)
                {
                    Console.WriteLine(ex1.Message);
                    throw;
                }  
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                if (myConn.State == System.Data.ConnectionState.Open)
                {
                    myConn.Close();
                }
            }

            
            //في حال وجود اوامر يقوم النظام بإضافة سجل جديد في جدول قواعد البيانات 
            if (sql_arr != null)
            {
                try
                {                    
                        string sql1 = "USE DataConfig; " +
                                        "INSERT INTO [DataConfig].[dbo].[Databases] " +
                                            "([Name], data_version, Note, [server], [login], [password]) " +
                                        "VALUES " +
                                            "('" + db_name1 + "', '1.0.4.307', '" + customerIdLocal + "', null, null, null)";

                        int count = _context.Database.ExecuteSqlCommand(sql1);                 
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            CreateDatabaseResult createDatabaseResult = new CreateDatabaseResult();
            createDatabaseResult.CustomerId = customerIdLocal;
            createDatabaseResult.DatabaseName = db_name1;
            sql_arr = null;

            return Ok(createDatabaseResult);

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        static string GetRandomString(int lenOfTheNewStr)
        {
            string output = string.Empty;
            while (true)
            {
                output = output + Path.GetRandomFileName().Replace(".", string.Empty);
                if (output.Length > lenOfTheNewStr)
                {
                    output = output.Substring(0, lenOfTheNewStr);
                    break;
                }
            }
            return output;
        }
    }
}
