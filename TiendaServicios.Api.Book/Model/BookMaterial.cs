using System.ComponentModel.DataAnnotations.Schema;

namespace TiendaServicios.Api.Book.Model
{
    [Table("Book")]
    public class BookMaterial
    {
        public Guid? Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime? Published { get; set; }
        public Guid? AuthorId { get; set; }
    }
}
