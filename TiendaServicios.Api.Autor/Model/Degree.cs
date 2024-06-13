using System.ComponentModel.DataAnnotations.Schema;

namespace TiendaServicios.Api.Autor.Model
{
    [Table("Degree")]
    public class Degree
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string AcademicCenter { get; set; } = null!;
        public DateTime? DegreeDate { get; set; }

        public int BookAuthorId { get; set; }
        public virtual BookAuthor? BookAuthor { get; set; }
    }
}
