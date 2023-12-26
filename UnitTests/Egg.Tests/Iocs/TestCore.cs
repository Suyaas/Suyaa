using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuyaaTest.Iocs
{
    public interface ITestCore
    {
        string GetName();
    }
    public sealed class TestCore : ITestCore
    {
        private readonly ITestGeneric<string> _testGeneric;

        public TestCore(ITestGeneric<string> testGeneric)
        {
            _testGeneric = testGeneric;
        }

        public string GetName() => _testGeneric.GetTypeName();
    }
}
