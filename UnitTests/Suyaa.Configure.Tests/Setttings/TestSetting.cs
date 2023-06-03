using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Configure.Tests.Setttings
{
    [Description("测试配置")]
    public class TestSetting : IConfig
    {
        [Description("姓名")]
        public string Name { get; set; } = string.Empty;

        [Description("年龄")]
        public int Age { get; set; } = 0;

        [Description("小球")]
        public TestGood Ball { get; set; } = new TestGood();

        public void Default()
        {
            //throw new NotImplementedException();
        }
    }

    [Description("物品信息")]
    public class TestGood
    {
        [Description("名称")]
        public string Name { get; set; } = string.Empty;

        [Description("物品参数")]
        public TestGoodParams Params { get; set; } = new TestGoodParams() { };
    }

    //[Description("物品参数信息")]
    public class TestGoodParams
    {

        [Description("尺寸")]
        public int Size { get; set; } = 0;

        [Description("长度")]
        public int Length { get; set; } = 0;
    }
}
