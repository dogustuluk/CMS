using CMS.Application.Abstractions.Services;
using CMS.Application.Common.DTOs;
using CMS.Application.Features.Commands.Login;
using CMS.Application.Features.Commands.Register;
using CMS.Application.Helpers;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace CMS.Infrastructure.Services;
public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly JsonHelper _jsonHelper;

    public AuthService(HttpClient httpClient, IConfiguration configuration, JsonHelper jsonHelper)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _jsonHelper = jsonHelper;
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

    public async Task<RegisterResponse> RegisterAsync(string username, string email, string password, int role, string tenantName, string domainName)
    {
        var authApi = _configuration["AuthAPI:BaseURL"];
        var request = new { Username = username, Email = email, Password = password, Role = role, TenantName = tenantName, Domain = domainName };
        var response = await _httpClient.PostAsJsonAsync($"{authApi}/auth/register", request);

        if (!response.IsSuccessStatusCode)
            return new RegisterResponse(false, "Kayıt İşlemi Başarısız", "");

        var responseContent = await response.Content.ReadAsStringAsync();
        string? id = _jsonHelper.GetValue(responseContent, "id");

        return new RegisterResponse(true, "İşlem Başarılı", id);
    }
}