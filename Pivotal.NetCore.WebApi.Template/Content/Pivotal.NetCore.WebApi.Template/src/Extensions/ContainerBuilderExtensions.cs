using Autofac;
using MediatR;
using System.Reflection;

namespace Pivotal.NetCore.WebApi.Template.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static void RegisterMediatRHandlers(this ContainerBuilder builder)
        {
            var mediatrOpenTypes = new[] { typeof(IRequestHandler<,>), typeof(INotificationHandler<>) };

            foreach (var mediatrOpenType in mediatrOpenTypes)
                builder.RegisterAssemblyTypes(typeof(Program).GetTypeInfo().Assembly).AsClosedTypesOf(mediatrOpenType)
                    .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(Program).GetTypeInfo().Assembly).AsSelf().AsImplementedInterfaces();
        }
    }
}
