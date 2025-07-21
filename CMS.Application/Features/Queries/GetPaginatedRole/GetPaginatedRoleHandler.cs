using MediatR;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System.Text.Json;

namespace CMS.Application.Features.Queries.GetPaginatedRole;
public class GetPaginatedRoleHandler : IRequestHandler<GetPaginatedRoleCommand, GetPaginatedRoleResponse>
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public GetPaginatedRoleHandler(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }


    public async Task<GetPaginatedRoleResponse> Handle(GetPaginatedRoleCommand request, CancellationToken cancellationToken)
    {
        var baseUrl = _configuration["AuthAPI:BaseURL"];
        var url = $"{baseUrl}/auth/getRole?page={request.Page}&pageSize={request.PageSize}";

        var response = await _httpClient.PostAsJsonAsync(url, request, cancellationToken);
        response.EnsureSuccessStatusCode();

        var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        var jsonDoc = await JsonDocument.ParseAsync(stream, cancellationToken: cancellationToken);

        return new GetPaginatedRoleResponse(jsonDoc);
    }
}