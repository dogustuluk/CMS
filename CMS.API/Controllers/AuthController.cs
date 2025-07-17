using CMS.Application.Features.Commands.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

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

        var accessCookieOpt = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTime.UtcNow.AddMinutes(60)
        };

        var refreshCookie = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTime.UtcNow.AddDays(3)
        };

        Response.Cookies.Append("accessToken", result.AccessToken, accessCookieOpt);
        Response.Cookies.Append("refreshToken", result.RefreshToken, refreshCookie);


        return Ok(result);

    }

    [HttpGet("me")]
    public async Task<IActionResult> Me()
    {
        var accessToken = Request.Cookies["accessToken"];
        if (string.IsNullOrEmpty(accessToken))
            return Unauthorized();

        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(accessToken);
        var sub = token.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
        var email = token.Claims.FirstOrDefault(c => c.Type == "email")?.Value;

        return Ok(new { sub, email });
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var expiredCookieOpt = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTimeOffset.UtcNow.AddDays(-1)
        };

        Response.Cookies.Append("accessToken", "", expiredCookieOpt);
        Response.Cookies.Append("refreshToken", "", expiredCookieOpt);

        return Ok(new { message = "Çıkış Yapıldı" });

    }

}