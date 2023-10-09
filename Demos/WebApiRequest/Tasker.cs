using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiRequest
{
    public static class Tasker
    {
        private static async Task RunAsync()
        {
            string content = await sy.Http.GetAsync("https://www.baidu.com");
            sy.Console.WriteLine(content);
        }
        public static async void Run()
        {
            await RunAsync();
        }
    }
}
