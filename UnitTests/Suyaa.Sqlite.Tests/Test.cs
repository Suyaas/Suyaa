using Microsoft.EntityFrameworkCore;
using Suyaa.Data;
using Suyaa.EFCore.Dbsets;
using Suyaa.Sqlite.Tests.Entities;
using Xunit.Abstractions;
using System.Linq;

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
            using (DatabaseConnection conn = new DatabaseConnection(DatabaseTypes.Sqlite, connectionString))
            {
                conn.Open();
                conn.TableCreated<Department>().Wait();
                conn.TableCreated<People>().Wait();
            }
            // 返回结果
            _output.WriteLine("OK");
        }

        [Fact]
        public void Insert()
        {
            // 定义数据
            string connectionString = $"data source={sy.IO.GetExecutionPath("temp.db")}";
            // 执行方法
            using (DatabaseConnection conn = new DatabaseConnection(DatabaseTypes.Sqlite, connectionString))
            {
                conn.Open();
                IRepository<People, string> peopleRepository = new Repository<People, string>(conn);
                IRepository<Department, string> departmentRepository = new Repository<Department, string>(conn);
                // 添加大部门
                Department? bigDepartment = departmentRepository.GetRow(d => d.Name == "大部门");
                if (bigDepartment is null)
                {
                    bigDepartment = new Department()
                    {
                        Name = "大部门"
                    };
                    departmentRepository.Insert(bigDepartment);
                }
                // 添加小部门
                Department? smallDepartment = departmentRepository.GetRow(d => d.Name == "小部门");
                if (smallDepartment is null)
                {
                    smallDepartment = new Department()
                    {
                        Name = "小部门"
                    };
                    departmentRepository.Insert(smallDepartment);
                }
                // 添加张三
                if (peopleRepository.GetRow(d => d.Name == "张三") is null)
                {
                    People people = new People()
                    {
                        Age = 10,
                        Name = "张三",
                        DepartmentId = bigDepartment.Id,
                    };
                    peopleRepository.Insert(people);
                }
                // 添加李四
                if (peopleRepository.GetRow(d => d.Name == "李四") is null)
                {
                    People people = new People()
                    {
                        Age = 20,
                        Name = "李四",
                        DepartmentId = bigDepartment.Id,
                    };
                    peopleRepository.Insert(people);
                }
                // 添加王五
                if (peopleRepository.GetRow(d => d.Name == "王五") is null)
                {
                    People people = new People()
                    {
                        Age = 30,
                        Name = "王五",
                        DepartmentId = smallDepartment.Id,
                    };
                    peopleRepository.Insert(people);
                }
            }
            // 返回结果
            _output.WriteLine("OK");
        }

        [Fact]
        public void Query()
        {
            // 定义数据
            string connectionString = $"data source={sy.IO.GetExecutionPath("temp.db")}";
            // 执行方法
            using (DatabaseConnection conn = new DatabaseConnection(DatabaseTypes.Sqlite, connectionString))
            {
                conn.Open();
                IRepository<People, string> repository = new Repository<People, string>(conn);
                var peoples = repository.GetRows(d => d.Age > 8);
                // 返回结果
                _output.WriteLine($"peoples: {peoples.Count}");
            }

        }

        [Fact]
        public void EFQuery()
        {
            // 定义数据
            string connectionString = $"data source={sy.IO.GetExecutionPath("temp.db")}";
            var optionsBuilder = new DbContextOptionsBuilder<DbContext>();
            optionsBuilder.UseSqlite(connectionString);
            // 执行方法
            using (TestDbContext context = new TestDbContext(optionsBuilder.Options, connectionString))
            {
                IEfRepository<People, string> peopleRepository = new EfRepository<People, string>(context);
                IEfRepository<Department, string> departmentRepository = new EfRepository<Department, string>(context);
                var query = from p in peopleRepository.Query()
                            join d in departmentRepository.Query() on p.DepartmentId equals d.Id
                            where d.Name.Contains("大")
                            select p;
                // 返回结果
                _output.WriteLine($"peoples: {query.Count()}");
            }

        }
    }
}