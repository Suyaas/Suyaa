using Suyaa.Ranges;
using Suyaa.Tests.Datas;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Xunit;
using Xunit.Abstractions;
using Suyaa;
using System.Diagnostics;
using Suyaa.Text;
using Microsoft.VisualStudio.TestPlatform.Utilities;

namespace Suyaa.Tests
{

    public class UUIDTest
    {
        private readonly ITestOutputHelper _output;

        public UUIDTest(ITestOutputHelper testOutput)
        {
            _output = testOutput;
        }

        [Fact]
        public void New10()
        {
            foreach (var i in new IntegerRange(0, 10))
            {
                _output.WriteLine(sy.Generator.GetNewUUID().ToString());
            }
        }

        [Fact]
        public void Parse()
        {
            if (UUID.TryParse("018995663df72f8c59000000005a8015", out UUID uuid))
            {
                _output.WriteLine(uuid.ToString());
                _output.WriteLine("time:" + uuid.Timestamp);
                _output.WriteLine(uuid.GetTime().ToFullDateTimeString());
            }
            else
            {
                _output.WriteLine("fail");
            }
        }

        [Fact]
        public void New10s()
        {
            int tick1 = Environment.TickCount;
            sy.Generator.MachineId = 0x11;
            sy.Generator.AppId = 0x22;
            for (int i = 0; i < 10000000; i++)
            {
                //_output.WriteLine(sy.Generator.GetNewUUID().ToString(false));
                string str = sy.Generator.GetNewUUID().ToString(false);
            }
            int tick2 = Environment.TickCount;
            _output.WriteLine($"ºÄÊ± {tick2 - tick1}ms");
        }

        [Fact]
        public void New10g()
        {
            foreach (var i in new IntegerRange(0, 10))
            {
                _output.WriteLine(sy.Generator.GetNewNGuid());
            }
        }

        [Fact]
        public void New10sg()
        {
            int tick1 = Environment.TickCount;
            sy.Generator.MachineId = 0x11;
            sy.Generator.AppId = 0x22;
            for (int i = 0; i < 10000000; i++)
            {
                //_output.WriteLine(sy.Generator.GetNewUUID().ToString(false));
                string str = sy.Generator.GetNewNGuid();
            }
            int tick2 = Environment.TickCount;
            _output.WriteLine($"ºÄÊ± {tick2 - tick1}ms");
        }
    }
}