using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuyaaTest.Iocs
{
    public interface ITestGeneric<T>
    {
        string GetTypeName();
    }
    public sealed class TestGeneric<T> : ITestGeneric<T>
    {
        public string GetTypeName()
        {
            return typeof(T).FullName ?? string.Empty;
        }
    }
}
