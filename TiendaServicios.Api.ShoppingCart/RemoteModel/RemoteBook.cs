namespace TiendaServicios.Api.ShoppingCart.RemoteModel
{
    public class RemoteBook
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime? Published { get; set; }
        public Guid AuthorId { get; set; }
    }
}
