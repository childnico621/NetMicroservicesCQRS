using MediatR;
using TiendaServicios.Api.Book.Model.Dto;

namespace TiendaServicios.Api.Book.Model.Request
{
    public class GetBooksRequestModel : IRequest<List<BookDto>> { }
}
