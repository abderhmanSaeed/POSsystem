using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApplication1.Models;

namespace WebApplication1
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoList"));
            //services.AddDbContext<TodoContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddMvc();
            //services.AddDbContext<EZSportsContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConn")));

            //services.AddDbContext<DbConfigContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConn")));

            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

            services.AddDbContext<TodoContext>(((serviceProvider, options) =>
            {
                var httpContext = serviceProvider.GetService<IHttpContextAccessor>().HttpContext;
                var httpRequest = httpContext.Request;

                // Get the 'database' querystring parameter from the request (if supplied - default is empty).
                // TODO: Swap this out for an enum.
                //var databaseQuerystringParameter = httpRequest.Query["database"].ToString();

                var serverQuerystringParameter = "";
                var databaseQuerystringParameter = "";
                var UserIdQuerystringParameter = "";
                var PwdQuerystringParameter = "";

                try
                {
                    serverQuerystringParameter = httpRequest.Form["server"].ToString();
                    databaseQuerystringParameter = httpRequest.Form["database"].ToString();
                    UserIdQuerystringParameter = httpRequest.Form["user"].ToString();
                    PwdQuerystringParameter = httpRequest.Form["pwd"].ToString();
                }
                catch (Exception ee)
                {
                    //throw;
                }                

                // Get the base, formatted connection string with the 'DATABASE' paramter missing.
                var dbConnectionString = Configuration.GetConnectionString("DefaultConn");

                if (databaseQuerystringParameter != "" &&
                     serverQuerystringParameter != "" &&
                       UserIdQuerystringParameter != "" &&
                         PwdQuerystringParameter != "")
                {
                    // We have a 'database' param, stick it in.
                    dbConnectionString = string.Format(dbConnectionString, 
                                                        databaseQuerystringParameter,
                                                            serverQuerystringParameter, 
                                                            UserIdQuerystringParameter,
                                                             PwdQuerystringParameter);
                }
                else
                {
                    // We havent been given a 'database' param, use the default.
                    var dbDefaultDatabaseValue = Configuration.GetConnectionString("DefaultConn");
                    dbConnectionString = dbDefaultDatabaseValue;//string.Format(dbConnectionString, dbDefaultDatabaseValue);
                }

                // Build the EF DbContext using the built conn string.
                //options.UseDb2(db2ConnectionString, p => p.SetServerInfo(IBMDBServerType.OS390));
                options.UseSqlServer(dbConnectionString);
            }));

            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            app.UseMvc();
        }
    }
}
