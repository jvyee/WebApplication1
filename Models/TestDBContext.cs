using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class TestDBContext:DbContext
    {
        public TestDBContext() 
        {
        }
        public TestDBContext(DbContextOptions<TestDBContext> options):base(options)
        {
        }
        public DbSet<Test>  Tests{ get; set; }

    }
}
 