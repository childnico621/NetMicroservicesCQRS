using System.Text.Json;
using TiendaServicios.Api.ShoppingCart.RemoteInterface;
using TiendaServicios.Api.ShoppingCart.RemoteModel;

namespace TiendaServicios.Api.ShoppingCart.RemoteService
{
    public class BookService : IBookService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger _logger;
        public BookService(IHttpClientFactory httpClientFactory, ILogger<BookService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<(bool result, RemoteBook? Book, string? ErrorMessage)> GetBook(Guid BookId)
        {
            try
            {
                var _httpClient = _httpClientFactory.CreateClient("Books");
                var response = await _httpClient.GetAsync($"/api/Books/{BookId}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<RemoteBook>(content, options);

                    return (true, result!, null);

                }

                return (false, null, response.ReasonPhrase);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Get book failed.");
                return (false, null, e.Message);
            }
        }
    }
}
