using MediatR;
using TiendaServicios.Api.Autor.Model.Response;

namespace TiendaServicios.Api.Autor.Model.Request
{
    public class AddAuthorRequestModel: IRequest<AddAuthorResponseModel>
    {
        public string Name { get; set; } = null!;
        public string? LastName { get; set; }
        public DateTime? Birthdate { get; set; }
    }
}
