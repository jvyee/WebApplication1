using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class SQLTest 
    { 
        private TestDBContext _context;

        public SQLTest(TestDBContext context)
        {
            _context = context;
        }
        public void Addtest(Test test)
        {
            _context.Tests.Add(test);
            _context.SaveChanges();
        }
        public Test GetTestById(int id)
        {
            return _context.Tests.FirstOrDefault(x => x.Id == id);
        }
        public  IEnumerable<Test> GetAlld()
        {
            return _context.Tests.ToList<Test>();
        }
    }
}

