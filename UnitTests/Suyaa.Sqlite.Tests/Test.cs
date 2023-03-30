using Suyaa.Data;
using Suyaa.Sqlite.Tests.Entities;
using Xunit.Abstractions;

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
                People people = new People()
                {
                    Age = 10,
                    Name = "张三",
                };
                IRepository<People, string> repository = new Repository<People, string>(conn);
                repository.Insert(people);
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
    }
}