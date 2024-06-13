using Microsoft.EntityFrameworkCore;
using MediatR;
using System.Reflection;
using TiendaServicios.Api.Autor.Persistence;
using Microsoft.AspNetCore.Hosting;
using TiendaServicios.Api.Autor.Application.Command;
using Microsoft.OpenApi.Models;
using FluentValidation.AspNetCore;
using TiendaServicios.Api.Autor.Model.Request;
using FluentValidation;
using TiendaServicios.Api.Autor.Model.Validators;
using TiendaServicios.Api.Autor.Application.Queries;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});

builder.Services.AddDbContext<AuthorContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("psdbconnection"));
});

builder.Services.AddAutoMapper(typeof(GetAuthorQueryHandler));
builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<AddAuthorRequestModel>, AddAuthorValidator>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SIM Core API",
        Version = "v1",
        Description = "Servicio Principal SIM para registro de plantas Eólicas y Solares"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("./v1/swagger.json", "V1"); });

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
