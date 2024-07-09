using Ocelot.Responses;
using System.Text.Json;
using TiendaServicios.Api.Gateway.Remote;
using TiendaServicios.Api.Gateway.RemoteInterface;

namespace TiendaServicios.Api.Gateway.RemoteImplement
{
    public class RemoteAuthor : IAuthorRemote
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<RemoteAuthor> _logger;
        public RemoteAuthor(IHttpClientFactory httpClientFactory, ILogger<RemoteAuthor> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }
        public async Task<(bool result, RemoteModelAuthor? author, string? ErrorMessage)> GetAuthor(Guid AuthorId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("AuthorService");
                var response = await client.GetAsync($"/authors/{AuthorId}");
                if (!response.IsSuccessStatusCode)
                    return (false, null, response.ReasonPhrase);

                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var result = JsonSerializer.Deserialize<RemoteModelAuthor>(content, options);

                return (true, result, null);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "remote request");
                return (false, null, e.Message);
            }
        }
    }
}
