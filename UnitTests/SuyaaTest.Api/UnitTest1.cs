using SuyaaTest.Api.Datas;
using Xunit.Abstractions;

namespace SuyaaTest.Api
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper _output;

        public UnitTest1(ITestOutputHelper testOutput)
        {
            _output = testOutput;
        }

        [Fact]
        public void Get()
        {
            var content = sy.Http.Post("http://127.0.0.1:8888/api/services/pack/OfflinePack/Scan", "{\"workOperationId\":\"5e02eda788f84779bd2ad290e520a67e\",\"workOperationCode\":\"B50\",\"lineId\":\"3a07497c33704d29a2d715ad9cf06e5e\",\"orderId\":\"031f53c3073c45469b3f5ec20140c2c2\",\"orderNo\":\"SYCX-SRCBUBS23020004\",\"taskOrderId\":\"3a09a349d2bd26493581533270d84dad\",\"taskOrderNo\":\"WO2023022320265822\",\"containerName\":\"8823630000007\",\"packScanCheckType\":4,\"scanedItems\":[{\"id\":null,\"containerName\":\"8823630000007\",\"parentContainerName\":null,\"barcodeType\":\"CMES_UNIT_SERIALNO_TYPE_ZJTM\",\"labelType\":\"CMES_UNIT_SERIALNO_BQLX_ZJSN\",\"machineSn\":null,\"partNo\":\"001.001.0029272\",\"quantity\":0,\"isSampling\":false,\"lastModificationTime\":\"2023-07-06T17:47:19.348811\",\"lastModifierUserName\":\"¹ËÎÊÍ¨ÓÃ\",\"creationTime\":null}],\"parentContainerName\":\"58220007\",\"parentBarcodeType\":\"CMES_UNIT_SERIALNO_TYPE_ZJWX\",\"parentLabelType\":\"CMES_UNIT_SERIALNO_BQLX_XMT\"}", option =>
            {
                option.Headers.ContentType = "application/json";
                option.IsEnsureStatus = false;
            });
            _output.WriteLine(content);
            var res = sy.WebApi.Post<ApiResult>("http://127.0.0.1:8888/api/services/pack/OfflinePack/Scan",
                new { workOperationId = "5e02eda788f84779bd2ad290e520a67e" },
                opt =>
                {
                    opt.IsEnsureStatus = false;
                });
            _output.WriteLine($"{res.Success}:{res.Error?.Message}");
        }
    }
}