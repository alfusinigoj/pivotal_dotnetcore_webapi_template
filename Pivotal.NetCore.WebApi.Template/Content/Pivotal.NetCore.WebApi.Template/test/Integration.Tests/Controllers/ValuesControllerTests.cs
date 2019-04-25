using FluentAssertions;
using Pivotal.NetCore.WebApi.Template.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Xunit;

namespace Pivotal.NetCore.WebApi.Template.Integration.Tests.Controllers
{
    public class ValuesControllerTests
    {
        [Fact]
        public async Task GetValues_ShouldGetSuccessStatusCode()
        {
            using (var testServer = TestHelper.GetTestServer())
            {
                var response = await testServer.CreateRequest("/api/Values/v1/12345678/123").GetAsync();
                response.IsSuccessStatusCode.Should().BeTrue();
            }

        }
        [Fact]
        public async Task GetValues_ShouldReturnCorrectResponse()
        {
            using (var testServer = TestHelper.GetTestServer())
            {
                var response = await testServer.CreateRequest("/api/Values/v1/12345678/123").GetAsync();
                var content = response.Content.ReadAsStringAsync();
                var formResponse = JsonConvert.DeserializeObject<ValuesResponse>(content.Result);
                formResponse.Result.Should().Be(JsonConvert.SerializeObject(new ValuesRequest { Param1 = "12345678", Param2 = "123" }));
            }
        }
    }
}
