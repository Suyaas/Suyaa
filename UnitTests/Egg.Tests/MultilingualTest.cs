using Suyaa.Multilingual;
using Suyaa.Ranges;
using Suyaa.Tests.Datas;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Xunit;
using Xunit.Abstractions;

namespace Suyaa.Tests
{

    public class MultilingualTest
    {
        private readonly ITestOutputHelper _output;

        public MultilingualTest(ITestOutputHelper testOutput)
        {
            _output = testOutput;
        }

        [Fact]
        public void Test()
        {
            LanguagePackProvider languagePackProvider = new LanguagePackProvider();
            languagePackProvider.SetContent(Languages.SimplifiedChinese, "Exception.Null.Type", "类型'{0}'对象为空");
            LanguagePackFactory languagePackFactory = new LanguagePackFactory(languagePackProvider);
            sy.Safety.Invoke(() =>
            {
                throw new NullException<string>();
            }, ex =>
            {
                if (ex is KeyException keyException)
                {
                    _output.WriteLine(keyException.Key);
                    var pack = languagePackFactory.GetLanguagePack(Languages.SimplifiedChinese);
                    var content = pack?.GetContent(keyException.Key, keyException.OriginalParameters) ?? string.Empty;
                    if (content.IsNullOrWhiteSpace()) content = string.Format(keyException.OriginalMessage, keyException.OriginalParameters);
                    _output.WriteLine(content);
                }
            });
        }
    }
}