using Microsoft.EntityFrameworkCore;


namespace hardStore.Models
{
    public class ProductosContext : DbContext
    {

        public ProductosContext(DbContextOptions<ProductosContext> options)
            :base(options)
        {

        }
        public DbSet<Producto> Productos {get;set;}
        public DbSet<Producto> Marcas { get; set; }
        public DbSet<Producto> TipoProductos { get; set; }

    }

}