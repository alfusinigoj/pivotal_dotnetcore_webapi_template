using Pivotal.NetCore.WebApi.Template.Binders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Pivotal.NetCore.WebApi.Template.Models
{
    [ModelBinder(typeof(ValuesRequestBinder))]
    public class ValuesRequest : IRequest, IRequest<ValuesResponse>
    {
        public string Param1 { get; set; }
        public string Param2 { get; set; }
    }
}
