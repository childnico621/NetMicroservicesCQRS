using AutoMapper;
using TiendaServicios.Api.Book.Model;
using TiendaServicios.Api.Book.Model.Dto;

namespace TiendaServicios.Api.Book.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookMaterial, BookDto>();
        }
    }
}
