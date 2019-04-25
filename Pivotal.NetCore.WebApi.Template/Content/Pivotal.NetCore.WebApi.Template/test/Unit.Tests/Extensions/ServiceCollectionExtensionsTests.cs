using Autofac;
using Pivotal.NetCore.WebApi.Template.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Steeltoe.Common.HealthChecks;
using Steeltoe.Management.Endpoint.Health;
using System.Linq;
using Xunit;

namespace Pivotal.NetCore.WebApi.Template.Unit.Tests.Extensions
{
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void Test_GetAutofacContainer()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton(new ServiceStub());

            var container = services.GetAutofacContainer();

            Assert.NotNull(container);
            Assert.True(container is IContainer);

            container.IsRegistered(typeof(ServiceStub));
        }


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

            Assert.Contains(healthContributors, s =>s.Id=="SqlServer");

            Assert.Contains(services.BuildServiceProvider().GetServices<IHealthContributor>().ToList(), s => s.Id == "SqlServer");
            Assert.NotNull(services.BuildServiceProvider().GetService<HealthEndpoint>());
        }

        class ServiceStub
        {

        }
    }
}
