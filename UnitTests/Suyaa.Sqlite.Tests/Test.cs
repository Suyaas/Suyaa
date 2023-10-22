using Microsoft.EntityFrameworkCore;
using Suyaa.Data;
using Suyaa.Sqlite.Tests.Entities;
using Xunit.Abstractions;
using System.Linq;
using System.Text.Json;
using System.Text.Encodings.Web;
using Suyaa.EFCore.Dependency;
using Suyaa.Data.Queries;
using Suyaa.Data.Sqlite;
using Suyaa.EFCore;
using SqlServerDemo.Entities;

namespace Suyaa.Sqlite.Tests
{
    public class Test
    {
        private readonly ITestOutputHelper _output;

        public Test(ITestOutputHelper testOutput)
        {
            _output = testOutput;
        }

        [Fact]
        public void Create()
        {
            // 定义数据
            string connectionString = $"data source={sy.IO.GetExecutionPath("temp.db")}";
            // 执行方法
            using (DatabaseConnection conn = new DatabaseConnection(DbTypes.Sqlite, connectionString))
            {
                conn.Open();
                conn.TableCreated<Department>().Wait();
                conn.TableCreated<People>().Wait();
            }
            // 返回结果
            _output.WriteLine("OK");
        }

        //[Fact]
        //public void Insert()
        //{
        //    // 定义数据
        //    string connectionString = $"data source={sy.IO.GetExecutionPath("temp.db")}";
        //    // 执行方法
        //    using (DatabaseConnection conn = new DatabaseConnection(DatabaseTypes.Sqlite, connectionString))
        //    {
        //        conn.Open();
        //        Data.Dependency.IRepository<People, string> peopleRepository = new Data.Dependency.Repositories.Repository<People, string>(conn);
        //        Data.Dependency.IRepository<Department, string> departmentRepository = new Data.Dependency.Repositories.Repository<Department, string>(conn);
        //        // 添加大部门
        //        Department? bigDepartment = departmentRepository.GetRow(d => d.Name == "大部门");
        //        if (bigDepartment is null)
        //        {
        //            bigDepartment = new Department()
        //            {
        //                Name = "大部门"
        //            };
        //            departmentRepository.Insert(bigDepartment);
        //        }
        //        // 添加小部门
        //        Department? smallDepartment = departmentRepository.GetRow(d => d.Name == "小部门");
        //        if (smallDepartment is null)
        //        {
        //            smallDepartment = new Department()
        //            {
        //                Name = "小部门"
        //            };
        //            departmentRepository.Insert(smallDepartment);
        //        }
        //        // 添加张三
        //        if (peopleRepository.GetRow(d => d.Name == "张三") is null)
        //        {
        //            People people = new People()
        //            {
        //                Age = 10,
        //                Name = "张三",
        //                DepartmentId = bigDepartment.Id,
        //            };
        //            peopleRepository.Insert(people);
        //        }
        //        // 添加李四
        //        if (peopleRepository.GetRow(d => d.Name == "李四") is null)
        //        {
        //            People people = new People()
        //            {
        //                Age = 20,
        //                Name = "李四",
        //                DepartmentId = bigDepartment.Id,
        //            };
        //            peopleRepository.Insert(people);
        //        }
        //        // 添加王五
        //        if (peopleRepository.GetRow(d => d.Name == "王五") is null)
        //        {
        //            People people = new People()
        //            {
        //                Age = 30,
        //                Name = "王五",
        //                DepartmentId = smallDepartment.Id,
        //            };
        //            peopleRepository.Insert(people);
        //        }
        //    }
        //    // 返回结果
        //    _output.WriteLine("OK");
        //}

        [Fact]
        public void Query()
        {
            //// 定义数据
            //string connectionString = $"data source={sy.IO.GetExecutionPath("temp.db")}";
            //// 执行方法
            //using (DatabaseConnection conn = new DatabaseConnection(DatabaseTypes.Sqlite, connectionString))
            //{
            //    conn.Open();
            //    Data.Dependency.IRepository<People, string> repository = new Data.Dependency.Repositories.Repository<People, string>(conn);
            //    var peoples = repository.GetRows(d => d.Age > 8);
            //    // 返回结果
            //    _output.WriteLine($"peoples: {peoples.Count}");
            //}
            var provider = new SqliteProvider();
            var department = new EntityQueryable<Department>(new EntityQueryProvider(provider.QueryProvider));
            var query = from d in department
                        select d;
            var list = query.ToList();
        }

