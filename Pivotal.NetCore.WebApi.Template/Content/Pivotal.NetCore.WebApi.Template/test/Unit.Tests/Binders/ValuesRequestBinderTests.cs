using FluentAssertions;
using Pivotal.NetCore.WebApi.Template.Binders;
using Pivotal.NetCore.WebApi.Template.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Pivotal.NetCore.WebApi.Template.Unit.Tests.Binders
{
    public class ValuesRequestBinderTests
    {
        [Fact]
        public void Test_BinderType()
        {
            var binder = new ValuesRequestBinder();
            Assert.True(binder is IModelBinder);
        }

        [Fact]
        public void Test_BinderShouldReturnTaskCompletedIfNoException()
        {
            var binder = new ValuesRequestBinder();

            var bindingContext = new ModelBindingContextStub();

            Assert.Equal(TaskStatus.RanToCompletion, binder.BindModelAsync(bindingContext).Status);
        }

        [Fact]
        public void Test_BinderShouldCreateTheModelIfRouteDataIsValid()
        {
            var binder = new ValuesRequestBinder();

            var bindingContext = new ModelBindingContextStub();

            bindingContext.ActionContext.RouteData.Values["param1"] = "12345678";
            bindingContext.ActionContext.RouteData.Values["param2"] = "123";

            binder.BindModelAsync(bindingContext);

            var expectedModel = new ValuesRequest() { Param1 = "12345678", Param2 = "123" };

            bindingContext.Result.Model.Should().BeEquivalentTo(expectedModel);
        }

        [Fact]
        public void Test_BinderShouldReturnTaskFaultedIfAnyUnhandledExceptionOccurs()
        {
            var binder = new ValuesRequestBinder();

            var bindingContext = new ModelBindingContextStub();

            bindingContext.ActionContext.RouteData = null;

            Assert.Equal(TaskStatus.Faulted, binder.BindModelAsync(bindingContext).Status);

            bindingContext.Result.Should().BeEquivalentTo(ModelBindingResult.Failed());
        }

        class ModelBindingContextStub : ModelBindingContext
        {
            public override ActionContext ActionContext { get; set; } = new ActionContext() { RouteData = new Microsoft.AspNetCore.Routing.RouteData() };
            public override string BinderModelName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public override BindingSource BindingSource { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public override string FieldName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public override bool IsTopLevelObject { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public override object Model { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public override ModelMetadata ModelMetadata { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public override string ModelName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public override ModelStateDictionary ModelState { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public override Func<ModelMetadata, bool> PropertyFilter { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public override ValidationStateDictionary ValidationState { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public override IValueProvider ValueProvider { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public override ModelBindingResult Result { get; set; }

            public override NestedScope EnterNestedScope(ModelMetadata modelMetadata, string fieldName, string modelName, object model)
            {
                throw new NotImplementedException();
            }

            public override NestedScope EnterNestedScope()
            {
                throw new NotImplementedException();
            }

            protected override void ExitNestedScope()
            {
                throw new NotImplementedException();
            }
        }
    }
}
