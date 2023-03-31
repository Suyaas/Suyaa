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
            // ��������
            string connectionString = $"data source={sy.IO.GetExecutionPath("temp.db")}";
            // ִ�з���
            using (DatabaseConnection conn = new DatabaseConnection(DatabaseTypes.Sqlite, connectionString))
            {
                conn.Open();
                conn.TableCreated<People>().Wait();
            }
            // ���ؽ��
            _output.WriteLine("OK");
        }

        [Fact]
        public void Insert()
        {
            // ��������
            string connectionString = $"data source={sy.IO.GetExecutionPath("temp.db")}";
            // ִ�з���
            using (DatabaseConnection conn = new DatabaseConnection(DatabaseTypes.Sqlite, connectionString))
            {
                conn.Open();
                People people = new People()
                {
                    Age = 10,
                    Name = "����",
                };
                IRepository<People, string> repository = new Repository<People, string>(conn);
                repository.Insert(people);
            }
            // ���ؽ��
            _output.WriteLine("OK");
        }

        [Fact]
        public void Query()
        {
            // ��������
            string connectionString = $"data source={sy.IO.GetExecutionPath("temp.db")}";
            // ִ�з���
            using (DatabaseConnection conn = new DatabaseConnection(DatabaseTypes.Sqlite, connectionString))
            {
                conn.Open();
                IRepository<People, string> repository = new Repository<People, string>(conn);
                var peoples = repository.GetRows(d => d.Age > 8);
                // ���ؽ��
                _output.WriteLine($"peoples: {peoples.Count}");
            }

        }
    }
}