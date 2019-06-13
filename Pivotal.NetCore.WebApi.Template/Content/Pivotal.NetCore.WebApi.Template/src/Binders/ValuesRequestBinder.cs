using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Threading.Tasks;
using Pivotal.NetCore.WebApi.Template.Features;
using Pivotal.NetCore.WebApi.Template.Features.Values;

namespace Pivotal.NetCore.WebApi.Template.Binders
{
    public class ValuesRequestBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            try
            {
                var model = new GetValues.Request
                {
                    Param1 = (string)bindingContext.ActionContext.RouteData.Values["param1"],
                    Param2 = (string)bindingContext.ActionContext.RouteData.Values["param2"]
                };

                bindingContext.Result = ModelBindingResult.Success(model);
                return Task.CompletedTask;
            }
            catch (Exception exception)
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.FromException(exception);
            }
        }
    }
}
