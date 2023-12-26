using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuyaaTest.Iocs
{
    public interface ITestProperty
    {
        string Name { get; }
    }
    public sealed class TestProperty : ITestProperty
    {
        public string Name => nameof(TestProperty);
    }

    public sealed class DefaultTestProperty : ITestProperty
    {
        public string Name => nameof(DefaultTestProperty);
    }
}
