using CMS.Application.Features.Commands.Login;

namespace CMS.Application.Abstractions.Services;
public interface IAuthService
{
    Task<LoginResponse> LoginAsync(string email, string password);
}
