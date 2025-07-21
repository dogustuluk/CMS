using CMS.Application.Features.Queries.GetPaginatedRole;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CMS.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IMediator _mediator;

    public RoleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetRoles([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetPaginatedRoleCommand(page, pageSize));
        return Content(result.json.RootElement.ToString(), "application/json");
    }
}
