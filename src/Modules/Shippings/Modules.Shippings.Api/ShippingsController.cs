using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Modules.Shippings.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ShippingsController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
