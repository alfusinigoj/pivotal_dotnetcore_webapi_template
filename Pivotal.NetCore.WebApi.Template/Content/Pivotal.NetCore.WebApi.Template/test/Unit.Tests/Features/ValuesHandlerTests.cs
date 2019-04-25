using Pivotal.NetCore.WebApi.Template.Features;
using Pivotal.NetCore.WebApi.Template.Models;
using MediatR;
using Newtonsoft.Json;
using System.Threading;
using Xunit;

namespace Pivotal.NetCore.WebApi.Template.Unit.Tests.Features.Values
{
    public class ValuesHandlerTests
    {
        [Fact]
        public void Test_IsTypeOfIRequestHandler()
        {
            var handler = new ValuesHandler();
            Assert.True(handler is IRequestHandler<ValuesRequest, ValuesResponse>);
        }

        [Fact]
        public async void Test_HandleReturnsCorrectResponse()
        {
            var handler = new ValuesHandler();
            var request = new ValuesRequest() { Param1 = "param1", Param2 = "param2" };
            var response = await handler.Handle(request, new CancellationToken());

            Assert.Equal(JsonConvert.SerializeObject(request), response.Result);
        }
    }
}
