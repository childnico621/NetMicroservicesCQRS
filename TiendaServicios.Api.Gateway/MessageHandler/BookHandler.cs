
using System.Diagnostics;
using System.Text.Json;
using TiendaServicios.Api.Gateway.Remote;
using TiendaServicios.Api.Gateway.RemoteInterface;

namespace TiendaServicios.Api.Gateway.MessageHandler
{
    public class BookHandler : DelegatingHandler
    {
        private readonly ILogger<BookHandler> _logger;
        private readonly IAuthorRemote _authorRemote;

        public BookHandler(ILogger<BookHandler> logger, IAuthorRemote authorRemote)
        {
            _logger = logger;
            _authorRemote = authorRemote;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var tiempo = Stopwatch.StartNew();
            _logger.LogInformation("inicia el request");
            var response = await base.SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
                return response;

            var content = await response.Content.ReadAsStreamAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var result = JsonSerializer.Deserialize<RemoteModelBook>(content, options);
            var authorResponse = await _authorRemote.GetAuthor(result?.AuthorId ?? Guid.Empty);

            if (authorResponse.result)
            {
                result.Author = authorResponse.author;
                var strResult = JsonSerializer.Serialize(result);
                response.Content = new StringContent(strResult, System.Text.Encoding.UTF8, "application/json");
            }

            _logger.LogInformation("se realizo en {ElapsedMilliseconds} ms", tiempo.ElapsedMilliseconds);
            return response;
        }
    }
}
