using System.Threading;
using System.Threading.Tasks;
using Pivotal.NetCore.WebApi.Template.Models;
using MediatR;
using Newtonsoft.Json;

namespace Pivotal.NetCore.WebApi.Template.Features
{
    public class ValuesHandler : IRequestHandler<ValuesRequest, ValuesResponse>
    {
        public async Task<ValuesResponse> Handle(ValuesRequest request, CancellationToken cancellationToken)
        {
            //Task is used here just to demonstrate the usage of async/await method
            return await Task.Run(() => new ValuesResponse { Result = JsonConvert.SerializeObject(request) });
        }
    }
}