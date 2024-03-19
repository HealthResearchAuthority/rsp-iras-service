using System.Reflection;
using Microsoft.OpenApi.Models;
using Rsp.IrasService.Application.Contracts;
using Rsp.IrasService.Services;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
// Add services to the container.
services.AddHealthChecks();
services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();

services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "IRAS Categories API",
        Description = "Categories API for managing application and project categories",
    });

    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

// add the categories service to the DI container
services.AddTransient<ICategoriesService, CategoriesService>();

var app = builder.Build();

app.MapHealthChecks("/health");

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