using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modules.Customers.Application.Commands.SignInCustomerCommand;
using Modules.Customers.Application.Commands.SignUpCustomerCommand;
using Shared.Abstractions.Dispatchers;
using Swashbuckle.AspNetCore.Annotations;

namespace Modules.Customers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public CustomersController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost("sign-up")]
        [SwaggerOperation("Sign up")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignUpAsync([FromBody] SignUpCommand command)
        {
            await _dispatcher.SendAsync(command);
            return Ok();
        }

        [HttpPost("sign-in")]
        [SwaggerOperation("Sign in")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignInAsync([FromBody] SignInCommand command)
        {
            await _dispatcher.SendAsync(command);
            return Ok();
        }
    }
}
