using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Autor.Model;

namespace TiendaServicios.Api.Autor.Persistence
{
    public class AuthorContext:DbContext
    {
        public AuthorContext(DbContextOptions<AuthorContext> options):base(options) { }

        public virtual DbSet<BookAuthor> BookAuthors { get; set; } = null!;
        public virtual DbSet<Degree> Degrees { get; set; } = null!;
    }
}
