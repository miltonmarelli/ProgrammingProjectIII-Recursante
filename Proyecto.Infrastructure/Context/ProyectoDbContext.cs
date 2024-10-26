using Microsoft.EntityFrameworkCore;
using Proyecto.Domain.Models;
using System.Configuration;

namespace Proyecto.Infraestructure.Context
{
    public class ProyectoDbContext : DbContext
    {
        public ProyectoDbContext(DbContextOptions<ProyectoDbContext> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CommercialInvoice> CommercialInvoices { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de User y las entidades que heredan de User
            modelBuilder.Entity<User>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<User>("User")
                .HasValue<Admin>("Admin")
                .HasValue<Client>("Client")
                .HasValue<Dev>("Dev");

            // Configuración de User y las entidades que heredan de User
            modelBuilder.Entity<Admin>().HasBaseType<User>();
            modelBuilder.Entity<Client>().HasBaseType<User>();
            modelBuilder.Entity<Dev>().HasBaseType<User>();

            // Relación uno a uno entre Client y ShoppingCart
            //modelBuilder.Entity<Client>()
            //    .HasOne(c => c.ShoppingCart)
            //    .WithOne()
            //    .HasForeignKey<Client>(c => c.ShoppingCartId);
            modelBuilder.Entity<Client>()
                .HasOne(c => c.ShoppingCart)
                .WithOne(sc => sc.Client)
                .HasForeignKey<ShoppingCart>(sc => sc.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación muchos a muchos entre ShoppingCart y Producto
            modelBuilder.Entity<ShoppingCart>()
                .HasMany(sc => sc.Productos)
                .WithMany(p => p.ShoppingCarts)
                .UsingEntity<Dictionary<string, object>>(
                    "ShoppingCartProducto",
                    j => j.HasOne<Producto>().WithMany().HasForeignKey("ProductoId"),
                    j => j.HasOne<ShoppingCart>().WithMany().HasForeignKey("ShoppingCartId")
                );

            // Configurar el campo Role en la tabla Users
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .IsRequired()
                .HasMaxLength(10);
        }
    }
}

