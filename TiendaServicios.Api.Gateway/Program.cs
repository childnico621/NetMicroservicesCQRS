using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using TiendaServicios.Api.Gateway.MessageHandler;
using TiendaServicios.Api.Gateway.RemoteImplement;
using TiendaServicios.Api.Gateway.RemoteInterface;

var builder = WebApplication.CreateBuilder(args);
var GatewayEndpoint = builder.Configuration.GetValue<string>("gateway_service");

// Add services to the container.
// Add Ocelot json file configuration
builder.Services.AddHttpClient("AuthorService", httpClient => { httpClient.BaseAddress = new Uri(GatewayEndpoint); });
builder.Services.AddSingleton<IAuthorRemote, RemoteAuthor>();

builder.Services.AddOcelot().AddDelegatingHandler<BookHandler>();

builder.Configuration.AddJsonFile("ocelot.json");


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(_ => { });



await app.UseOcelot();

app.Run();
