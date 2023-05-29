using Suyaa.Configure.Tests.Setttings;
using Xunit.Abstractions;

namespace Suyaa.Configure.Tests
{
    public class Test
    {
        private readonly ITestOutputHelper _output;

        public Test(ITestOutputHelper testOutput)
        {
            _output = testOutput;
        }

        [Fact]
        public void ContentSetting()
        {
            var setting = sy.Configure.LoadJsonSetting<TestSetting>("{\"Name\":\"qwer\"}");
            _output.WriteLine(setting?.Config.Name);
            Assert.NotNull(setting);
        }
    }
}