        [Fact]
        public async void EFQuery()
        {
            // 定义数据
            string connectionString = $"data source={sy.IO.GetExecutionPath("temp.db")}";
            var optionsBuilder = new DbContextOptionsBuilder<DbContext>();
            optionsBuilder.UseSqlite(connectionString);
            // 执行方法
            //using (TestDbContext context = new TestDbContext(optionsBuilder.Options, connectionString))
            //{
            //    //IRepository<People, string> peopleRepository = new EFCore.Dbsets.Repository<People, string>(context);
            //    //IRepository<Department, string> departmentRepository = new EFCore.Dbsets.Repository<Department, string>(context);
            //    var query = from p in context.Peoples
            //                join d in context.Departments on p.DepartmentId equals d.Id
            //                where d.Name.Contains("大")
            //                select p;
            //    var datas = await query.ToListAsync();
            //    // 返回结果
            //    _output.WriteLine(JsonSerializer.Serialize(datas, new JsonSerializerOptions()
            //    {
            //        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            //        WriteIndented = true,
            //    }));
            //}
        }

        //[Fact]
        //public async void EFUpdate()
        //{
        //    // 定义随机函数
        //    Random random = new Random();
        //    // 定义数据
        //    string connectionString = $"data source={sy.IO.GetExecutionPath("temp.db")}";
        //    var optionsBuilder = new DbContextOptionsBuilder<DbContext>();
        //    optionsBuilder.UseSqlite(connectionString);
        //    // 执行方法
        //    using (TestDbContext context = new TestDbContext(optionsBuilder.Options, connectionString))
        //    {
        //        IRepository<People, string> peopleRepository = new EFCore.Dbsets.Repository<People, string>(context);
        //        IRepository<Department, string> departmentRepository = new EFCore.Dbsets.Repository<Department, string>(context);
        //        var query = from p in peopleRepository.Query()
        //                    join d in departmentRepository.Query() on p.DepartmentId equals d.Id
        //                    where p.Name == "张三"
        //                    select p;
        //        var data = await query.FirstOrDefaultAsync();
        //        if (data is null)
        //        {
        //            _output.WriteLine($"not found.");
        //            return;
        //        }
        //        data.Age = random.Next(100);
        //        await peopleRepository.UpdateAsync(data);
        //        // 返回结果
        //        _output.WriteLine($"OK");
        //    }
        //}

        [Fact]
        public async void EFQueryDescriptor()
        {
            var descriptor = new DbConnectionDescriptor("def", "[SqlServer]Server=10.10.10.32,1433;Initial Catalog=Aos_cnglj;User ID=sa;Password=123456;TrustServerCertificate=true;");
            // 定义随机函数
            Random random = new Random();
            // 定义数据
            //string connectionString = $"data source={sy.IO.GetExecutionPath("temp.db")}";
            //var optionsBuilder = new DbContextOptionsBuilder<DbContext>();
            //optionsBuilder.UseSqlite(connectionString);
            List<Type> types = new List<Type>() {
                typeof(SystemTables),
                typeof(SystemObjects),
            };
            // 执行方法
            using var testDbContext = new TestDbContext(descriptor);
            using var context = new DbDescriptorTypeContext(testDbContext.ConnectionDescriptor, testDbContext.Options, types);
            {
                //IRepository<People, string> peopleRepository = new EFCore.Dbsets.Repository<People, string>(context);
                //IRepository<Department, string> departmentRepository = new EFCore.Dbsets.Repository<Department, string>(context);
                var query = from st in context.Set<SystemTables>()
                            join so in context.Set<SystemObjects>() on st.ObjectID equals so.Id
                            //where p.Name == "张三"
                            select st;
                var data = await query.FirstOrDefaultAsync();
                if (data is null)
                {
                    _output.WriteLine($"not found.");
                    return;
                }
                //data.Age = random.Next(100);
                //await peopleRepository.UpdateAsync(data);
                // 返回结果
                _output.WriteLine(data.Name);
            }
        }
    }
}