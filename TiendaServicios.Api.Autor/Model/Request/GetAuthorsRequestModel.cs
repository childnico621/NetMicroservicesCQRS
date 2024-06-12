using MediatR;
using TiendaServicios.Api.Autor.Model.Dto;
using TiendaServicios.Api.Autor.Model.Response;

namespace TiendaServicios.Api.Autor.Model.Request
{
    public class GetAuthorsRequestModel : IRequest<List<AuthorDto>> { }
}
