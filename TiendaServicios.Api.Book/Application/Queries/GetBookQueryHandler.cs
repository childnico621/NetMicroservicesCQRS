using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Book.Model;
using TiendaServicios.Api.Book.Model.Dto;
using TiendaServicios.Api.Book.Model.Request;
using TiendaServicios.Api.Book.Persistence;

namespace TiendaServicios.Api.Book.Application.Queries
{
    public class GetBookQueryHandler : IRequestHandler<GetBooksRequestModel, List<BookDto>>
    {
        private readonly LibraryContext _context;
        private readonly IMapper _mapper;
        public GetBookQueryHandler(LibraryContext Context, IMapper Mapper)
        {
            _context = Context;
            _mapper = Mapper;
        }

        public async Task<List<BookDto>> Handle(GetBooksRequestModel request, CancellationToken cancellationToken)
        {
            var dbBooks =await _context.Library.ToListAsync();
            var listDtoBooks= _mapper.Map<List<BookMaterial>, List<BookDto>>(dbBooks);

            return listDtoBooks;
        }
    }
}
