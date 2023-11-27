using Suyaa.Data;
using Suyaa.Data.Dependency;
using Suyaa.Data.Dependency.Repositories;
using Suyaa.Sqlite.Tests.Entities;
using Xunit.Abstractions;

namespace Suyaa.PostgreSQL.Tests
{
    public class Test
    {
        private const string _connString = "server=10.10.20.12;port=5432;database=test;username=dbuser;password=123456";

        private readonly ITestOutputHelper _output;

        public Test(ITestOutputHelper testOutput)
        {
            _output = testOutput;
        }

        [Fact]
        public void Create()
        {
            // ִ�з���
            using (DatabaseConnection conn = new DatabaseConnection(DbTypes.PostgreSQL, _connString))
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
            // ִ�з���
            using (DatabaseConnection conn = new DatabaseConnection(DbTypes.PostgreSQL, _connString))
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
            // ִ�з���
            using (DatabaseConnection conn = new DatabaseConnection(DbTypes.PostgreSQL, _connString))
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