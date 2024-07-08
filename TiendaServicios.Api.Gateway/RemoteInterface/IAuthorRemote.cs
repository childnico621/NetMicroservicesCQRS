using TiendaServicios.Api.Gateway.Remote;

namespace TiendaServicios.Api.Gateway.RemoteInterface
{
    public interface IAuthorRemote
    {
        Task<(bool result, RemoteModelAuthor? author, string? ErrorMessage)> GetAuthor(Guid AuthorId);
    }
}
