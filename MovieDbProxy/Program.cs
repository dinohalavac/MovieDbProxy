using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using System.Net.Http;
using MovieDbProxy.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular4200", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
// Load configuration from appconfig.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Bind configuration to AppSettings class
builder.Services.Configure<MovieDBSettings>(builder.Configuration);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpClient(); // Register IHttpClientFactory for making HTTP calls

// Register Swagger services
builder.Services.AddSwaggerGen(c => 
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MovieDB API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Enable Swagger in the pipeline
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MovieDB API V1");
    c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
});
app.UseCors("AllowAngular4200");
app.UseRouting();
app.UseAuthorization();

app.MapControllers(); // Map attribute-routed controllers

app.Run();