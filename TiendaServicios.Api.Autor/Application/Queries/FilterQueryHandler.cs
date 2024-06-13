using AutoMapper;
using MediatR;
using TiendaServicios.Api.Autor.Model;
using TiendaServicios.Api.Autor.Model.Dto;
using TiendaServicios.Api.Autor.Model.Request;
using TiendaServicios.Api.Autor.Persistence;

namespace TiendaServicios.Api.Autor.Application.Queries
{
    public class FilterQueryHandler:IRequestHandler<GetSingleAuthorRequestModel, AuthorDto>
    {
        public readonly AuthorContext _context;
        private readonly IMapper _mapper;
        public FilterQueryHandler(AuthorContext Context, IMapper Mapper)
        {
            _context = Context;
            _mapper = Mapper;
        }

        public async Task<AuthorDto> Handle(GetSingleAuthorRequestModel request, CancellationToken cancellationToken)
        {
            var bookAuthor = await _context.BookAuthors.FindAsync(request.BookAuthorId);
            if (bookAuthor == null) return new AuthorDto();

            var authorDto = _mapper.Map<BookAuthor, AuthorDto>(bookAuthor);
            return authorDto;
        }
    }
}
