using TiendaServicios.Api.ShoppingCart.RemoteModel;

namespace TiendaServicios.Api.ShoppingCart.RemoteInterface
{
    public interface IBookService
    {
        Task<(bool result, RemoteBook? Book, string? ErrorMessage)> GetBook(Guid BookId);
    }
}
