using FluentValidation;
using Pivotal.NetCore.WebApi.Template.Models;
using Pivotal.NetCore.WebApi.Template.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Pivotal.NetCore.WebApi.Template.Unit.Tests.Validators
{
    public class ValuesRequestValidatorTests
    {
        ValuesRequestValidator validator;
        ValuesRequest request;

        public ValuesRequestValidatorTests()
        {
            validator = new ValuesRequestValidator();
        }

        [Fact]
        public void Test_IfValidatorIsOfTypeAbstractValidator()
        {
            Assert.True(validator is AbstractValidator<ValuesRequest>);
        }

        [Theory]
        [InlineData("1234as", false)]
        [InlineData("123456789asas", false)]
        [InlineData("123456AA", false)]
        public void Test_ValidatorValidatesIfParam1Of8Digits(string value, bool isValid)
        {
            request = new ValuesRequest
            {
                Param1 = value,
                Param2 = "1234"
            };
            Assert.Equal(isValid, validator.Validate(request).IsValid);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("1234", true)]
        public void Test_ValidatorValidatesIfParam2IsNotNullAndNotEmpty(string value, bool isValid)
        {
            request = new ValuesRequest
            {
                Param1 = "12345678",
                Param2 = value
            };
            Assert.Equal(isValid, validator.Validate(request).IsValid);
        }
    }
}
