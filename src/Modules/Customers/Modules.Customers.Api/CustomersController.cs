using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modules.Customers.Application.Commands.SignInCustomerCommand;
using Modules.Customers.Application.Commands.SignUpCustomerCommand;
using Shared.Abstractions.Dispatchers;
using Shared.Abstractions.Mediation.Commands;
using Shared.Application.Auth;
using Swashbuckle.AspNetCore.Annotations;

namespace Modules.Customers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("sign-up")]
        [SwaggerOperation("Sign up")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthenticationResult>> SignUpAsync(SignUpCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("sign-in")]
        [SwaggerOperation("Sign in")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthenticationResult>> SignInAsync(SignInCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
