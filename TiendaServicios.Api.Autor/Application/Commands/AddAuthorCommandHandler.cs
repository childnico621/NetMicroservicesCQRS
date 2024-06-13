using MediatR;
using TiendaServicios.Api.Autor.Model;
using TiendaServicios.Api.Autor.Model.Request;
using TiendaServicios.Api.Autor.Model.Response;
using TiendaServicios.Api.Autor.Persistence;

namespace TiendaServicios.Api.Autor.Application.Command
{
    public class AddAuthorCommandHandler : IRequestHandler<AddAuthorRequestModel, AddAuthorResponseModel>
    {
        public readonly AuthorContext _context;
        public AddAuthorCommandHandler(AuthorContext Context) => _context = Context;
        public async Task<AddAuthorResponseModel> Handle(AddAuthorRequestModel request, CancellationToken cancellationToken)
        {
            var bookAuthor = new BookAuthor
            {
                Id= Guid.NewGuid(),
                Name = request.Name,
                LastName = request.LastName,
                Birthdate = request.Birthdate
            };

            var result = await _context.BookAuthors.AddAsync(bookAuthor);
            await _context.SaveChangesAsync();


            return new AddAuthorResponseModel
            {
                AuthorId = result.Entity.Id
            };
        }


    }
}
