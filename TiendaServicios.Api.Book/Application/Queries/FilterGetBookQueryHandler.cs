using AutoMapper;
using MediatR;
using TiendaServicios.Api.Book.Model;
using TiendaServicios.Api.Book.Model.Dto;
using TiendaServicios.Api.Book.Model.Request;
using TiendaServicios.Api.Book.Persistence;

namespace TiendaServicios.Api.Book.Application.Queries
{
    public class FilterGetBookQueryHandler : IRequestHandler<GetSingleBookRequestModel, BookDto>
    {
        private readonly LibraryContext _context;
        private readonly IMapper _mapper;
        public FilterGetBookQueryHandler(LibraryContext Context, IMapper Mapper)
        {
            _context = Context;
            _mapper = Mapper;
        }

        public async Task<BookDto> Handle(GetSingleBookRequestModel request, CancellationToken cancellationToken)
        {
            var dbBook = await _context.Library.FindAsync(request.BookId);
            if (dbBook == null) return new BookDto();

            var bookdto = _mapper.Map<BookMaterial, BookDto>(dbBook);

            return bookdto;

        }
    }
}
