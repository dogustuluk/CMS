using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CMS.Auth.Extensions;

public static class ClaimsPrincipalExtension
{
    public static string? GetUserId(this ClaimsPrincipal principal)
        => principal.FindFirstValue(JwtRegisteredClaimNames.Sub);

    public static string? GetTenantId(this ClaimsPrincipal principal)
        => principal.FindFirstValue("tenantId");

    public static string? GetEmail(this ClaimsPrincipal principal)
        => principal.FindFirstValue("email");

    public static string? getUsername(this ClaimsPrincipal principal)
        => principal.FindFirstValue("username");
}
