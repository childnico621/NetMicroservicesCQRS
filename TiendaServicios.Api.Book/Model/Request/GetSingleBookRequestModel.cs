using MediatR;
using TiendaServicios.Api.Book.Model.Dto;

namespace TiendaServicios.Api.Book.Model.Request
{
    public class GetSingleBookRequestModel : IRequest<BookDto>
    {
        public Guid BookId { get; set; }
    }
}
