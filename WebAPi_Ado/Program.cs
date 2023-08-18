using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebAPi_Ado.DataAccessLayer;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;
using Serilog;
using Serilog.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers().AddFluentValidation(c => c.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

var logFilePath = Path.Combine(builder.Environment.ContentRootPath, "Log/application.log");
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day) // Specify file path and rolling interval
    .CreateLogger();

builder.Logging.AddProvider(new SerilogLoggerProvider(Log.Logger));




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<DataAccessItems>();

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseSwagger();
//app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

////Logging at the end of the pipeline
//app.Use(async (context, next) =>
//{
//    // Example of logging a request
//    app.Logger.LogInformation($"Received request: {context.Request.Method} {context.Request.Path}");

//    await next();

//    // Example of logging a response
//    app.Logger.LogInformation($"Sent response: {context.Response.StatusCode}");
//});

app.Run();

