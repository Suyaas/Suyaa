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
            // ��������
            string connectionString = $"data source={sy.IO.GetExecutionPath("temp.db")}";
            // ִ�з���
            using (DatabaseConnection conn = new DatabaseConnection(DatabaseTypes.Sqlite, connectionString))
            {
                conn.Open();
                conn.TableCreated<Department>().Wait();
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
                IRepository<People, string> peopleRepository = new Repository<People, string>(conn);
                IRepository<Department, string> departmentRepository = new Repository<Department, string>(conn);
                // ��Ӵ���
                Department? bigDepartment = departmentRepository.GetRow(d => d.Name == "����");
                if (bigDepartment is null)
                {
                    bigDepartment = new Department()
                    {
                        Name = "����"
                    };
                    departmentRepository.Insert(bigDepartment);
                }
                // ���С����
                Department? smallDepartment = departmentRepository.GetRow(d => d.Name == "С����");
                if (smallDepartment is null)
                {
                    smallDepartment = new Department()
                    {
                        Name = "С����"
                    };
                    departmentRepository.Insert(smallDepartment);
                }
                // �������
                if (peopleRepository.GetRow(d => d.Name == "����") is null)
                {
                    People people = new People()
                    {
                        Age = 10,
                        Name = "����",
                        DepartmentId = bigDepartment.Id,
                    };
                    peopleRepository.Insert(people);
                }
                // �������
                if (peopleRepository.GetRow(d => d.Name == "����") is null)
                {
                    People people = new People()
                    {
                        Age = 20,
                        Name = "����",
                        DepartmentId = bigDepartment.Id,
                    };
                    peopleRepository.Insert(people);
                }
                // �������
                if (peopleRepository.GetRow(d => d.Name == "����") is null)
                {
                    People people = new People()
                    {
                        Age = 30,
                        Name = "����",
                        DepartmentId = smallDepartment.Id,
                    };
                    peopleRepository.Insert(people);
                }
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

        [Fact]
        public void EFQuery()
        {
            // ��������
            string connectionString = $"data source={sy.IO.GetExecutionPath("temp.db")}";
            var optionsBuilder = new DbContextOptionsBuilder<DbContext>();
            optionsBuilder.UseSqlite(connectionString);
            // ִ�з���
            using (TestDbContext context = new TestDbContext(optionsBuilder.Options, connectionString))
            {
                IEfRepository<People, string> peopleRepository = new EfRepository<People, string>(context);
                IEfRepository<Department, string> departmentRepository = new EfRepository<Department, string>(context);
                var query = from p in peopleRepository.Query()
                            join d in departmentRepository.Query() on p.DepartmentId equals d.Id
                            where d.Name.Contains("��")
                            select p;
                // ���ؽ��
                _output.WriteLine($"peoples: {query.Count()}");
            }

        }
    }
}