namespace TiendaServicios.Api.ShoppingCart.Model.Dto
{
    public class CartSessionDto
    {
        public Guid? CartId { get; set; }
        public DateTime? CreatedSession { get; set; }
        public List<ProductDetailDto>? Products { get; set; }
    }
}
