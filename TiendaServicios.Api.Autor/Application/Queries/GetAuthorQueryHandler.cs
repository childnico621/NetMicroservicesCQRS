using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Autor.Model;
using TiendaServicios.Api.Autor.Model.Dto;
using TiendaServicios.Api.Autor.Model.Request;
using TiendaServicios.Api.Autor.Model.Response;
using TiendaServicios.Api.Autor.Persistence;

namespace TiendaServicios.Api.Autor.Application.Queries
{
    public class GetAuthorQueryHandler : IRequestHandler<GetAuthorsRequestModel, List<AuthorDto>>
    {
        private readonly AuthorContext _context;
        private readonly IMapper _mapper;
        public GetAuthorQueryHandler(AuthorContext Context, IMapper Mapper)
        {
            _context = Context;
            _mapper = Mapper;
        }

        public async Task<List<AuthorDto>> Handle(GetAuthorsRequestModel request, CancellationToken cancellationToken)
        {
            var authors = await _context.BookAuthors.ToListAsync();
            var authorsDto= _mapper.Map<List<BookAuthor>,List<AuthorDto>>(authors);
            return authorsDto;
        }
    }
}
