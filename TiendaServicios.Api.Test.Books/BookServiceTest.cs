using AutoMapper;
using TiendaServicios.Api.Book.Application.Commands;
using TiendaServicios.Api.Book.Application.Queries;
using TiendaServicios.Api.Book.Model.Dto;
using TiendaServicios.Api.Book.Model.Request;
using TiendaServicios.Api.Book.Model.Response;
using TiendaServicios.Api.Test.Books.Fixture;

namespace TiendaServicios.Api.Test.Books
{
    public class BookServiceTest
    {


        [Fact]
        public async Task GetBooks()
        {
            //arrange
            var mockContext = await GeneralFixture.GetDatabaseContext();
            var mapConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MappingTest()));
            var mapper = mapConfig.CreateMapper();
            var sut = new GetBookQueryHandler(mockContext, mapper);
            var request = new GetBooksRequestModel();
            //act
            var result = await sut.Handle(request, CancellationToken.None);

            //assert
            Assert.NotNull(result);
            Assert.True(result.Any());

        }

        [Fact]
        public async Task GetQueryBook()
        {
            //arrange
            var mockContext = await GeneralFixture.GetDatabaseContext();
            var mapConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MappingTest()));
            var mapper = mapConfig.CreateMapper();
            var sut = new FilterGetBookQueryHandler(mockContext, mapper);
            var request = new GetSingleBookRequestModel() { BookId = new Guid("8c90c679-8bff-48cd-0493-08dc8cb4c889") };

            //act
            var result = await sut.Handle(request, CancellationToken.None);

            //assert
            Assert.NotNull(result);
            Assert.IsType<BookDto>(result);

        }


        [Fact]
        public async Task AddBook()
        {
            //arrange
            var mockContext = await GeneralFixture.GetDatabaseContext();
            var request = new AddBookRequestModel
            {
                Title = "La Metamorfosis",
                Published = Convert.ToDateTime("1915-01-01T00:00:00"),
                AuthorId = new Guid("5a5d733b-f92a-441b-99ed-071fb4e38960")
            };
            var sut = new AddBookCommandHandler(mockContext);

            //act
            var result = await sut.Handle(request, CancellationToken.None);

            //assert
            Assert.NotNull(result);
            Assert.IsType<AddBookResponseModel>(result);
            Assert.Equal(31, mockContext.Library.Count());
        }
    }
}