using CMS.Auth.Extensions;
using CMS.Auth.Features.Login;
using CMS.Auth.Features.RefreshToken;
using CMS.Auth.Features.Register;
using CMS.Auth.Infrastructure.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices(builder.Configuration);

#region JWT
var jwtSettings = builder.Configuration.GetSection("Token").Get<JWTSettings>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero, // Token süresi tam dolunca expired olur

        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
    };
});
#endregion

//api rate limit services
builder.Services.AddRateLimiter(async options =>
{
    options.AddFixedWindowLimiter(policyName: "FixedWindowPolicy", configureOptions: configOptions =>
    {
        configOptions.QueueLimit = 0; //yeni istekleri sýraya almaz; direkt reddeder.
        configOptions.PermitLimit = 10; //maks 5 istek için.
        configOptions.Window = TimeSpan.FromMinutes(1); //1 dakika aralýk
        configOptions.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
    });

    options.OnRejected = async (context, token) =>
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        context.HttpContext.Response.ContentType = "application/json";
        var response = new
        {
            error = "Çok fazla istek gönderildi. Lütfen daha sonra tekrar deneyiniz."
        };

        await context.HttpContext.Response.WriteAsJsonAsync(response, cancellationToken: token);
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


#region minimal api
//register iþlemi
app.MapPostEndpoint<RegisterUserCommand, RegisterUserResponse>("/auth/register");

//Token Create
app.MapPostEndpoint<AuthenticateUserCommand, AuthenticateUserResponse>("/auth/createToken", rateLimitPolicyName: "FixedWindowPolicy");

//Create Token By Refresh Token
app.MapPostEndpoint<RefreshTokenCommand, RefreshTokenResponse>("auth/createTokenByRefreshToken", rateLimitPolicyName: "FixedWindowPolicy");

#endregion


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRateLimiter();

app.Run();
