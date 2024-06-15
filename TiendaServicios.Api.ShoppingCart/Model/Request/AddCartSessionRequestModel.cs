using MediatR;
using TiendaServicios.Api.ShoppingCart.Model.Response;

namespace TiendaServicios.Api.ShoppingCart.Model.Request
{
    public class AddCartSessionRequestModel : IRequest<AddCartSessionResponseModel>
    {
        public DateTime? Created { get; set; }
        public List<string> Products { get; set; } = null!;
    }
}
