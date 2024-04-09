using DayBook.DAL.DependencyInjection;
using DayBook.Application.DependencyInjection;
using Serilog;
using DayBook.Api;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddSwagger();

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddDataAccessLayer(configuration);
builder.Services.AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(s =>
    {
        s.SwaggerEndpoint("/swagger/v1/swagger.json", "DayBook Swagger v1.0");
        s.SwaggerEndpoint("/swagger/v2/swagger.json", "DayBook Swagger v2.0");
        s.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.UseAuthorization();

app.MapControllers();

app.Run();
