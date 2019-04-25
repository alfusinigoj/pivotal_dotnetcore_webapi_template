using Autofac;
using Pivotal.NetCore.WebApi.Template.Extensions;
using Pivotal.NetCore.WebApi.Template.Features;
using Xunit;

namespace Pivotal.NetCore.WebApi.Template.Unit.Tests.Extensions
{
    public class ContainerBuilderExtensionsTests
    {
        [Fact]
        public void Test_RegisterMediatRHandlers()
        {
            var builder = new ContainerBuilder();

            builder.RegisterMediatRHandlers();

            var container = builder.Build(Autofac.Builder.ContainerBuildOptions.None);

            Assert.True(container.IsRegistered<ValuesHandler>());
        }
    }
}
