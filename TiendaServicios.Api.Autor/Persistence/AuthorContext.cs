using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Autor.Model;

namespace TiendaServicios.Api.Autor.Persistence
{
    public class AuthorContext:DbContext
    {
        public AuthorContext(DbContextOptions<AuthorContext> options):base(options) { }

        public DbSet<BookAuthor> BookAuthors { get; set; } = null!;
        public DbSet<Degree> Degrees { get; set; } = null!;
    }
}
