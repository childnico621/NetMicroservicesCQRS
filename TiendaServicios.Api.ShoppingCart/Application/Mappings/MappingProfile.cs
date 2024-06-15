using AutoMapper;
using TiendaServicios.Api.ShoppingCart.Model;
using TiendaServicios.Api.ShoppingCart.Model.Dto;

namespace TiendaServicios.Api.ShoppingCart.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CartSession, CartSessionDto>();
        }
    }
}
