using MediatR;
using TiendaServicios.Api.Book.Model;
using TiendaServicios.Api.Book.Model.Request;
using TiendaServicios.Api.Book.Model.Response;
using TiendaServicios.Api.Book.Persistence;

namespace TiendaServicios.Api.Book.Application.Commands
{
    public class AddBookCommandHandler : IRequestHandler<AddBookRequestModel, AddBookResponseModel>
    {
        public readonly LibraryContext _context;

        public AddBookCommandHandler(LibraryContext Context) => _context = Context;

        public async Task<AddBookResponseModel> Handle(AddBookRequestModel request, CancellationToken cancellationToken)
        {
            var newBook = new BookMaterial
            {                
                Title= request.Title,
                Published= request.Published,
                AuthorId = request.AuthorId
            };

            var result =  await _context.Library.AddAsync(newBook);
            await _context.SaveChangesAsync();

            return new AddBookResponseModel
            {
                BookId = result.Entity.Id
            };
        }
    }
}
