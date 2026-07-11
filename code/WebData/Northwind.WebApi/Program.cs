using Microsoft.AspNetCore.HttpLogging; // To use HttpLoggingFields.
using Northwind.EntityModels; // To use AddNorthwindDb method.
using Scalar.AspNetCore; // To use MapScalarApiReference method.
using System.ComponentModel.DataAnnotations; // To use RangeAttribute.

const string corsPolicyName = "allowWasmClient";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(documentName: "book-readers");

builder.Services.AddNorthwindDb();

builder.Services.AddValidation();

builder.Services.AddHttpLogging(options =>
{
  options.LoggingFields = HttpLoggingFields.All;
  options.RequestBodyLogLimit = 4096; // Default is 32k.
  options.ResponseBodyLogLimit = 4096; // Default is 32k.
});

builder.Services.AddCors(options =>
{
  options.AddPolicy(name: corsPolicyName,
    policy =>
    {
      policy.WithOrigins(
        "https://localhost:5132",
        "http://localhost:5133")
      .AllowAnyHeader()
      .WithMethods("GET", "POST", "PUT", "DELETE");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  //app.MapOpenApi(); // Defaults to /openapi/v1.json.
  app.MapOpenApi("/openapi/{documentName}.json");
  app.MapOpenApi("/openapi/{documentName}.yaml");
  app.MapScalarApiReference();
}

app.UseHttpLogging();

app.UseHttpsRedirection();

app.UseCors(corsPolicyName);

app.MapGet("/weatherforecast", 
  ([Range(1, 14)] int days = 5) => GetWeather(days))
  .WithName("GetWeatherForecast");

app.MapGet("/hello", () => "Hello World");

app.MapGet("/user", () => new {
  FirstName = "Bob",
  Age = 45
});

app.MapCustomers();

app.Run();

public partial class Program
{
}
