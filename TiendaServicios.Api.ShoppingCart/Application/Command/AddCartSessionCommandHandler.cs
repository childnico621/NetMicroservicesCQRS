using MediatR;
using MySqlX.XDevAPI.Common;
using TiendaServicios.Api.ShoppingCart.Model;
using TiendaServicios.Api.ShoppingCart.Model.Request;
using TiendaServicios.Api.ShoppingCart.Model.Response;
using TiendaServicios.Api.ShoppingCart.Persistence;

namespace TiendaServicios.Api.ShoppingCart.Application.Command
{
    public class AddCartSessionCommandHandler : IRequestHandler<AddCartSessionRequestModel, AddCartSessionResponseModel>
    {
        public readonly CartContext _context;
        public AddCartSessionCommandHandler(CartContext Context) => _context = Context;

        public async Task<AddCartSessionResponseModel> Handle(AddCartSessionRequestModel request, CancellationToken cancellationToken)
        {
            var newSession = new CartSession
            {
                Created = request.Created,
            };
            var sessionResult = await _context.CartSessions.AddAsync(newSession);
            await _context.SaveChangesAsync();


            foreach (var item in request.Products)
            {
                await _context.CartDetails.AddAsync(new CartDetail
                {
                    Created = request.Created,
                    SelectedProduct = item,
                    CartSessionId = sessionResult.Entity.Id!.Value,
                });
            }
            await _context.SaveChangesAsync();

            return new AddCartSessionResponseModel
            {
                SessionId = sessionResult.Entity.Id
            };
        }
    }
}
