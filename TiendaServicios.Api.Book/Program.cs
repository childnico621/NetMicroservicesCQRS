using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TiendaServicios.Api.Book.Application.Queries;
using TiendaServicios.Api.Book.Model.Request;
using TiendaServicios.Api.Book.Model.Validators;
using TiendaServicios.Api.Book.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly); });

builder.Services.AddDbContext<LibraryContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbBooksConn")));

//GetBookQueryHandler
builder.Services.AddAutoMapper(typeof(GetBookQueryHandler));

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<AddBookRequestModel>, AddBookValidator>();

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Books API",
        Version = "v1",
        Description = "Servicio administración de libros"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("./v1/swagger.json", "V1"); });


app.UseAuthorization();

app.MapControllers();

app.Run();
