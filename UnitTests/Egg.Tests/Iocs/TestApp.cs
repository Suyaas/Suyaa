using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuyaaTest.Iocs
{
    public sealed class TestApp
    {
        private readonly ITestCore _iocTestCore;

        public TestApp(ITestCore iocTestCore)
        {
            _iocTestCore = iocTestCore;
            TestProperty = new DefaultTestProperty();
        }

        public string GetCoreName() => _iocTestCore.GetName();

        public ITestProperty TestProperty { get; set; }
    }
}
