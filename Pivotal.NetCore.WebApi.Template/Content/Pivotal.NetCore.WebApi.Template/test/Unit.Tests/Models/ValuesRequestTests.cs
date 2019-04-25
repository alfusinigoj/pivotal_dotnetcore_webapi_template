using Pivotal.NetCore.WebApi.Template.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Pivotal.NetCore.WebApi.Template.Unit.Tests.Models
{
    public class ValuesRequestTests
    {
        [Fact]
        public void TestRequestTypes()
        {
            var request = new ValuesRequest();
            Assert.True(request is IRequest);
            Assert.True(request is IRequest<ValuesResponse>);
        }

        [Fact]
        public void TestRequestProperties()
        {
            var request = new ValuesRequest();

            Assert.NotNull(request.GetType().GetProperty("Param1"));
            Assert.Equal("String", request.GetType().GetProperty("Param1").PropertyType.Name);

            Assert.NotNull(request.GetType().GetProperty("Param2"));
            Assert.Equal("String", request.GetType().GetProperty("Param2").PropertyType.Name);
        }

        [Fact]
        public void TestRequestBindedAttributes()
        {
            var request = new ValuesRequest();
            var attributes = Attribute.GetCustomAttributes(request.GetType());
        
            Assert.Single(attributes);
            Assert.True(attributes[0] is ModelBinderAttribute);
            Assert.Equal("ValuesRequestBinder", ((ModelBinderAttribute)attributes[0]).BinderType.Name);
        }
    }
}
