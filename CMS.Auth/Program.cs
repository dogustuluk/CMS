using CMS.Auth.Extensions;
using CMS.Auth.Features.Register;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


//register iþlemi
app.MapPost("/auth/register", async (RegisterUserCommand command, IMediator mediator) =>
{
    var response = await mediator.Send(command);
    if (!response.Success)
        return Results.BadRequest(new { error = response.ErrorMessage });
    return Results.Ok(new { message = "Kayýt Baþarýlý" });
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
