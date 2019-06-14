using Pivotal.NetCore.WebApi.Template.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Steeltoe.Common.HealthChecks;
using Steeltoe.Management.Endpoint.Health;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Pivotal.NetCore.WebApi.Template.Unit.Tests.Extensions
{
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void Test_AddActuatorsAndHealthContributors()
        {
            IServiceCollection services = new ServiceCollection();

            var configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            configuration["ConnectionStrings:Database1"] = "Database = Database1; Server = Server1; Integrated Security = SSPI;";

            services.AddActuatorsAndHealthContributors(configuration);

            var healthContributors = services.BuildServiceProvider().GetServices<IHealthContributor>().ToList();
            healthContributors.Should().Contain(x => x.Id.StartsWith("SqlServer"));

            services.BuildServiceProvider().GetServices<IHealthContributor>().Should().Contain(x => x.Id.StartsWith("SqlServer"));
            Assert.NotNull(services.BuildServiceProvider().GetService<HealthEndpoint>());
        }

        class ServiceStub
        {

        }
    }
}
