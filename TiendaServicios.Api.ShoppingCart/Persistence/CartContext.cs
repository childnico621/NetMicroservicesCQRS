using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TiendaServicios.Api.ShoppingCart.Model;

namespace TiendaServicios.Api.ShoppingCart.Persistence
{
    public class CartContext : DbContext
    {
        public CartContext(DbContextOptions<CartContext> options) : base(options) {        }

        public virtual DbSet<CartSession> CartSessions { get; set; } = null!;               
        public virtual DbSet<CartDetail> CartDetails { get; set; } = null!;

    }
}
