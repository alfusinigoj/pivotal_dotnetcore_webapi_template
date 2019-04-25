using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Steeltoe.CloudFoundry.Connector.Relational;
using Steeltoe.Common.HealthChecks;
using Steeltoe.Management.CloudFoundry;
using System.Data.SqlClient;

namespace Pivotal.NetCore.WebApi.Template.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddActuatorsAndHealthContributors(this IServiceCollection services, IConfiguration configuration)
        {
            //Add additional Health Contributors here

            services.Add(
                new ServiceDescriptor(typeof(IHealthContributor),
                    context =>
                        new RelationalHealthContributor(
                            new SqlConnection(configuration["ConnectionStrings:Database1"]),
                            context.GetService<ILogger<RelationalHealthContributor>>()
                        ),
                    ServiceLifetime.Transient)
            );

            services.AddCloudFoundryActuators(configuration);
        }

        public static IContainer GetAutofacContainer(this IServiceCollection services)
        {
            var builder = new ContainerBuilder();

            builder.RegisterMediatRHandlers();

            builder.Populate(services);

            return builder.Build();
        }
    }
}
