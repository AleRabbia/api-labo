using MiApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MiApi.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Producto> Productos { get; set; }
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
