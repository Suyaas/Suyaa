using Suyaa.Logs;
using Suyaa.Logs.Loggers;
using System.Diagnostics;
using Xunit.Abstractions;

namespace Suyaa.Logs.Tests
{
    public class Test
    {
        private readonly ITestOutputHelper _output;

        public Test(ITestOutputHelper testOutput)
        {
            _output = testOutput;
        }

        [Fact]
        public void Log()
        {
            sy.Logger.GetCurrentLogger()
                .Use((string mesage) =>
                    {
                        Debug.WriteLine(mesage);
                    })
                .Use(new FileLogger(sy.IO.GetExecutionPath("log")))
                .Use(msg => Debug.WriteLine(msg));
            sy.Logger.Info("≤‚ ‘");
        }
    }
}