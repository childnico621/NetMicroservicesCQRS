namespace TiendaServicios.Api.Autor.Model.Dto
{
    public class AuthorDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime? Birthdate { get; set; }
    }
}
