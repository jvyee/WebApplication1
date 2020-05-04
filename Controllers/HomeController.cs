using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using WebApplication1.Models;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private  DbContext dbt;
        private readonly Configuration cfg;
        private readonly TestDBContext _TestDBContext;


        public HomeController(ILogger<HomeController> logger, TestDBContext _dbt)
        {
            _logger = logger;
            dbt = _dbt;
            //cfg = _cfg;
            _TestDBContext = (TestDBContext)_dbt;
        }

        public IActionResult Index() {
            //MySqlConnection con =(MySqlConnection)dbt.Database.GetDbConnection();

            //String ConnStr = Configuration.GetConnectionString("Default");

            //MySqlConnection con = new MySqlConnection(ConnStr); //建立连接

            /*dbt.Database.ExecuteSqlRaw("Insert into test_table1 values(1,'ttt',NOW())"); //新增数据

            dbt.Database.ExecuteSqlRaw("Insert into test_table1 values(2,'sss',NOW())");

            dbt.Database.ExecuteSqlRaw("Insert into test_table1 values(3,'ggg',NOW())"); */

            /*dbt.Database.ExecuteSqlRaw("update test_table1 set Name = 'ttt' where Id = @Id", new List<MySqlParameter>{ new MySqlParameter("@Id", 3) }); //修改数据      
            String connstr=dbt.Database.GetDbConnection().ConnectionString;
            MySql.Simple.Database smd = new MySql.Simple.Database(connstr+ "; Password=123456"); 
            //con= new MySql.Data.MySqlClient.MySqlConnection(connstr); 
            //var list = con.Query("select * from test_table1"); //查询数据
            var list = smd.Query("select * from test_table1"); //查询数据
            
            //foreach (var item in list)
            while(list.Read())
            {

                //Console.WriteLine($"姓名：{item.Name} 生日：{item.Birth}");
                Console.WriteLine($"姓名：{list["Name"]} id：{list["id"]}");

            }

            //dbt.Database.ExecuteSqlRaw("delete from test_table1 where Id = @Id", new { Id = 1 }); //删除数据
            

            return View();  */
            var tests = _TestDBContext.Tests.ToList();
            //AsyncEnumerableQuery<Test> l_list = new AsyncEnumerableQuery<Test>(_TestDBContext.Tests);
            return View(tests);
            //return View(l_list);

        }

        /// <summary>
        /// 详细页 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //AsyncQueryProvider<Test> l_list = new AsyncQueryProvider<Test>(new AsyncEnumerableQuery<Test>(_TestDBContext.Tests));
            //var movie = await l_list.ExecuteAsync(m => m.Id == id);  // FirstOrDefaultAsync(m => m.Id == id);
            //var movie = await _TestDBContext.Tests.FirstOrDefaultAsync(m => m.Id == id);
            //var l_list = new AsyncEnumerableQuery<Test>(_TestDBContext.Tests);
            //var movie = await l_list.FirstOrDefaultAsync(m => m.Id == id);
            Test movie = _TestDBContext.Tests.FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        public async Task<IActionResult> Add()
        {
            Test movie = new Test();
           
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("Id,Name")] Test movie)
        {
            if (movie == null || movie.Id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _TestDBContext.Add(movie);
                    await _TestDBContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Test movie = _TestDBContext.Tests.FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Test movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _TestDBContext.Update(movie);
                    await _TestDBContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // 删除没有对应的页面，从列表页的Delete点击进入,下面是删除的关键代码
        public async Task<IActionResult> Delete(int id)
        {
            var movie = _TestDBContext.Tests.FirstOrDefault(m => m.Id == id);
            _TestDBContext.Tests.Remove(movie);
            await _TestDBContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
