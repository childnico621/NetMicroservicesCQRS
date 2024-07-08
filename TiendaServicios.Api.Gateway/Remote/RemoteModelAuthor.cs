namespace TiendaServicios.Api.Gateway.Remote
{
    public class RemoteModelAuthor
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime? Birthdate { get; set; }
    }
}
