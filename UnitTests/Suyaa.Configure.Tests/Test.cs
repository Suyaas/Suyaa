using Suyaa.Configure.Tests.Setttings;
using System.Text.Encodings.Web;
using System.Text.Json;
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

        [Fact]
        public void ProfileSetting()
        {
            var random = new Random();
            var setting = sy.Configure.LoadProfileSetting<TestSetting>(sy.IO.GetFullPath("./sss.conf"));
            //_output.WriteLine(setting.Config.Name);
            _output.WriteLine(JsonSerializer.Serialize(setting.Config, new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true,
            }));
            setting.Config.Name = "ÕÅÈý";
            setting.Config.Age = random.Next(1, 100);
            setting.Config.Ball.Name = "Ð¡Çò";
            setting.Config.Ball.Params.Length = random.Next(10, 20);
            setting.Config.Ball.Params.Size = random.Next(10, 20);
            setting.Save();
            _output.WriteLine(JsonSerializer.Serialize(setting.Config, new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true,
            }));
            Assert.NotNull(setting);
        }
    }
}