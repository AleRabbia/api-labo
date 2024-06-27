using MiApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MiApi.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<DetalleCompra> DetallesCompra { get; set; }


    }
}
