using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class DbConfigContext: DbContext
    {
        public DbConfigContext(DbContextOptions<DbConfigContext> options)
            : base(options)
        {}

        public DbSet<Databases> Databases { get; set; }
    }
}
