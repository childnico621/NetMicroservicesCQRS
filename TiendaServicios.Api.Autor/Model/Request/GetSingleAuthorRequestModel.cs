using MediatR;
using TiendaServicios.Api.Autor.Model.Dto;

namespace TiendaServicios.Api.Autor.Model.Request
{
    public class GetSingleAuthorRequestModel:IRequest<AuthorDto>
    {
        public Guid BookAuthorId { get; set; }
    }
}
