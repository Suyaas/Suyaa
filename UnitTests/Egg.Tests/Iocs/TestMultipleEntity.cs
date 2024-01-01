using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuyaaTest.Iocs
{
    public class TestMultipleEntity
    {
        public TestMultipleEntity()
        {
            Name = string.Empty;
        }
        public TestMultipleEntity(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
