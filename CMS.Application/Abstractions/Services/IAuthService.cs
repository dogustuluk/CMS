using CMS.Application.Features.Commands.Login;
using CMS.Application.Features.Commands.Register;

namespace CMS.Application.Abstractions.Services;
public interface IAuthService
{
    Task<LoginResponse> LoginAsync(string email, string password);
    Task<RegisterResponse> RegisterAsync(string username, string email, string password, int role, string tenantName, string domain);
}
