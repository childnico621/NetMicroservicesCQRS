using System.ComponentModel.DataAnnotations.Schema;

namespace TiendaServicios.Api.ShoppingCart.Model
{
    [Table("ShoppingCartDetail")]
    public class CartDetail
    {
        public Guid? Id { get; set; }
        public DateTime? Created { get; set; }
        public string SelectedProduct { get; set; } = null!;

        public Guid CartSessionId { get; set; }
        public CartSession? CartSession { get; set; }
    }
}
