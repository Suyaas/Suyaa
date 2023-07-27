using Suyaa.Ranges;
using Suyaa.Tests.Datas;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Xunit;
using Xunit.Abstractions;

namespace Suyaa.Tests
{
    public enum TestOut : int
    {
        None = 0,
        OK = 1,
        Fail = 2,
    }

    public class Test
    {
        private readonly ITestOutputHelper _output;

        public Test(ITestOutputHelper testOutput)
        {
            _output = testOutput;
        }

        [Fact]
        public void Terminal_Execute()
        {
            // 定义数据
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            Encoding encoding = Encoding.GetEncoding("GB2312");
            // 执行方法
            string res = sy.Terminal.Execute("ping", "www.baidu.com", encoding);
            _output.WriteLine(res);
            // 返回结果
            IntegerRange range = new(10, 100, 2);
            foreach (var num in range)
            {
                _output.WriteLine($"{num}");
            }
            var dr = new DoubleRange(10, 20, 0.1);
            foreach (var num in dr)
            {
                _output.WriteLine($"{num}");
            }
        }

        [Fact]
        public void Output_Enum()
        {
            // 定义数据
            var outs = TestOut.OK;
            // 执行方法
            _output.WriteLine(outs.ToString());
            // 返回结果
        }

        [Fact]
        public void ToString_Compare()
        {
            string sz = string.Empty;
            StringBuilder sb = new();
            // 第一组测试
            int t1 = Environment.TickCount;
            double sum = 0;
            for (double i = 0.00; i < 1000000; i += 0.01)
            {
                sum += i;
                sz = "sum:" + sum.ToString();
            }
            int t2 = Environment.TickCount;
            _output.WriteLine($"第一组：{sz} / {t2 - t1} 毫秒");
            // 第二组测试
            t1 = Environment.TickCount;
            sum = 0;
            for (double i = 0.00; i < 1000000; i += 0.01)
            {
                sum += i;
                sz = "sum:" + sum;
            }
            t2 = Environment.TickCount;
            _output.WriteLine($"第二组：{sz} / {t2 - t1} 毫秒");
            // 第三组测试
            t1 = Environment.TickCount;
            sum = 0;
            for (double i = 0.00; i < 1000000; i += 0.01)
            {
                sum += i;
                sz = $"sum:{sum}";
            }
            t2 = Environment.TickCount;
            _output.WriteLine($"第三组：{sz} / {t2 - t1} 毫秒");
            // 第四组测试
            t1 = Environment.TickCount;
            sum = 0;
            for (double i = 0.00; i < 1000000; i += 0.01)
            {
                sum += i;
                sz = "sum:" + Convert.ToString(sum);
            }
            t2 = Environment.TickCount;
            _output.WriteLine($"第四组：{sz} / {t2 - t1} 毫秒");
            // 第五组测试
            t1 = Environment.TickCount;
            sum = 0;
            for (double i = 0.00; i < 1000000; i += 0.01)
            {
                sum += i;
                sz = $"sum:{Convert.ToString(sum)}";
            }
            t2 = Environment.TickCount;
            _output.WriteLine($"第五组：{sz} / {t2 - t1} 毫秒");
            // 第六组测试
            t1 = Environment.TickCount;
            sum = 0;
            for (double i = 0.00; i < 1000000; i += 0.01)
            {
                sum += i;
                sb.Clear();
                sb.Append("sum:");
                sb.Append(Convert.ToString(sum));
                sz = sb.ToString();
            }
            t2 = Environment.TickCount;
            _output.WriteLine($"第六组：{sz} / {t2 - t1} 毫秒");
            // 第七组测试
            t1 = Environment.TickCount;
            sum = 0;
            for (double i = 0.00; i < 1000000; i += 0.01)
            {
                sum += i;
                sb.Clear();
                sb.Append("sum:");
                sb.Append(sum);
                sz = sb.ToString();
            }
            t2 = Environment.TickCount;
            _output.WriteLine($"第七组：{sz} / {t2 - t1} 毫秒");
        }

        [Fact]
        public void TypeCode()
        {
            // 定义数据
            int? a1 = null;
            _output.WriteLine("a1:" + Type.GetTypeCode(a1?.GetType()).ToString());
            int? a2 = 1;
            _output.WriteLine("a2:" + Type.GetTypeCode(a2.GetType()).ToString());

            _output.WriteLine("test:" + Type.GetTypeCode(typeof(Test)).ToString());

            string? s1 = null;
            _output.WriteLine("s1:" + Type.GetTypeCode(s1?.GetType()).ToString());
            string? s2 = string.Empty;
            _output.WriteLine("s2:" + Type.GetTypeCode(s2?.GetType()).ToString());
            string? s3 = nameof(Test);
            _output.WriteLine("s3:" + Type.GetTypeCode(s3?.GetType()).ToString());
            _output.WriteLine("TypeCode:" + Type.GetTypeCode(typeof(TypeCode)).ToString());

            _output.WriteLine("int?:" + Type.GetTypeCode(typeof(int?)).ToString());
            _output.WriteLine(typeof(int?).FullName);

        }

        [Fact]
        public void Clone_Test()
        {
            People people1 = new()
            {
                Age = 1,
                Name = "张三",
            };
            var people2 = sy.Typer.Clone(people1);
            people2.Name = "李四";
            _output.WriteLine(JsonSerializer.Serialize(people1, new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                //WriteIndented = true,
            }));
            _output.WriteLine(JsonSerializer.Serialize(people2, new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                //WriteIndented = true,
            }));
        }

        [Fact]
        public void Fill_Test()
        {
            People people1 = new()
            {
                Age = 1,
                Name = "张三",
                IsMan = false,
            };
            People people2 = new();
            sy.Typer.Copy(people1, people2);
            people2.Name = "李四";
            People people3 = sy.Typer.Clone(people1);
            _output.WriteLine(JsonSerializer.Serialize(people1, new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                //WriteIndented = true,
            }));
            _output.WriteLine(JsonSerializer.Serialize(people2, new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                //WriteIndented = true,
            }));
            _output.WriteLine(JsonSerializer.Serialize(people3, new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                //WriteIndented = true,
            }));
            _output.WriteLine("IsMan:" + people2.IsMan);
        }

        [Fact]
        public void UnNull()
        {
            try
            {
                People? str = null;
                People str2 = str!;
                _output.WriteLine($"{str} {str2}");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"{ex}");
            }
        }
    }
}