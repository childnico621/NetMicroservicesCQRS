using AutoMapper;
using TiendaServicios.Api.Autor.Model;
using TiendaServicios.Api.Autor.Model.Dto;

namespace TiendaServicios.Api.Autor.Application.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<BookAuthor, AuthorDto>();
        }
    }
}
