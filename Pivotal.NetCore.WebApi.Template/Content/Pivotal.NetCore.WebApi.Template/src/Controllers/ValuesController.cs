using System;
using System.Threading.Tasks;
using Pivotal.NetCore.WebApi.Template.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Pivotal.NetCore.WebApi.Template.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ValuesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [Route("v1/{param1}/{param2}")]
        public async Task<ActionResult<ValuesResponse>> GetValues(ValuesRequest request)
        {
            return await _mediator.Send<ValuesResponse>(request);
        }
    }
}