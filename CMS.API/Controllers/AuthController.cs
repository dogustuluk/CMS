using CMS.Application.Features.Commands.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CMS.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromServices] IMediator mediator, [FromBody] LoginCommand command)
    {
        var result = await mediator.Send(command);
        if (!result.Status)
            return Unauthorized(new { message = result.Message });

        return Ok(result);

    }

}