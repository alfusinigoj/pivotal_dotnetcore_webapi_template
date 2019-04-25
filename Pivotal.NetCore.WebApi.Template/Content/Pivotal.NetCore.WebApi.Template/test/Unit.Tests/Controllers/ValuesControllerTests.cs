using Pivotal.NetCore.WebApi.Template.Controllers;
using Pivotal.NetCore.WebApi.Template.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Pivotal.NetCore.WebApi.Template.Unit.Tests.Controllers
{
    public class ValuesControllerTests
    {
        Mock<IMediator> mediator;

        public ValuesControllerTests()
        {
            mediator = new Mock<IMediator>();
        }

        [Fact]
        public async void Test_GetValues_AccecptsTypeOfValuesRequest()
        {
            var controller = new ValuesController(mediator.Object);

            mediator.Setup(m => m.Send<ValuesResponse>(It.IsAny<ValuesRequest>(), It.IsAny<CancellationToken>()))
                .Returns(Task.Run<ValuesResponse>(()=> { return new ValuesResponse(); }));

            var response = await controller.GetValues(new ValuesRequest { Param1 = "123", Param2 = "234" });

            Assert.True(response.Value is ValuesResponse);
        }

        [Fact]
        public void Test_ControllerAttributes()
        {
            var controller = new ValuesController(mediator.Object);

            var attributes = Attribute.GetCustomAttributes(controller.GetType()).ToList();

            Assert.Equal(3, attributes.Count);
            Assert.Contains(attributes, a =>a is RouteAttribute);
            Assert.Contains(attributes, a =>a is ControllerAttribute);
            Assert.Contains(attributes, a =>a is ApiControllerAttribute);

            var routeAttribute = (RouteAttribute)attributes.First(a => a is RouteAttribute);
            Assert.Equal("api/[Controller]", routeAttribute.Template);
        }

        [Fact]
        public void Test_ControllerGetValuesMethodAttributes()
        {
            var controller = new ValuesController(mediator.Object);

            var members = controller.GetType().GetMembers().ToList();

            var getValueMember = members.First(a => a.Name == "GetValues");

            var attributes = Attribute.GetCustomAttributes(getValueMember).ToList();

            var routeAttribute = (RouteAttribute)attributes.First(a => a is RouteAttribute);
            Assert.Equal("v1/{param1}/{param2}", routeAttribute.Template);

            Assert.Contains(attributes, a => a is HttpGetAttribute);
        }
    }
}
