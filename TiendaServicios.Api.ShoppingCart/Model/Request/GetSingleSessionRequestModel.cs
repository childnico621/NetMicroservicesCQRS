using MediatR;
using TiendaServicios.Api.ShoppingCart.Model.Dto;

namespace TiendaServicios.Api.ShoppingCart.Model.Request
{
    public class GetSingleSessionRequestModel : IRequest<CartSessionDto>
    {
        public Guid SessionCartId { get; set; }
    }
}
