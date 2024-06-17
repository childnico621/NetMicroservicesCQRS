using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaServicios.Api.Book.Model;
using TiendaServicios.Api.Book.Model.Dto;

namespace TiendaServicios.Api.Test.Books.Fixture
{
    public class MappingTest : Profile
    {
        public MappingTest()
        {
            CreateMap<BookMaterial, BookDto>();
        }
    }
}
