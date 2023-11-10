using IdentityService.Application.RequestsEventsHandlers.MediatrForAPI.Account.Commands.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.API.Controllers
{
    [ApiController]
    [Route("api/identityservice/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;


        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // username: admin
        // password: 123
        [HttpPost("GetToken")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model) => Ok(await _mediator.Send(model));

    }
}