using CMS.Auth.Extensions;
using CMS.Auth.Features.Login;
using CMS.Auth.Features.RefreshToken;
using CMS.Auth.Features.Register;
using CMS.Auth.Infrastructure.Configurations;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


#region minimal api
//register iþlemi
app.MapPost("/auth/register", async (RegisterUserCommand command, IMediator mediator) =>
{
    var response = await mediator.Send(command);
    if (!response.Success)
        return Results.BadRequest(new { error = response.ErrorMessage });
    return Results.Ok(new { message = "Kayýt Baþarýlý" });
});

//Token Create
app.MapPost("/auth/createToken", async (AuthenticateUserCommand command, IMediator mediator) =>
{
    var response = await mediator.Send(command);
    if (!response.Status)
        return Results.BadRequest(new { error = response.Message });
    return Results.Ok(new { message = "Giriþ Baþarýlý" });
});

//Create Token By Refresh Token
app.MapPost("auth/createTokenByRefreshToken", async (RefreshTokenCommand command, IMediator mediator) =>
{
    var response = await mediator.Send(command);
    if (!response.Status)
        return Results.BadRequest(new { error = response.Message });
    return Results.Ok(new { message = "Ýþlem Baþarýlý" });
});
#endregion


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();
