using MediatR;
using TiendaServicios.Api.ShoppingCart.Model.Dto;

namespace TiendaServicios.Api.ShoppingCart.Model.Request
{
    public class GetCartSessionRequestModel : IRequest<List<CartSessionDto>> { }
    
}
