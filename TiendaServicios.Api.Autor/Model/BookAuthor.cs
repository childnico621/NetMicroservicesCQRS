using System.ComponentModel.DataAnnotations.Schema;

namespace TiendaServicios.Api.Autor.Model
{
    [Table("BookAuthor")]
    public class BookAuthor
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? LastName { get; set; }
        public DateTime? Birthdate { get; set; }
        public virtual ICollection<Degree> DegreeList { get; set; } = new HashSet<Degree>();
    }
}
