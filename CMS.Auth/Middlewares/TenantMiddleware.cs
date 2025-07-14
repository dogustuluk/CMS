using CMS.Auth.Tenant;

namespace CMS.Auth.Middlewares;

public class TenantMiddleware
{
    private readonly RequestDelegate _requestDelegate;

    public TenantMiddleware(RequestDelegate requestDelegate) => _requestDelegate = requestDelegate;

    public async Task InvokeAsync(HttpContext context, ITenantAccessor tenantAccessor)
    {
        var tenantId = context.Request.Headers["X-Tenant-Id"].FirstOrDefault();
        if (string.IsNullOrEmpty(tenantId))
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("Müşteri Kimliği Hatası");
            return;
        }

        tenantAccessor.TenantId = tenantId;
        await _requestDelegate(context);
    }
}
