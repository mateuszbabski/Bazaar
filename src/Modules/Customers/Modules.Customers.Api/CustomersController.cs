using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modules.Customers.Application.Commands.SignInCustomerCommand;
using Modules.Customers.Application.Commands.SignUpCustomerCommand;
using Shared.Abstractions.Mediation.Commands;
using Swashbuckle.AspNetCore.Annotations;

namespace Modules.Customers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public CustomersController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost("sign-up")]
        [SwaggerOperation("Sign up")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignUpAsync([FromBody] SignUpCommand command)
        {
            await _commandDispatcher.SendAsync(command);
            return Ok();
        }

        [HttpPost("sign-in")]
        [SwaggerOperation("Sign in")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignInAsync([FromBody] SignInCommand command)
        {
            await _commandDispatcher.SendAsync(command);
            return Ok();
        }
    }
}
