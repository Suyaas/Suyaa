using Suyaa.Ranges;
using Suyaa.Tests.Datas;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Xunit;
using Xunit.Abstractions;
using Suyaa;
using System.Diagnostics;

namespace Suyaa.Tests
{

    public class HttpTest
    {
        private readonly ITestOutputHelper _output;

        public HttpTest(ITestOutputHelper testOutput)
        {
            _output = testOutput;
        }

        [Fact]
        public void Get()
        {
            var content = sy.Http.Get("https://www.baidu.com", opt =>
            {
                opt.Headers.Set("aaa", "sss");
                opt.Cookies.Set("BDUSS", "5mdkR6QnJDajIwZWpoS1dFbHY5Nzd1SEVNRzdZcm9zVXBLc3JXbkhUaXM0WHBqSVFBQUFBJCQAAAAAAAAAAAEAAAB1kz0Fd2lucmVud2luAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAKxUU2OsVFNjZ3");
                opt.OnResponse(resp =>
                {
                    foreach (var cookie in resp.GetCookies())
                    {
                        _output.WriteLine(cookie.Key + "=" + cookie.Value);
                    }
                    return true;
                });
            });
            _output.WriteLine(content);
        }

        [Fact]
        public void Post()
        {
            string content = @"------WebKitFormBoundaryuVY5GTAY9pzkKx4F
Content-Disposition: form-data; name=""ref""

BbuRd1bPtd
------WebKitFormBoundaryuVY5GTAY9pzkKx4F
Content-Disposition: form-data; name=""submit""

点击下载
------WebKitFormBoundaryuVY5GTAY9pzkKx4F--
";
            byte[] bytes = Encoding.UTF8.GetBytes(content);
            var res = sy.Http.Post("https://xlnote.cn/Proxy?http://www.rep2p.com:80/load.php", bytes, opt =>
            {
                opt.Headers.ContentType = "multipart/form-data; boundary=----WebKitFormBoundaryuVY5GTAY9pzkKx4F";
                opt.Headers.ContentLength = bytes.Length;
            });
            _output.WriteLine(res);
        }

        [Fact]
        public void GetTest()
        {
            foreach (var a in new IntegerRange(0, 2))
            {
                var content = sy.Http.Get("http://127.0.0.1:8888/api/services/bas/CacheTest2/Test");
                _output.WriteLine(content);
                Thread.Sleep(10);
            }
        }

        [Fact]
        public void PostTest()
        {
            foreach (var a in new IntegerRange(0, 2))
            {
                var content = sy.Http.Post("http://127.0.0.1:8888/api/services/pack/OfflinePack/Scan", Encoding.UTF8.GetBytes("{\"workOperationId\":\"5e02eda788f84779bd2ad290e520a67e\",\"workOperationCode\":\"B50\",\"lineId\":\"3a07497c33704d29a2d715ad9cf06e5e\",\"orderId\":\"031f53c3073c45469b3f5ec20140c2c2\",\"orderNo\":\"SYCX-SRCBUBS23020004\",\"taskOrderId\":\"3a09a349d2bd26493581533270d84dad\",\"taskOrderNo\":\"WO2023022320265822\",\"containerName\":\"8823630000007\",\"packScanCheckType\":4,\"scanedItems\":[{\"id\":null,\"containerName\":\"8823630000007\",\"parentContainerName\":null,\"barcodeType\":\"CMES_UNIT_SERIALNO_TYPE_ZJTM\",\"labelType\":\"CMES_UNIT_SERIALNO_BQLX_ZJSN\",\"machineSn\":null,\"partNo\":\"001.001.0029272\",\"quantity\":0,\"isSampling\":false,\"lastModificationTime\":\"2023-07-06T17:47:19.348811\",\"lastModifierUserName\":\"顾问通用\",\"creationTime\":null}],\"parentContainerName\":\"58220007\",\"parentBarcodeType\":\"CMES_UNIT_SERIALNO_TYPE_ZJWX\",\"parentLabelType\":\"CMES_UNIT_SERIALNO_BQLX_XMT\"}"), option =>
                {
                    option.Headers.ContentType = "application/json";
                    option.IsEnsureStatus = false;
                });
                _output.WriteLine(content);
                Thread.Sleep(10);
            }
        }

        [Fact]
        public void Download()
        {
            var folder = sy.IO.GetFullPath("./down");
            sy.IO.CreateFolder(folder);
            var file = sy.IO.CombinePath(folder, $"Suyaas_Suyaa_main.zip");
            //if (sy.IO.FileExists(file)) sy.IO.DeleteFile(file);
            sy.Http.Download("https://github.com/Suyaas/Suyaa/archive/refs/heads/main.zip", file, opt =>
            {
                opt.OnDownload(info =>
                {
                    Debug.WriteLine(info.ReceiveSize);
                });
            });
        }
    }
}