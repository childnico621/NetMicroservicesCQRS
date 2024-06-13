using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TiendaServicios.Api.Autor.Application.Queries;
using TiendaServicios.Api.Autor.Model.Request;
using TiendaServicios.Api.Autor.Model.Validators;
using TiendaServicios.Api.Autor.Persistence;

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
        Title = "MS Authors",
        Version = "v1",
        Description = "Servicio para administracion de Autores de libros"
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
