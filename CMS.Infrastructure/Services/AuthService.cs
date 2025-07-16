using CMS.Application.Abstractions.Services;
using CMS.Application.Common.DTOs;
using CMS.Application.Features.Commands.Login;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace CMS.Infrastructure.Services;
public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public AuthService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<LoginResponse> LoginAsync(string email, string password)
    {
        var authApi = _configuration["AuthAPI:BaseURL"];

        var request = new { Email = email, Password = password };

        var response = await _httpClient.PostAsJsonAsync($"{authApi}/auth/createToken", request);

        if (!response.IsSuccessStatusCode)
        {
            return new LoginResponse(false, "Giriş başarısız");
        }

        var tokenData = await response.Content.ReadFromJsonAsync<TokenResultDto>();
        return new LoginResponse(true, tokenData?.AccessToken, tokenData?.RefreshToken, tokenData?.Message);
    }
}
