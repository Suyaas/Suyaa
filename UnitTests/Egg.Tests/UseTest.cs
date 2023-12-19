using Suyaa.Ranges;
using Suyaa.Tests.Datas;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Xunit;
using Xunit.Abstractions;
using Suyaa.Usables.Helpers;
using Suyaa;
using System.Diagnostics;

namespace SuyaaTest
{

    public class UseTest
    {
        private readonly ITestOutputHelper _output;

        public UseTest(ITestOutputHelper testOutput)
        {
            _output = testOutput;
        }

        [Fact]
        public void Run()
        {
            var toy = Use<Assembly>.Toy;
            _output.WriteLine(toy.GetExecutionFile());
            _output.WriteLine(toy.GetProductFullName());
            var info = toy.GetFileVersionInfo(toy.GetModuleFile());
            _output.WriteLine(info.GetProductVersion());
        }

    }
}