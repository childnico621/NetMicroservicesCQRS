namespace TiendaServicios.Api.ShoppingCart.Model.Dto
{
    public class ProductDetailDto
    {
        public Guid? BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateTime? Published { get; set; }
    }
}
