using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Pivotal.Extensions.Configuration.ConfigServer;
using Steeltoe.Extensions.Configuration.CloudFoundry;
using System;
using System.IO;

namespace Pivotal.NetCore.WebApi.Template
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder().UseKestrel().UseCloudFoundryHosting()
                .UseContentRoot(Directory.GetCurrentDirectory()).UseIISIntegration().UseStartup<Startup>()
                .ConfigureAppConfiguration(ConfigureAppAction()).ConfigureLogging(ConfigureLogging()).Build();

            host.Run();
        }

        private static Action<WebHostBuilderContext, IConfigurationBuilder> ConfigureAppAction()
        {
            var environment = Environment.GetEnvironmentVariable("ENV") ?? "Development";
            var clientSettings = new ConfigServerClientSettings { Environment = environment };
            return (builderContext, config) =>
                {
                    config.SetBasePath(builderContext.HostingEnvironment.ContentRootPath)
                        .AddJsonFile("appsettings.json", false, false)
                        .AddJsonFile($"appsettings.{environment}.json", true, false)
                        .AddEnvironmentVariables()
                        .AddConfigServer(clientSettings, new LoggerFactory().AddConsole(config.Build()));
                };
        }

        private static Action<WebHostBuilderContext, ILoggingBuilder> ConfigureLogging()
        {
            return (builderContext, loggingBuilder) =>
                {
                    loggingBuilder.AddConfiguration(builderContext.Configuration.GetSection("Logging"));
                    loggingBuilder.AddConsole(
                        options =>
                            {
                                options.IncludeScopes = Convert.ToBoolean(
                                    builderContext.Configuration["Logging:IncludeScopes"]);
                            });
                    loggingBuilder.AddDebug();
                };
        }
    }
}