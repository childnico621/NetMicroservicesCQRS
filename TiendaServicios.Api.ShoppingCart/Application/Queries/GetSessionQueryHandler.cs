using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.ShoppingCart.Model.Dto;
using TiendaServicios.Api.ShoppingCart.Model.Request;
using TiendaServicios.Api.ShoppingCart.Persistence;
using TiendaServicios.Api.ShoppingCart.RemoteInterface;

namespace TiendaServicios.Api.ShoppingCart.Application.Queries
{
    public class GetSessionQueryHandler : IRequestHandler<GetSingleSessionRequestModel, CartSessionDto>
    {
        private readonly CartContext _cartContext;
        private readonly IBookService _bookService;
        public GetSessionQueryHandler(CartContext context, IBookService bookService)
        {
            _cartContext = context;
            _bookService = bookService;
        }

        public async Task<CartSessionDto> Handle(GetSingleSessionRequestModel request, CancellationToken cancellationToken)
        {
            var sessionCart = await _cartContext.CartSessions.FindAsync(request.SessionCartId);
            var detailSession = await _cartContext.CartDetails.Where(x => x.CartSessionId == request.SessionCartId).ToListAsync();

            var listDetail = new List<ProductDetailDto>();

            foreach (var product in detailSession)
            {
                var response = await _bookService.GetBook(new Guid(product.SelectedProduct));
                if (response.result)
                {
                    var book = response.Book!;
                    listDetail.Add(new ProductDetailDto
                    {
                        BookId = book.Id,
                        Title = book.Title,
                        Author = book.AuthorId.ToString(),
                        Published = book.Published,
                    });
                }
            }

            return new CartSessionDto()
            {
                CartId = sessionCart!.Id,
                CreatedSession = sessionCart.Created,
                Products = listDetail
            };

        }
    }
}
