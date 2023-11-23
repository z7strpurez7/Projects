using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using shoppingAPI.Context;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Add dbContext
builder.Services.AddDbContext<myDbContext>(option =>
//Getting connectionstring from appsettings.json

option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);


//husk å legge til <GenerateDocumentationFile>true</GenerateDocumentationFile> i i PropertyGroup tag i .csproj file

var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "Shopping-API",
        Description = "Run your own E-Commerce",
        Contact = new OpenApiContact
        {
            Name = "Ali Todashev",
        }
    });
    options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
//app.UseSwaggerUI();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1"));

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
