using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Modules.Shops.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShopsController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
