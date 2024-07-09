namespace TiendaServicios.Api.Gateway.Remote
{
    public class RemoteModelBook
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime? Published { get; set; }
        public Guid? AuthorId { get; set; }
        public RemoteModelAuthor? Author { get; set; }
    }
}
