using GenFu;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Book.Model;
using TiendaServicios.Api.Book.Persistence;

namespace TiendaServicios.Api.Test.Books.Fixture
{
    public static class GeneralFixture
    {
        private static IEnumerable<BookMaterial> GetTestData()
        {
            A.Configure<BookMaterial>()
                .Fill(x => x.Title).AsArticleTitle()
                .Fill(x => x.Id, () => { return Guid.NewGuid(); });

            var lista = A.ListOf<BookMaterial>(29);

            return lista;
        }
        public static async Task<LibraryContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new LibraryContext(options);
            databaseContext.Database.EnsureCreated();

            if (!(await databaseContext.Library.AnyAsync()))
            {
                databaseContext.Library.Add(new BookMaterial()
                {
                    Id = new Guid("8c90c679-8bff-48cd-0493-08dc8cb4c889"),
                    Title= "El Viejo y el Mar",
                    Published= Convert.ToDateTime("1952-09-01T00:00:00"),
                    AuthorId=new Guid("6040706d-adb6-46bb-8ae9-0b460e65a015")
                });
                databaseContext.Library.AddRange(GetTestData());

                await databaseContext.SaveChangesAsync();
            }

            return databaseContext;
        }
    }
}
