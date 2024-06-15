using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TiendaServicios.Api.ShoppingCart.Persistence;
using TiendaServicios.Api.ShoppingCart.RemoteInterface;
using TiendaServicios.Api.ShoppingCart.RemoteService;

var builder = WebApplication.CreateBuilder(args);

var AuthorsEndpoint = builder.Configuration.GetValue<string>("authors_service");
var BooksEndpoint = builder.Configuration.GetValue<string>("books_service");
// Add services to the container.
builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly); });


builder.Services.AddHttpClient("Authors", httpClient => { httpClient.BaseAddress = new Uri(AuthorsEndpoint); });
builder.Services.AddHttpClient("Books", httpClient => { httpClient.BaseAddress = new Uri(BooksEndpoint); });

builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddDbContext<CartContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("ShoppingCartConn")!));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Shopping Cart API",
        Version = "v1",
        Description = "MS carrito de compras"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("./v1/swagger.json", "V1"); });

app.UseAuthorization();

app.MapControllers();

app.Run();
