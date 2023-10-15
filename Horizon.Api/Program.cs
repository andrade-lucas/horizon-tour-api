using Horizon.Api.Extensions;
using Horizon.Auth;
using Horizon.Domain;
using Horizon.Infra.Context;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwagger();
builder.Services.AddCors(cors =>
{
    cors.AddDefaultPolicy(policy => policy
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
    );
});
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuth();
builder.Services.AddDomain();
builder.Services.ConfigureAuth(builder.Configuration);
builder.Services.ConfigureServices();
builder.Services.ConfigureValidations();
builder.Services.ConfigureRepositories();

builder.Services.AddScoped<IDB, MySqlDb>();

var app = builder.Build();
app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
