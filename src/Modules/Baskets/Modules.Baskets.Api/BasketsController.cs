using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Modules.Baskets.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BasketsController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
