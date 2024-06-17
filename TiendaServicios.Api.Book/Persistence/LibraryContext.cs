using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TiendaServicios.Api.Book.Model;

namespace TiendaServicios.Api.Book.Persistence
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        public virtual DbSet<BookMaterial> Library { get; set; } = null!;
    }
}
