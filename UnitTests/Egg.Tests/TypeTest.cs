using Suyaa.Ranges;
using Suyaa.Tests.Datas;
using SuyaaTest.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Suyaa;

namespace SuyaaTest
{
    /// <summary>
    /// 类型测试
    /// </summary>
    public class TypeTest
    {
        private readonly ITestOutputHelper _output;

        public TypeTest(ITestOutputHelper testOutput)
        {
            _output = testOutput;
        }

        [Fact]
        public void Interface()
        {
            var peopleType = typeof(People);
            var entityType = typeof(Entity<>);
            var entityInterfaceType = typeof(IEntity<>);
            _output.WriteLine($"Entity<>.HasInterface(IEntity<>) : {entityType.HasInterface(entityInterfaceType)}");
            _output.WriteLine($"People.HasInterface(IEntity<>): {peopleType.HasInterface(entityInterfaceType)}");
            _output.WriteLine($"IEntity<>.IsAssignableFrom(People): {entityInterfaceType.IsAssignableFrom(peopleType)}");

            _output.WriteLine($"People.IsBased(IEntity<>) : {peopleType.IsBased(entityInterfaceType)}");
            _output.WriteLine($"People.IsBased(Entity<>): {peopleType.IsBased(entityType)}");
            _output.WriteLine($"Entity<>.IsAssignableFrom(People): {entityType.IsAssignableFrom(peopleType)}");
        }
    }
}
