using FrontiersDemo.Api.Endpoints;
using FrontiersDemo.Api.Middleware;
using FrontiersDemo.Application;
using FrontiersDemo.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapOpenApi();
app.MapScalarApiReference();

if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.MapUserEndpoints();
app.MapReviewerEndpoints();

app.Run();
