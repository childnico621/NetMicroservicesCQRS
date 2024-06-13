namespace TiendaServicios.Api.Book.Model.Dto
{
    public class BookDto
    {
        public Guid? Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime? Published { get; set; }
        public Guid? AuthorId { get; set; }
    }
}
