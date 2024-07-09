using MediatR;
using TiendaServicios.Api.Book.Model;
using TiendaServicios.Api.Book.Model.Request;
using TiendaServicios.Api.Book.Model.Response;
using TiendaServicios.Api.Book.Persistence;
using TiendaServicios.RabbitMQ.Bus.BusRabbit;
using TiendaServicios.RabbitMQ.Bus.QueueEvent;

namespace TiendaServicios.Api.Book.Application.Commands
{
    public class AddBookCommandHandler : IRequestHandler<AddBookRequestModel, AddBookResponseModel>
    {
        public readonly LibraryContext _context;
        private readonly IRabbitEventBus _eventBus;
        public AddBookCommandHandler(LibraryContext Context, IRabbitEventBus eventBus)
        {
            _context = Context;
            _eventBus = eventBus;
        }

        public async Task<AddBookResponseModel> Handle(AddBookRequestModel request, CancellationToken cancellationToken)
        {
            var newBook = new BookMaterial
            {
                Title = request.Title,
                Published = request.Published,
                AuthorId = request.AuthorId
            };
            var dbBook = _context.Library.FirstOrDefault(x => x.Title == newBook.Title && x.AuthorId == newBook.AuthorId);
            if (dbBook != null) return new AddBookResponseModel { BookId = dbBook.Id };

            var result = await _context.Library.AddAsync(newBook);
            await _context.SaveChangesAsync();

            _eventBus.Publish(new EmailQueueEvent("childnico621@gmail.com", request.Title, "this content is a example"));

            return new AddBookResponseModel
            {
                BookId = result.Entity.Id
            };
        }
    }
}
