using MediatR;
using TiendaServicios.Api.Book.Model.Response;

namespace TiendaServicios.Api.Book.Model.Request
{
    public class AddBookRequestModel : IRequest<AddBookResponseModel>
    {
        public string Title { get; set; } = null!;
        public DateTime? Published { get; set; }
        public Guid? AuthorId { get; set; }
    }
}
