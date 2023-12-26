using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuyaaTest.Iocs
{
    public sealed class TestApp
    {
        private readonly TestCore _iocTestCore;

        public TestApp(TestCore iocTestCore)
        {
            _iocTestCore = iocTestCore;
        }

        public string GetCoreName() => _iocTestCore.GetName();
    }
}
