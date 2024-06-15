using System.ComponentModel.DataAnnotations.Schema;

namespace TiendaServicios.Api.ShoppingCart.Model
{
    [Table("ShoppingCartSession")]
    public class CartSession
    {
        public Guid? Id { get; set; }
        public DateTime? Created { get; set; }

        public ICollection<CartDetail> Products { get; set; } = new HashSet<CartDetail>();
    }
}
