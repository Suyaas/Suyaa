using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Configure.Tests.Setttings
{
    public class TestSetting : IConfig
    {
        public string Name { get; set; } = string.Empty;

        public void Default()
        {
            //throw new NotImplementedException();
        }
    }
}
